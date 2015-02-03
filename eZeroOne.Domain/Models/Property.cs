//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eZeroOne.Domain
{
    using System;
    using System.Collections.Generic;
    
    public  class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AccommodationType { get; set; }
        public int AccommodationStandard { get; set; }
        public decimal DisplayPrice { get; set; }
        public int NoOfRooms { get; set; }
        public Nullable<decimal> Squarefeet { get; set; }
        public Nullable<System.DateTime> ActiveFrom { get; set; }
        public Nullable<System.DateTime> ActiveTo { get; set; }
        public Nullable<bool> IsPromotion { get; set; }
        public string PromotionDescription { get; set; }
        public Nullable<decimal> PromotionAmount { get; set; }
        public Nullable<int> PromotionType { get; set; }
        public Nullable<bool> ShowOnSliderBanner { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<int> Country { get; set; }
        public Nullable<int> MainCity { get; set; }
        public string MainCityName { get; set; }
        public string Adderss { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Fax { get; set; }
        public string LandLinePrimary { get; set; }
        public string MobilePrimary { get; set; }
        public string LandLineSecondary { get; set; }
        public string MobileSecondary { get; set; }
        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string RouteTo { get; set; }
        public string MainTown { get; set; }
        public Nullable<decimal> DistanceFromMainTown { get; set; }
        public string SearchKeys { get; set; }
        public string Features { get; set; }
        public string Interests { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string OwnerDesignation { get; set; }
        public string Location { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public string Email { get; set; }
        public string Website { get; set; }
        public string VideoUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsPosted { get; set; }

        public string ImageName { get; set; }
        public string AboutOwner { get; set; }

        public string MealImage { get; set; }
        public string PoliceReport { get; set; }
        public string GSReport { get; set; }
        
    }
}