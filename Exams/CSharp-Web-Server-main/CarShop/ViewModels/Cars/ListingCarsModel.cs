namespace CarShop.ViewModels.Cars
{
    public class ListingCarsModel
    {
        public string Id { get; init; }
        public string ImgUrl { get; init; }
        public string Model { get; init; }
        public int Year { get; init; }
        public string Plate { get; init; }
        public int RemainingIssues { get; init; }
        public int FixedIssues { get; init; }
    }
}
