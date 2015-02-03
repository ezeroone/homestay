using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eResorts.Models
{
    public class PropertyModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string AccommodationType { get; set; }
        public string AccommodationStandard { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal DisplayPrice { get; set; }
        [Required]
        public int NoOfRooms { get; set; }
        [Required]
        public decimal Squarefeet { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public bool IsPromotion { get; set; }
        public string PromotionDescription { get; set; }
        public decimal PromotionAmount { get; set; }
        public decimal PromotionPercent { get; set; }
        public int PromotionType { get; set; }
        public bool ShowOnSliderBanner { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public string Country { get; set; }
        public int MainCity { get; set; }
        public string MainCityName { get; set; }
        [Required]
        public string Adderss { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Fax { get; set; }
        [Required]
        
        public string LandLinePrimary { get; set; }
        [Required]
       
        public string MobilePrimary { get; set; }
        public string LandLineSecondary { get; set; }
        public string MobileSecondary { get; set; }
        [Required]
        [Display(Name = "Contact Person")]
        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }
         [Required]
        public decimal Latitude { get; set; }
         [Required]
        public decimal Longitude { get; set; }
        public string RouteTo { get; set; }
        public string MainTown { get; set; }
        public decimal DistanceFromMainTown { get; set; }
        public string SearchKeys { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public List<PlaceModel> Places { get; set; }
        public List<DiningModel> Meals {get; set; }
        public List<PointOfInterest> PoIs { get; set; }
        public List<Features> Features { get; set; }
        public List<ImageModel> Images { get; set; }
        public List<ImageModel> DistrictImages { get; set; }
        public List<TranslatorModel> Translators { get; set; }
        public string PropertyUrl { get; set; }
        public string LocationUrl { get; set; }
        public string DefaultImageUrl { get; set; }

        public string FeatureList { get; set; }
        public string PoiList { get; set; }

        public int LocationId { get; set; }
        public int AccomodationTypeId { get; set; }
        public int AccomodationStandardId { get; set; }
        public int Designation { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string VideoUrl { get; set; }
        public string AddedBy { get; set; }

        public string ImageName { get; set; }
        public string AboutOwner { get; set; }
        public string MealImage { get; set; }

        public string PoliceReport { get; set; }
        public string GSReport { get; set; }
    }
}