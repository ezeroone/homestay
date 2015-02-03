using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eResorts.Models
{
    public class PlaceModel
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
         [Required]
        public string PlaceName { get; set; }
         [Required]
        public decimal Distance { get; set; }
         [Required]
         public decimal Latitude { get; set; }
         [Required]
         public decimal Longitude { get; set; }
         [Required]
        public string TimeTake { get; set; }
         
        public string TravelBy { get; set; }
        [Required]
        public int Travel { get; set; }
        public List<PlaceModel> Places { get; set; }
        public int PoiId { get; set; }

        public string Description { get; set; }
    }
}