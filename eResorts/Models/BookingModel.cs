using System;
using System.Web.Mvc;

namespace eResorts.Models
{
    public class BookingModel
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AccommodationType { get; set; }
        public string AccommodationStandard { get; set; }
        public decimal DisplayPrice { get; set; }
        public int NoOfRooms { get; set; }
        public int NoOfDays { get; set; }
        public decimal Squarefeet { get; set; }
        public string  BookingFrom { get; set; }
        public string BookingTo { get; set; }
        [AllowHtml]
        public string TravellerRoute { get; set; }
        public string Adderss { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Fax { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public string LandLinePrimary { get; set; }
        public string MobilePrimary { get; set; }
        public string LandLineSecondary { get; set; }
        public string MobileSecondary { get; set; }
        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }


        public int ClientId { get; set; }
        public int VisitorId { get; set; }
    }
}