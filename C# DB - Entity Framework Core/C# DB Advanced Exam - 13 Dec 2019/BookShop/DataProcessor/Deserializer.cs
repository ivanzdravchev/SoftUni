namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(BookDto[]), new XmlRootAttribute("Books"));

            var booksDto = (BookDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Book> books = new List<Book>();

            foreach (var bookDto in booksDto)
            {
                if (!IsValid(bookDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isDateParsed = DateTime.TryParseExact(bookDto.PublishedOn,
                    "MM/dd/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var PublishDate);

                if (!isDateParsed)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isGenreParsed = Enum.IsDefined(typeof(Genre), bookDto.Genre);
                if (!isGenreParsed)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var parsedGenre = (Genre)bookDto.Genre;

                var book = new Book
                {
                    Name = bookDto.Name,
                    Genre = parsedGenre,
                    Price = bookDto.Price,
                    Pages = bookDto.Pages,
                    PublishedOn = PublishDate
                };

                books.Add(book);
                sb.AppendLine($"Successfully imported book {book.Name} for {book.Price:f2}.");
            }

            context.Books.AddRange(books);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var authorsDto = JsonConvert.DeserializeObject<List<AuthorDto>>(jsonString);

            List<Author> authors = new List<Author>();

            var validBookIds = context.Books.Select(b => b.Id).ToList();

            foreach (var authorDto in authorsDto)
            {
                if (!IsValid(authorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (authors.Any(a => a.Email == authorDto.Email))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName,
                    Phone = authorDto.Phone,
                    Email = authorDto.Email
                };

                foreach (var authorBookDto in authorDto.Books)
                {
                    if (!authorBookDto.Id.HasValue || !validBookIds.Contains(authorBookDto.Id.Value))
                    {
                        continue;
                    }

                    author.AuthorsBooks.Add(new AuthorBook { BookId = authorBookDto.Id.Value });
                }

                if (author.AuthorsBooks.Count() == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                authors.Add(author);
                sb.AppendLine($"Successfully imported author - {author.FirstName} {author.LastName} with {author.AuthorsBooks.Count()} books.");
            }

            context.Authors.AddRange(authors);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}