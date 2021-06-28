using BattleCards.Data;
using BattleCards.Models;
using BattleCards.Services;
using BattleCards.ViewModels.Cards;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IValidator validator;
        public CardsController(ApplicationDbContext context, IValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var cards = this.context.Cards.Select(c => new ListingViewCardsModel
            {
                ImageUrl = c.ImageUrl,
                Name = c.ImageUrl,
                Type = c.Keyword,
                Attack = c.Attack,
                Health = c.Health,
                Description = c.Description
            })
             .ToList();

            return View(cards);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(CreateCardViewModel model)
        {
            var errorsModel = this.validator.ValidateCard(model);
            if (errorsModel.Any())
            {
                return Redirect("/Cards/Add");
            }

            var card = new Card
            {
                Name = model.Name,
                ImageUrl = "f",//model.ImageUrl,
                Keyword = model.Keyword,
                Attack = model.Attack,
                Health = model.Health,
                Description = model.Description
            };

            this.context.Cards.Add(card);

            this.context.SaveChanges();

            return Redirect("/Cards/All");
        }

        [Authorize]
        public HttpResponse Collection()
        {
            var userId = this.User.Id;

            var collection = this.context.Cards
                .Where(c => c.Users.Any(u => u.Id == userId))
                .Select(c => new ListingViewCardsModel
                {
                    Id = userId,
                    ImageUrl = c.ImageUrl,
                    Name = c.Name,
                    Type = c.Keyword,
                    Attack = c.Attack,
                    Health = c.Health,
                    Description = c.Description
                })
                .Distinct()
                .ToList();

            return View(collection);
        }

        [Authorize]
        public HttpResponse AddToCollection(string cardId)
        {
            var user = this.context.Users
                 .FirstOrDefault(u => u.Id == this.User.Id);

            var cardToAdd = this.context.Cards.FirstOrDefault(c => c.Id == cardId);

            user.Cards.Add(cardToAdd);

            return Redirect("/Cards/All");
        }

        [Authorize]
        public HttpResponse RemoveToCollection(string cardId)
        {
            var cardToRemove = this.context.Cards.FirstOrDefault(c => c.Id == cardId);
            if (cardToRemove == null)
            {
                return Error("Cannot remove the card in the collection.");
            }

            var user = this.context.Users
                .FirstOrDefault(u => u.Id == this.User.Id);

            user.Cards.Remove(cardToRemove);

            return Redirect("/Cards/Collection");
        }
    }
}
