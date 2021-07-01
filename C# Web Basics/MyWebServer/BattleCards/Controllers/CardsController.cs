using BattleCards.Data;
using BattleCards.Data.Models;
using BattleCards.Services;
using BattleCards.ViewModels.Card;
using Microsoft.EntityFrameworkCore;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using System.Linq;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly IValidator validator;
        private readonly BattleCardsDbContext data;

        public CardsController(IValidator validator, BattleCardsDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(CreateCardViewModel model)
        {
            var modelErrors = this.validator.ValidateCardCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var card = new Card
            {
                Name = model.Name,
                ImageUrl = model.Image,
                Keyword = model.Keyword,
                Attack = model.Attack,
                Health = model.Health,
                Description = model.Description
            };

            this.data.Cards.Add(card);

            var userCard = new UserCard { UserId = User.Id, CardId = card.Id };

            this.data.UserCards.Add(userCard);

            this.data.SaveChanges();

            return Redirect("/Cards/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var allCards = this.data.Cards
                .Select(card => new AllCardsViewModel
                {
                    Id = card.Id,
                    Name = card.Name,
                    ImageUrl = card.ImageUrl,
                    Keyword = card.Keyword,
                    Attack = card.Attack,
                    Health = card.Health,
                    Description = card.Description
                })
                .ToList();

            return View(allCards);
        }

        [Authorize]
        public HttpResponse AddToCollection(string cardId)
        {
            var card = this.data.Cards.FirstOrDefault(c => c.Id == cardId);

            if (card == null)
            {
                return Error($"Card with ID: '{cardId}' does not exist.");
            }

            var userCard = this.data.UserCards.FirstOrDefault(uc => uc.UserId == User.Id && uc.CardId == cardId);
            
            // if already exists
            if (userCard != null)
            {
                return Error("Usercard already exists for the user.");
            }

            var newUserCard = new UserCard
            {
                UserId = this.User.Id,
                CardId = cardId
            };

            this.data.UserCards.Add(newUserCard);

            this.data.SaveChanges();

            return Redirect("/Cards/All");
        }

        [Authorize]
        public HttpResponse Collection()
        {
            var userCards = this.data.UserCards
                .Where(uc => uc.UserId == User.Id)
                .Select(uc => new AllCardsViewModel
                {
                    Id = uc.Card.Id,
                    Name = uc.Card.Name,
                    ImageUrl = uc.Card.ImageUrl,
                    Keyword = uc.Card.Keyword,
                    Attack = uc.Card.Attack,
                    Health = uc.Card.Health,
                    Description = uc.Card.Description
                })
                .ToList();

            return View(userCards);
        }

        [Authorize]
        public HttpResponse RemoveFromCollection(string cardId)
        {
            var userCard = this.data.UserCards.FirstOrDefault(uc => uc.CardId == cardId && uc.UserId == User.Id);

            if (userCard == null)
            {
                return Error($"User card with ID: '{cardId}' does not exist for this user.");
            }

            this.data.UserCards.Remove(userCard);

            this.data.SaveChanges();

            return Redirect("/Cards/Collection");
        }
    }
}
