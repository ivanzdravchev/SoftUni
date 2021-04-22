namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			StringBuilder sb = new StringBuilder();

			var gamesDto = JsonConvert.DeserializeObject<List<GameDto>>(jsonString);

			List<Game> games = new List<Game>();

			List<Developer> developers = new List<Developer>();
			List<Genre> genres = new List<Genre>();
			List<Tag> tags = new List<Tag>();

			foreach (var gameDto in gamesDto)
            {
				if (!IsValid(gameDto))
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

				var isDateParsed = DateTime.TryParseExact(gameDto.ReleaseDate,
					"yyyy-MM-dd",
					CultureInfo.InvariantCulture,
					DateTimeStyles.None,
					out var ReleaseDate);

				if (!isDateParsed)
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				var game = new Game
				{
					Name = gameDto.Name,
					Price = gameDto.Price,
					ReleaseDate = ReleaseDate
				};

				var developer = developers.FirstOrDefault(d => d.Name == gameDto.Developer);
				if (developer is null)
                {
					developer = new Developer { Name = gameDto.Developer };
                }
				game.Developer = developer;
				developers.Add(developer);

				var genre = genres.FirstOrDefault(g => g.Name == gameDto.Genre);
				if (genre is null)
                {
					genre = new Genre { Name = gameDto.Genre };
                }
				game.Genre = genre;
				genres.Add(genre);

				foreach (var tagName in gameDto.Tags)
                {
					var tag = tags.FirstOrDefault(t => t.Name == tagName);
					if (tag is null)
                    {
						tag = new Tag { Name = tagName };
                    }
					game.GameTags.Add(new GameTag { Game = game, Tag = tag });
					tags.Add(tag);
                }

				if (game.GameTags.Count() == 0)
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				games.Add(game);
				sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count()} tags");
            }

			context.Games.AddRange(games);

			context.SaveChanges();

			return sb.ToString().Trim();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			StringBuilder sb = new StringBuilder();

			var usersDto = JsonConvert.DeserializeObject<List<UserDto>>(jsonString);

			List<User> users = new List<User>();

			foreach (var userDto in usersDto)
            {
				if (!IsValid(userDto))
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				var user = new User
				{
					FullName = userDto.FullName,
					Username = userDto.Username,
					Email = userDto.Email,
					Age = userDto.Age
				};

				List<Card> cards = new List<Card>();

				foreach (var cardDto in userDto.Cards)
                {
					if (!IsValid(cardDto))
                    {
						cards.Clear();
						break;
					}

					var card = new Card
					{
						Number = cardDto.Number,
						Cvc = cardDto.Cvc,
						Type = cardDto.Type
					};
					cards.Add(card);
                }

				if (!cards.Any())
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				user.Cards = cards;

				users.Add(user);
				sb.AppendLine($"Imported {user.Username} with {user.Cards.Count()} cards");
            }

			context.Users.AddRange(users);

			context.SaveChanges();

			return sb.ToString().Trim();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof(PurchaseDto[]), new XmlRootAttribute("Purchases"));

			var purchasesDto = (PurchaseDto[])serializer.Deserialize(new StringReader(xmlString));

			List<Purchase> purchases = new List<Purchase>();

			foreach (var purchaseDto in purchasesDto)
            {
				bool isTypeParsed = Enum.TryParse(purchaseDto.Type, out PurchaseType PurchaseType);
				if (!isTypeParsed)
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				DateTime.TryParseExact(purchaseDto.Date,
					"dd/MM/yyyy HH:mm",
					CultureInfo.InvariantCulture,
					DateTimeStyles.None,
					out var PurchaseDate);

				var card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.Card);
				if (card is null)
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				var game = context.Games.FirstOrDefault(g => g.Name == purchaseDto.Title);
				if (game is null)
                {
					sb.AppendLine("Invalid Data");
					continue;
				}

				var purchase = new Purchase
				{
					Type = PurchaseType,
					ProductKey = purchaseDto.Key,
					Date = PurchaseDate,
					Card = card,
					Game = game
				};

				purchases.Add(purchase);
				sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
			}

			context.Purchases.AddRange(purchases);

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