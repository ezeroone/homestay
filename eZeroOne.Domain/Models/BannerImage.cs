namespace eZeroOne.Domain
{
    public class BannerImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImageDescription { get; set; }
        public string PlaceName { get; set; }
        public string Location { get; set; }
        public string PlaceUrl { get; set; }
        public string Distance { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public int DisplayOrder { get; set; }
    }
}