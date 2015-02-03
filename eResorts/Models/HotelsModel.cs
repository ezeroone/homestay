using System.Collections.Generic;

namespace eResorts.Models
{
    
    public class HotelSearchModel
    {
        public int LocationId { get; set; }
        public int DesignationId { get; set; }
        public string Location { get; set; }
        public int  Radius { get; set; }
        public int AccomodationTypeId { get; set; }
        public string AccomodationType { get; set; }
        public int AccomodationStandardId { get; set; }
        public string AccomodationStandard { get; set; }
        public int CountryId { get; set; }
        public int No_ofRooms { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public int PoI { get; set; }
        public int Feature { get; set; }
        public List<PointOfInterest> PointOfInterest { get; set; }
        public List<Features> Features { get; set; }

        public int LocationId2 { get; set; }
        public int DesignationId2 { get; set; }

        
    }
}