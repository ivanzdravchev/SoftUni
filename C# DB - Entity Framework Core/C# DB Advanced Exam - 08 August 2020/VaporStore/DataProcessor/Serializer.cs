namespace VaporStore.DataProcessor
{
	using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
			var games = context.Genres
				.ToList()
				.Where(genre => genreNames.Contains(genre.Name))
				.Select(genre => new
				{
					Id = genre.Id,
					Genre = genre.Name,
					Games = genre.Games
						.Where(game => game.Purchases.Count() > 0)
						.Select(game => new
						{
							Id = game.Id,
							Title = game.Name,
							Developer = game.Developer.Name,
							Tags = string.Join(", ", game.GameTags.Select(gt => gt.Tag.Name)),
							Players = game.Purchases.Count()
						})
						.OrderByDescending(game => game.Players)
						.ThenBy(game => game.Id)
						.ToList(),
					TotalPlayers = genre.Games.Sum(g => g.Purchases.Count)
				})
				.OrderByDescending(g => g.TotalPlayers)
				.ThenBy(g => g.Id)
				.ToList();

			return JsonConvert.SerializeObject(games, Formatting.Indented);
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			var users = context.Users
				.ToArray()
				.Where(u => u.Cards.Any(c => c.Purchases.Any()))
				.Select(u => new UserDto
				{
					Username = u.Username,
					Purchases = context.Purchases
						.ToArray()
						.Where(p => p.Card.User.Id == u.Id && p.Type.ToString() == storeType)
						.OrderBy(p => p.Date)
						.Select(p => new PurchaseDto
						{
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
							Game = new GameDto
                            {
								Title = p.Game.Name,
								Genre = p.Game.Genre.Name,
								Price = p.Game.Price
                            }
                        })
						.ToArray(),
					TotalSpent = context.Purchases
						.ToArray()
						.Where(p => p.Card.User.Id == u.Id && p.Type.ToString() == storeType)
						.Sum(p => p.Game.Price)
				})
				.Where(u => u.Purchases.Length > 0)
				.OrderByDescending(u => u.TotalSpent)
				.ThenBy(u => u.Username)
				.ToArray();

			XmlSerializer serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("Users"));

			StringWriter writer = new StringWriter();

			XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
			emptyNamespace.Add("", "");

			serializer.Serialize(writer, users, emptyNamespace);

			return writer.ToString();
		}
	}
}