using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eResorts.Models
{
    public class SliderModel
    {
        public int Id { get; set; }
        [Required]
        public string PlaceName { get; set; }
        [Required]
        public string Distance { get; set; }
        // [Required]
        public decimal Latitude { get; set; }
        //[Required]
        public decimal Longitude { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public string Location { get; set; }

        public string PlaceUrl { get; set; }
        public string ImageDescription { get; set; }
        public List<SliderModel> Sliders { get; set; }
        public int DisplayOrder { get; set; }

        
    }

    public class YoutubeUrlModel
    {
        public int Id { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public string Url { get; set; }

        public List<YoutubeUrlModel> Urls { get; set; }



    }

    public class ContactUs
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
       
        public string ContactNumber { get; set; }
        [Required]
        public string Comments { get; set; }



    }

    public class RecommendationModel
    {
        public int Id { get; set; }
        
         [Required]
        public string Name { get; set; }
         [Required]
         public string PhotoUrl { get; set; }
         [Required]
         public string Profession { get; set; }
         [Required]
         public string Email { get; set; }
         [Required]
         public string Comments { get; set; }

         public List<RecommendationModel> RecommendationList { get; set; }
    }
}