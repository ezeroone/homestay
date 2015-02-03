using System;
using System.Collections.Generic;
using eZeroOne.Domain;

namespace eZeroOne.Service.Property
{
    public interface IHotelService
    {
        IEnumerable<MainCity> GetLocationList();
        IEnumerable<Designation> GetDesignationList();
        IEnumerable<AccommodationType> GetAccomodationTypeList();
        IEnumerable<AccommodationStandard> GetAccomodationStandardList();
        IEnumerable<PointOfInterest> GetPointOfInterestList();
        IEnumerable<Feature> GetFeatureList();
        IEnumerable<eZeroOne.Domain.Property> GetHotelsList();
        IEnumerable<PointOfInterest> GetHotelPOIList();
        IEnumerable<Feature> GetHotelFeaturesList();
        IEnumerable<Place> GetPlaces(int propertyId);
        Place GetPlace(int id);
        IEnumerable<Dining> GetMeals(int propertyId);
        IEnumerable<PropertyImage> Images(int propertyId);
        Domain.Property GetPropertyDetail(int id);
        Cuisine GetMealDetail(int foodId);
        TravelBy GetTravelDetail(int travelId);
        IEnumerable<Designation> GetDesignations();
        IEnumerable<Cuisine> GetCuisines();
        IEnumerable<TravelBy> GetTravelBy();
        string TravelByName(int id);
        IEnumerable<DistrictImage> DistrictImages(int id);
        bool AddDistrictImage(int Id, string filePath, string description);
        bool RemoveDistrictImage(int imageId, int districtId);
        MainCity GetDistrict(int id);

       bool AddIImage(int propertyId, string filePath, string description, bool isDefault);
       bool RemoveImage(int imageId, int propertyId);
       bool SaveProperty(Domain.Property property);
       bool SaveNearetPlaces(Domain.Place place);
       bool SaveTranslator(Domain.Translator translator);
       bool SaveDining(Domain.Dining dining);
       bool SavePois(string pois, int propertyId);
       bool SaveFeatures(string features, int propertyId);
        string DesignationName(int id);

        IEnumerable<Translator> GetTranslators(int propertyId);
        Dining GetFood(int id);
        bool UpdateFeatures(int id, string featureList);
        bool UpdatePois(int id, string poiList);
        Designation GetDesignation(int id);

        //update
        Domain.Property GetProperty(int id);
        bool UpdateProperty(Domain.Property property);
        bool UpdateNearetPlaces(Domain.Place model);
        Translator GetTranslatorById(int id);
        bool UpdateTranslator(Domain.Translator model);
        bool UpdateDining(Domain.Dining model);
        bool MakeBooking(Booking booking);

        //deleting
        bool DeleteProperty(int id);
        bool DeletePlaces(int id);
        bool DeleteFeatures(int id);
        bool DeletePois(int id);
        bool DeleteContacts(int id);
        bool DeleteMeals(int id);

        bool ApproveProperty(int id);
        bool RejectProperty(int id);

        //Save
        bool SaveMealType(int id, string name);
        bool DeleteCuisine(int id);

        bool SaveFeature(int id, int groupId, string name);
        bool SavePoi(int id, string name);
        bool SaveOccupation(int id, string name);
        bool RemoveOccupation(int id);
        bool SaveAccomodationType(int id, string name);
        bool SaveAccomodationStandard(int id, string name);
        bool SaveFeatureGroup(int id, string name);

        bool RemoveAccomodationType(int id);
        bool RemoveAccomodationStandard(int id);
        bool RemoveFeatureGroup(int id);


        bool PostProperty(int id);

        //ordering
        bool OrderFeature(int id, int orderId);
        bool OrderAccomodationtype(int id, int orderId);
        bool OrderAccomodationStandard(int id, int orderId);

        IEnumerable<FeatureCategory> FeatureCategories();

        Domain.MainCity GetMainCity(int id);

        bool SaveTravelBy(int id, string name);
        bool RemoveTravelBy(int id);
        bool SaveDistrictInfo(int id, string name);

        IEnumerable<BannerImage> GetSliderImages();
        BannerImage GetSliderImage(int id);
        bool AddBannerImage(string filePath, string place, string description, string loction, decimal lat, decimal lon);
        bool RemoveBannerImage(int imageId);

        bool OrderMealType(int id, int orderId);
        bool AddDiningImage(string filePath, int id);

        IEnumerable<YoutubeUrl> GetYoutubeUrls();
        //BannerImage GetYoutubeUrl(int id);
        int AddYoutubeUrl(string filePath, string description);
        bool RemoveYoutubeUrl(int id);

        bool UpdateBannerImage(int id, string filePath, string place, string description, string loction, decimal lat,
                               decimal lon);

        bool OrderSliderImage(int id, int orderId);
        bool CheckBookingDates(int propertyId, DateTime frmDate, DateTime toDate);

        //ordering
        bool OrderFeatureGroup(int id, int orderId);
        bool OrderTravelBy(int id, int orderId);
        bool OrderOccupation(int id, int orderId);

        bool AddRecommandation(string photoUrl, string name, string description, string email, string profession);
        bool UpdateRecommandation(int id, string photoUrl, string name, string description, string email, string profession);
        bool RemoveRecommandation(int id);

        Recommendation GetRecommandation(int id);
        IEnumerable<Recommendation> GetRecommandations();

        bool AddReviewComments(int visitorId, string comment,int propertyId );
        IEnumerable<VisitorReview> GetReviewComments(int propertyId);
        VisitorReview GetReview(int id);
        IEnumerable<VisitorReview> GetReviewComments();
    }
}
