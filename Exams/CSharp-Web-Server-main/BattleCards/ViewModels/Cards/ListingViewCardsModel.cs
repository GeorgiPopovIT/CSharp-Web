namespace BattleCards.ViewModels.Cards
{
    public class ListingViewCardsModel
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
    }
}
