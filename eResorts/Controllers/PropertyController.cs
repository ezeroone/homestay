using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eResorts.Models;
using eZeroOne.Common;
using eZeroOne.Domain;
using eZeroOne.MailService;
using eZeroOne.Service.Property;
using eZeroOne.Service.Repository;
using eZeroOne.Service.Users;
using PointOfInterest = eResorts.Models.PointOfInterest;

namespace eResorts.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHotelService _hotelSvc;
        private readonly IUserService _userService;
        // GET: /Property/
        public PropertyController(IRepository repository, IUnitOfWork unitOfWork)
        {
            _hotelSvc = new HotelService(repository, unitOfWork);
            _userService = new UserService(repository, unitOfWork);
        }

        public ActionResult Search()
        {
            PreLoadData();
            return View();
        }
        public ActionResult SearchResults()
        {
            return View();
        }
        public ActionResult Properties()
        {
            return View();
        }
        public ActionResult PropertyDetail(int id)
        {
            var model = new PropertyModel
                {
                    Images = new List<ImageModel>(),
                    Features = new List<Features>(),
                    Places = new List<PlaceModel>(),
                    Meals=new List<DiningModel>(),
                    PoIs=new List<PointOfInterest>(),
                    Translators=new List<TranslatorModel>(),
                    DistrictImages = new List<ImageModel>()
                };

            var property = _hotelSvc.GetPropertyDetail(id);
            var images = _hotelSvc.Images(id);
            if (property != null)
            {
                model.Id = property.Id;
                model.LocationUrl = "/Property/GetPropertylocation?id=" + id;
                model.Name = property.Name;
                model.NoOfRooms = property.NoOfRooms;
                model.Description = property.Description;
                model.DisplayPrice = property.DisplayPrice;
                model.ClientName = "";
                model.ContactPerson1 = property.ContactPerson1;
                model.MobilePrimary = property.MobilePrimary;
                model.MobileSecondary = property.MobileSecondary;
                model.LandLinePrimary = property.LandLinePrimary;
                model.LandLineSecondary = property.LandLineSecondary;
                model.ContactPerson2 = property.ContactPerson2;
                model.Adderss = property.Adderss;
                model.Street = property.Street;
                model.City = property.City;
                model.FeatureList = property.Features;
                model.PoiList = property.Interests;
                model.Squarefeet = (decimal)property.Squarefeet;
                model.MainCityName = _hotelSvc.GetDistrict((int)property.MainCity).City;

                model.ImageName = !string.IsNullOrWhiteSpace(property.ImageName)
                                    ? "/PropertyImages/" + property.Id + "/thumbs/contact/" + property.ImageName
                                    : "/Content/img/defaultphoto.gif";

                model.MealImage = !string.IsNullOrWhiteSpace(property.MealImage)
                                    ? "/PropertyImages/MealImages/" + property.Id + "/" + property.MealImage
                                    : "";

                model.PoliceReport = !string.IsNullOrWhiteSpace(property.PoliceReport)
                                   ? "/PropertyImages/PoliceReport/" + property.Id + "/" + property.PoliceReport
                                   : "";

                model.GSReport = !string.IsNullOrWhiteSpace(property.GSReport)
                                 ? "/PropertyImages/GSReport/" + property.Id + "/" + property.GSReport
                                 : "";

                model.AboutOwner = property.AboutOwner??"N/A";
                var firstOrDefault = images.FirstOrDefault();
                if (firstOrDefault != null) model.DefaultImageUrl = firstOrDefault.ImagePath;


            }

            var plsColl = new List<PlaceModel>
                {
                    new PlaceModel
                        {
                            PlaceName = "Culture",
                            PoiId = 1,
                            Places=new List<PlaceModel>()
                        },
                        new PlaceModel
                        {
                            PlaceName = "Nature",
                            PoiId = 2,
                             Places=new List<PlaceModel>()
                        },
                        new PlaceModel
                        {
                            PlaceName = "Time & People",
                            PoiId = 3,
                             Places=new List<PlaceModel>()
                        }
                };

            foreach (var item in plsColl)
            {
                var places = _hotelSvc.GetPlaces(id).Where(q => q.PoiId == item.PoiId);
                foreach (var p in places)
                {
                    var travelBy = _hotelSvc.GetTravelDetail(p.TravelBy);
                    var pl = new PlaceModel
                    {
                        Id = p.Id,
                        PropertyId = p.PropertyId,
                        PlaceName = p.PlaceName,
                        Latitude = p.Lat,
                        Longitude = p.Long,
                        Distance = p.Distance,
                        TimeTake = p.TimeTake,
                        PoiId = p.PoiId,
                        TravelBy = travelBy == null ? "" : travelBy.Name,
                        Description=p.Description
                    };

                    item.Places.Add(pl);
                }
            }
            model.Places = plsColl;

        
            var meals = _hotelSvc.GetMeals(id);
            var cuisines = _hotelSvc.GetCuisines().OrderBy(q=>q.DisplayOrder);
            foreach (var cuisine in cuisines)
            {
                var _meals = meals.Where(q => q.FoodId == cuisine.Id).ToList();
                if (_meals.Any())
                {
                    var mealMod = new DiningModel
                    {
                        Meals = new List<DiningModel>(),
                        MealType = cuisine.FoodType,
                        FoodId = cuisine.Id,
                        Imagepath = !string.IsNullOrWhiteSpace(cuisine.ImageName) ? "/PropertyImages/DiningImages/" + cuisine.Id + "/" + cuisine.ImageName : "/Content/img/dining.jpg"
                    };
                    foreach (var m in _meals)
                    {
                        var meal = new DiningModel
                        {
                            Id = m.Id,
                            PropertyId = m.PropertyId,
                            MealType = m.MealType,
                            Food = cuisine.FoodType,
                            Price = m.Price,
                          

                        };
                        mealMod.Meals.Add(meal);
                    }

                    model.Meals.Add(mealMod);
                }
            }

           
            foreach (var i in images)
            {
                model.Images.Add(new ImageModel
                    {
                        Id = i.Id,
                        DefaultImage = (bool) i.DefaultImage,
                        ImageDescription = i.ImageDescription,
                        ImagePath = "/PropertyImages/" + id + "/thumbs/" + i.ImagePath,
                        PropertyId = i.PropertyId
                    });
            }

            var district = property.MainCity;
            if (district != null)
            {
                var distimages = _hotelSvc.DistrictImages(district.Value);
                foreach (var i in distimages)
                {
                    model.DistrictImages.Add(new ImageModel
                        {
                            Id = i.Id,
                            ImageDescription = i.Description,
                            ImagePath = "~/DistrictImages/" + district.Value + "/" + i.ImageName,
                            PropertyId = i.DistrictId
                        });
                }
            }
            var translators = _hotelSvc.GetTranslators(id);
            foreach (var t in translators)
            {
                model.Translators.Add(
                    new TranslatorModel
                        {
                            Id=t.Id,
                            Name=t.Name,
                            Landline=t.Landline,
                            Mobile=t.Mobile,
                            Language=t.Language,
                            ImageName = !string.IsNullOrWhiteSpace(t.ImageName) ? "/PropertyImages/" + t.PropertyId + "/thumbs/translator/" + t.Id + "/" + t.ImageName : "/Content/img/defaultphoto.gif"
                        });
            }

            ViewBag.ReviewComments = GetReviewComments(id);

            return View(model);
        }

        public ActionResult SearchHotels()
        {
            var cities = _hotelSvc.GetLocationList();
            ViewBag.LocationList = new SelectList(cities, "Id", "City");

            string[] radiusList = new string[] { "5", "10", "15", "20" };
            SelectList list = new SelectList(radiusList);
            ViewBag.RadiusList = list;

            var acmTypes = _hotelSvc.GetAccomodationTypeList();
            ViewBag.AccomodationTypeList = new SelectList(acmTypes, "Id", "Name");

            var acmStandards = _hotelSvc.GetAccomodationStandardList();
            ViewBag.AccomodationStandardList = new SelectList(acmStandards, "Id", "Name");

            string[] roomsList = new string[] { "1", "2", "3", "4" };
            ViewBag.RoomList = new SelectList(roomsList);

            var poiList = _hotelSvc.GetPointOfInterestList().ToList();
            var pl = poiList.Select(item => new eResorts.Models.PointOfInterest {Id = item.Id, Name = item.Name}).ToList();

            ViewBag.POTList = pl;

            var featureList = _hotelSvc.GetFeatureList().ToList();
            var fl = featureList.Select(item => new Features {Id = item.Id, Name = item.Name}).ToList();
            ViewBag.FeatureList = fl;

            return View("SearchHotels");
        }

        public ActionResult GetFilteredHotelData(HotelSearchModel filter,string poiList,string featureList)
       {
           var result = new List<Property>();
           var query = filter.Location;
            if (!string.IsNullOrWhiteSpace(query))
                query = query.ToLower();

           var desig = _hotelSvc.GetDesignation(filter.DesignationId);
           
           result = _hotelSvc.GetHotelsList().ToList().Where(q=>q.IsApproved).ToList();

           if (filter.AccomodationTypeId > 0)
               result = result.Where(q => q.AccommodationType == filter.AccomodationTypeId).ToList();

           if (filter.AccomodationStandardId > 0)
               result = result.Where(q => q.AccommodationStandard == filter.AccomodationStandardId
                      ).ToList();
            
           if (!string.IsNullOrWhiteSpace(query))
               result = result.Where(q => (q.Description != null && q.Description.ToLower().ToLower().Contains(query))
                   || (q.Name != null && q.Name.ToLower().Contains(query))
                   || (q.Adderss != null && q.Adderss.ToLower().Contains(query))
                   || (q.Street != null && q.Street.ToLower().Contains(query))
                   || (q.City != null && q.City.ToLower().Contains(query))).ToList();


           if (desig != null)
           {
               result = result.Where(q => (q.OwnerDesignation != null && q.OwnerDesignation.Contains(desig.Name))).ToList();
           }
           if (filter.LocationId > 0)
               result = result.Where(q => q.MainCity == filter.LocationId).ToList();

           if (filter.No_ofRooms > 0)
               result = result.Where(q => q.NoOfRooms >= filter.No_ofRooms
            ).ToList();


           if (filter.PriceTo != 0)
               result = result.Where(q => (q.DisplayPrice >= filter.PriceFrom && q.DisplayPrice <= filter.PriceTo)).ToList();
         

           var result1 = result;

           string[] pois = poiList.Split(',');
           int flag1 = 0;
           //for (int i = 0; i < pois.Count(); i++)
           //{
           //    var poiresult = result1.Where(q => q.Interests.Contains(pois[i].ToLower())).ToList();
           //    if (poiresult.Any())
           //    {
           //        result1 = poiresult;
           //        flag1 = flag1 + 1;
           //    }
           //}

           //result = flag1 == pois.Count() ? result1 : new List<Property>();

           var result2 = result;

           string[] features = featureList.Split(',');
           int flag2 = 0;
           for (int k = 0; k < features.Count(); k++)
           {
               if (!string.IsNullOrWhiteSpace(features[k]))
               {
                   var ftresult = result2.Where(q => q.Features.Contains(features[k].ToLower())).ToList();
                   if (ftresult.Any())
                   {
                       result2 = ftresult;
                       flag2 = flag2 + 1;
                   }
               }
              
           }

            result = result2;// flag2 == features.Count() ? result2 : new List<Property>(); 

            var cityObj = _hotelSvc.GetLocationList().Where(x => x.Id == filter.LocationId).FirstOrDefault();

            //use this for load the result partial view
            TempData["FilteredResults"] = result;
            return Json(new { city = cityObj, hotels = result, radius = filter.Radius == 52 ? 100 : filter.Radius });
        }
        public ActionResult GetPropertylocation(int propertId)
        {
            var result = new List<PlaceModel>();

            var places = _hotelSvc.GetPlaces(propertId);
            foreach (var p in places)
            {
                result.Add(new PlaceModel
                {
                    Id = p.Id,
                    PropertyId = p.PropertyId,
                    PlaceName = p.PlaceName,
                    Latitude = p.Lat,
                    Longitude = p.Long,
                    Distance = p.Distance,
                    TimeTake = p.TimeTake,
                    TravelBy = ""//p.TravelBy

                });
            }

            var property = _hotelSvc.GetPropertyDetail(propertId);
            var cityObj = new LatLong
                {
                    Latitude = (decimal) property.Latitude,
                    Name=property.Name,
                    VideoUrl=!string.IsNullOrWhiteSpace(property.VideoUrl)?property.VideoUrl:"",
                    Longitude=(decimal) property.Longitude
                };

            var mainCity = _hotelSvc.GetMainCity((int) property.MainCity);
            var districtObj = new LatLong
            {
                Latitude = mainCity!=null?(decimal) mainCity.Latitude:0,
                Name = mainCity != null ? mainCity.City : property.Name,
                Longitude = mainCity!=null?(decimal)mainCity.Longitude:0
            };

            return Json(new { city = cityObj, hotels = result, radius = 5, district = districtObj });
        }

        private void PreLoadData()
        {
            var cities = _hotelSvc.GetLocationList();
            ViewBag.LocationList = new SelectList(cities, "Id", "City");

            var designation = _hotelSvc.GetDesignationList();
            ViewBag.Designations = new SelectList(designation, "Id", "Name");


            var radius = new List<NameValue>
                {
                    new NameValue
                        {
                            Id = 0,
                            Name = "This area only"
                        },
                     new NameValue
                        {
                            Id = 1,
                            Name = "Within 1 Km"
                        },
                  new NameValue
                        {
                            Id = 2,
                            Name = "Within 2 Km"
                        },
                    new NameValue
                        {
                            Id = 3,
                            Name = "Within 3 Km"
                        },
                   new NameValue
                        {
                            Id = 4,
                            Name = "Within 4 Km"
                        },
                    new NameValue
                        {
                            Id = 5,
                            Name = "Within 5 Km"
                        },
                    new NameValue
                        {
                            Id = 10,
                            Name = "Within 10 Km"
                        }
                        ,
                    new NameValue
                        {
                            Id = 15,
                            Name = "Within 15 Km"
                        }
                        ,
                    new NameValue
                        {
                            Id = 20,
                            Name = "Within 20 Km"
                        }
                        ,
                    new NameValue
                        {
                            Id = 25,
                            Name = "Within 25 Km"
                        },
                    new NameValue
                        {
                            Id = 30,
                            Name = "Within 30 Km"
                        },
                    new NameValue
                        {
                            Id = 40,
                            Name = "Within 40 Km"
                        },
                    new NameValue
                        {
                            Id = 50,
                            Name = "Within 50 Km"
                        },
                    new NameValue
                        {
                            Id = 51,
                            Name = "Above 50 Km"
                        },
                   new NameValue
                        {
                            Id = 52,
                            Name = "--- Any ---"
                        },
                };

            //string[] radiusList = new string[] { "5", "10", "15", "20" };
            SelectList list = new SelectList(radius, "Id", "Name");
            ViewBag.RadiusList = list;

            var properties = _hotelSvc.GetHotelsList().ToList().Where(q => q.IsApproved).ToList();
            var acmTypes = _hotelSvc.GetAccomodationTypeList();

            var accTypelist = new List<AccommodationType>();

            foreach (var t in acmTypes)
            {
                if (t.Id != 0)
                {
                    var count = properties.Count(q => q.AccommodationType == t.Id);
                    var name = t.Name + " (" + count + ")";
                    accTypelist.Add(new AccommodationType
                    {
                        Id = t.Id,
                        Name = name
                    });
                }
                else
                {
                    var name = t.Name;
                    accTypelist.Add(new AccommodationType
                    {
                        Id = t.Id,
                        Name = name
                    });
                }

            }

            ViewBag.AccomodationTypeList = new SelectList(accTypelist, "Id", "Name");

            //count based on district
            var mainCitylist = new List<MainCity>();

            foreach (var t in cities)
            {
                if (t.Id != 0)
                {
                    var count = properties.Count(q => q.MainCity == t.Id);
                    var name = t.City + " (" + count + ")";
                    mainCitylist.Add(new MainCity
                    {
                        Id = t.Id,
                        City = name
                    });
                }
                //else
                //{
                //    var name = t.City;
                //    mainCitylist.Add(new MainCity
                //    {
                //        Id = t.Id,
                //        City = name
                //    });
                //}

            }

            mainCitylist = mainCitylist.OrderBy(o => o.City).ToList();
            var city = new MainCity
            {
                Id = 0,
                City = " Select the location"

            };
            mainCitylist.Insert(0, city);

            ViewBag.LocationList = new SelectList(mainCitylist, "Id", "City");

            var acmStandards = _hotelSvc.GetAccomodationStandardList();
            ViewBag.AccomodationStandardList = new SelectList(acmStandards, "Id", "Name");

            //string[] roomsList = new string[] { "1", "2", "3", "4" };
            var rooms = new List<NameValue>
                {
                    new NameValue
                        {
                            Id = 0,
                            Name = "---Any---"
                        },
                    new NameValue
                        {
                            Id = 1,
                            Name = "1"
                        },
                    new NameValue
                        {
                            Id = 2,
                            Name = "2"
                        },
                    new NameValue
                        {
                            Id = 3,
                            Name = "3"
                        }
                        ,
                    new NameValue
                        {
                            Id = 4,
                            Name = "4"
                        }
                        ,
                    new NameValue
                        {
                            Id = 5,
                            Name = "5"
                        }
                        ,
                    new NameValue
                        {
                            Id = 6,
                            Name = "6"
                        },
                    new NameValue
                        {
                            Id = 7,
                            Name = "7 +"
                        },
                  
                };


            ViewBag.RoomList = new SelectList(rooms, "Id", "Name");

            var poiList = _hotelSvc.GetPointOfInterestList().ToList();
            var pl = poiList.Select(item => new eResorts.Models.PointOfInterest { Id = item.Id, Name = item.Name }).ToList();

           // ViewBag.POTList = new SelectList(poiList, "Id", "Name");
            ViewBag.POTList = pl;

            var featureList = _hotelSvc.GetFeatureList().ToList();
            var fl = featureList.Select(item => new Features { Id = item.Id, Name = item.Name }).ToList();

           //FeatureGroup

            var featureGroup = new List<FeatureGroup>();

            var groups = _hotelSvc.FeatureCategories().OrderBy(o=>o.Name).ToList();
            foreach (var featureCategory in groups)
            {
                var feaList = new FeatureGroup
                    {
                        Features = new List<Features>(),
                        Id = featureCategory.Id,
                        GroupName = featureCategory.Name
                    };

                var _features=new List<Features>();
                foreach (var fea in featureList.Where(q=>q.GroupId==featureCategory.Id))
                {
                    _features.Add(new Features
                        {
                            Id=fea.Id,
                            Name=fea.Name,
                            IsChecked = false
                        });
                }

                feaList.Features = _features;
                featureGroup.Add(feaList);
                
            }
            //ViewBag.FeatureList = new SelectList(featureList, "Id", "Name");

            ViewBag.FeatureGroup = featureGroup;
            ViewBag.FeatureList = fl;

            var minPrice = new List<NameValue>
                {
                    new NameValue
                        {
                            Id = 0,
                            Name = "Min"
                        }
                };
            for (int i = 100; i < 50000; i = i + 100)
            {
                minPrice.Add(new NameValue
                {
                    Id = i,
                    Name = Convert.ToString(i)
                });
                //i = i + 100;
            }

            var maxPrice = new List<NameValue>
                {
                    new NameValue
                        {
                            Id = 0,
                            Name = "Max"
                        }
                };
            for (int i = 200; i < 80000; i = i + 100)
            {
                maxPrice.Add(new NameValue
                {
                    Id = i,
                    Name = Convert.ToString(i)
                });
                //i = i + 100;
            }


            ViewBag.MinPriceList = new SelectList(minPrice, "Id", "Name");
            ViewBag.MaxPiceList = new SelectList(maxPrice, "Id", "Name");

        }
        public ActionResult SearchResult(int? page)
        {
            var model = new List<PropertyModel>();
            ViewBag.Records = 0;
            ViewBag.ShowPage = page ?? 1;

            if (TempData["FilteredResults"] != null)
            {
                var properties = TempData["FilteredResults"] as IEnumerable<eZeroOne.Domain.Property>;
                if (properties != null && properties.Any())
                {
                    ViewBag.Records = properties.Count();

                    foreach (var p in properties)
                    {
                        var property = new PropertyModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            DisplayPrice = p.DisplayPrice,
                            Discount = p.Discount ?? 0,
                            NoOfRooms = p.NoOfRooms,
                            PropertyUrl = string.Format("/{0}/{1}", "Property", "PropertyDetail?id=" + p.Id),
                            Images = new List<ImageModel>(),
                            Features = new List<Features>(),
                            Places = new List<PlaceModel>(),
                            Meals = new List<DiningModel>(),
                            PoIs = new List<PointOfInterest>(),
                            ContactPerson1 = p.ContactPerson1,
                            MobilePrimary = p.MobilePrimary,
                            MobileSecondary = p.MobileSecondary,
                            LandLinePrimary = p.LandLinePrimary,
                            LandLineSecondary = p.LandLineSecondary,
                            ContactPerson2 = p.ContactPerson2,
                            Adderss = p.Adderss,
                            Street = p.Street,
                            City = p.City,
                            Squarefeet = (decimal)p.Squarefeet,
                            FeatureList=p.Features,
                            PoiList=p.Interests,
                            MainCityName = _hotelSvc.GetDistrict((int)p.MainCity).City
                        };

                        var images = _hotelSvc.Images(p.Id).FirstOrDefault();
                        property.DefaultImageUrl = "/Content/img/demo/apt/apt1.jpg";
                        if (images != null)
                        {
                            var path = "/PropertyImages/" + p.Id + "/thumbs/" + images.ImagePath;
                            property.DefaultImageUrl = path;
                        }

                        model.Add(property);
                    }

                }

            }
            return PartialView("_SearchResults", model);
        }
        public ActionResult Booking(int propertyId)
        {
            var model = new BookingModel();
            var property = _hotelSvc.GetPropertyDetail(propertyId);
            var visitorId = 0;
            if (User.Identity.IsAuthenticated)
            {
                var user = _userService.GetUser(User.Identity.Name, 1);
                if (user != null)
                    visitorId = user.UserId;
            }
            if (property != null)
            {
                model.PropertyId = property.Id;
                model.ClientId = (int) property.ClientId;
                model.VisitorId = visitorId;

                model.Name = property.Name;
                model.NoOfRooms = property.NoOfRooms;
                model.Description = property.Description;
                model.DisplayPrice = property.DisplayPrice;
                
                model.ContactPerson1 = property.ContactPerson1;
                model.MobilePrimary = property.MobilePrimary;
                model.MobileSecondary = property.MobileSecondary;
                model.LandLinePrimary = property.LandLinePrimary;
                model.LandLineSecondary = property.LandLineSecondary;
                model.ContactPerson2 = property.ContactPerson2;
                model.Adderss = property.Adderss;
                model.Street = property.Street;
                model.City = property.City;
                model.Latitude = (decimal) property.Latitude;
                model.Longitude = (decimal) property.Longitude;
                model.Squarefeet = (decimal)property.Squarefeet;
                model.BookingFrom = DateTime.Now.ToString("dd/MM/yyyy");
                model.BookingTo = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            }
            return View(model);
        }

        public JsonResult Book(int propertyId, string  frmDate, string toDate,decimal price,int days)
        {
            var property = _hotelSvc.GetPropertyDetail(propertyId);
            var visitorId = 0;
            if (User.Identity.IsAuthenticated)
            {
                var user = _userService.GetUser(User.Identity.Name, 1);
                if (user != null)
                {
                    visitorId = user.UserId;
                    if (property != null && visitorId > 0)
                    {
                        var booking = new Booking
                        {
                            PropertyId = propertyId,
                            VisitorId = visitorId,
                            CustomerId = (int)property.ClientId,
                            DateFrom = Convert.ToDateTime(frmDate),
                            DateTo = Convert.ToDateTime(toDate),
                            Amount = price,
                            Discount = 0,
                            Tax = 0

                        };

                        var booked = _hotelSvc.MakeBooking(booking);
                        if (booked)
                        {
                            var url = "";
                            try
                            {
                                var clientName = _userService.GetUser((int)property.ClientId).FirstName;

                                EmailService.SendbookingEmail(url, user.Email, string.Format("{0} {1}", user.FirstName, user.LastName), clientName, propertyId, frmDate,toDate,days,price.ToString());

                            }
                            catch (Exception)
                            {
                                
                            }
                         
                        }

                    }
                }

            }
           
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult PreBook(int propertyId, string frmDate, string toDate)
        {
            var property = _hotelSvc.GetPropertyDetail(propertyId);
            var retString = "proceed";
            if (User.Identity.IsAuthenticated)
            {
                var user = _userService.GetUser(User.Identity.Name, 1);
                if (user != null)
                {
                    
                    if (property != null && user.UserId > 0)
                    {
                        var r = _hotelSvc.CheckBookingDates(propertyId, Convert.ToDateTime(frmDate), Convert.ToDateTime(toDate));
                        if(r)
                            retString = "exist";
                    }

                }

            }
            else
            {
                retString = "login";
            }

            return Json(retString, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BookingSuccess()
        {
            return View();
        }

        public JsonResult GetFileredAccomodationType(int distriictId)
        {

            var properties = _hotelSvc.GetHotelsList().ToList().Where(q => q.IsApproved).ToList();
            var acmTypes = _hotelSvc.GetAccomodationTypeList();

            var accTypelist = new List<AccommodationType>();

            if (distriictId == 0)
                foreach (var t in acmTypes)
                {
                    if (t.Id != 0)
                    {
                        var count = properties.Count(q => q.AccommodationType == t.Id);
                        var name = t.Name + " (" + count + ")";
                        accTypelist.Add(new AccommodationType
                        {
                            Id = t.Id,
                            Name = name
                        });
                    }
                    else
                    {
                        var name = t.Name;
                        accTypelist.Add(new AccommodationType
                        {
                            Id = t.Id,
                            Name = name
                        });
                    }

                }
            else
            {
                foreach (var t in acmTypes)
                {
                    if (t.Id != 0)
                    {
                        var count = properties.Count(q => q.MainCity == distriictId && q.AccommodationType == t.Id);
                        var name = t.Name + " (" + count + ")";
                        accTypelist.Add(new AccommodationType
                        {
                            Id = t.Id,
                            Name = name
                        });
                    }
                    else
                    {
                        var name = t.Name;
                        accTypelist.Add(new AccommodationType
                        {
                            Id = t.Id,
                            Name = name
                        });
                    }

                }
            }
            ViewBag.AccomodationTypeList = new SelectList(accTypelist, "Id", "Name");


            return Json(accTypelist.OrderBy(o => o.Name).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFileredAccomodationStandard(int distriictId, int accoType)
        {
            var properties = _hotelSvc.GetHotelsList().ToList().Where(q => q.IsApproved).ToList();
            //var acmTypes = _hotelSvc.GetAccomodationTypeList();
            var acmStandards = _hotelSvc.GetAccomodationStandardList();
            var acmStandardlist = new List<AccommodationStandard>();

            if (distriictId == 0 && accoType > 0)
                foreach (var t in acmStandards)
                {
                    if (t.Id != 0)
                    {
                        var count = properties.Count(q => q.AccommodationType == accoType && q.AccommodationStandard == t.Id);
                        var name = t.Name + " (" + count + ")";
                        acmStandardlist.Add(new AccommodationStandard
                        {
                            Id = t.Id,
                            Name = name
                        });
                    }
                    else
                    {
                        var name = t.Name;
                        acmStandardlist.Add(new AccommodationStandard
                        {
                            Id = t.Id,
                            Name = name
                        });
                    }

                }
            else
            {
                if (accoType > 0)
                    foreach (var t in acmStandards)
                    {
                        if (t.Id != 0)
                        {
                            var count = properties.Count(q => q.MainCity == distriictId && q.AccommodationType == accoType && q.AccommodationStandard == t.Id);
                            var name = t.Name + " (" + count + ")";
                            acmStandardlist.Add(new AccommodationStandard
                            {
                                Id = t.Id,
                                Name = name
                            });
                        }
                        else
                        {
                            var name = t.Name;
                            acmStandardlist.Add(new AccommodationStandard
                            {
                                Id = t.Id,
                                Name = name
                            });
                        }

                    }
                else
                {
                    acmStandardlist = acmStandards.ToList();
                }
            }


            return Json(acmStandardlist.OrderBy(o => o.Name).ToList(), JsonRequestBehavior.AllowGet);
        }


          
        public JsonResult GetFileredOccupation(int distriictId)
        {
            var properties = _hotelSvc.GetHotelsList().ToList().Where(q => q.IsApproved && q.MainCity == distriictId).ToList();
            var designations = _hotelSvc.GetDesignationList();
            //  ViewBag.Designations = new SelectList(designation, "Id", "Name");
            //var acmTypes = _hotelSvc.GetAccomodationTypeList();
           // var acmStandards = _hotelSvc.GetAccomodationStandardList();
           // var designationList = new List<Designation>();
            var desic = new Designation
            {
                Id = 0,
                Name = "--- Any --- "

            };
            
            var enumerable = designations as Designation[] ?? designations.ToArray();
            if (distriictId > 0)

            {
                var desigs = (from d in enumerable
                              join p in properties on d.Name equals p.OwnerDesignation
                              select d).Distinct().ToList();

                var l = desigs.OrderBy(o => o.Name).ToList();
                l.Insert(0, desic);
                return Json(l, JsonRequestBehavior.AllowGet);
            }
                //foreach (var t in designations)
                //{
                //    if (t.Id != 0)
                //    {
                //        var count = properties.Count(q => q.OwnerDesignation == accoType && q.AccommodationStandard == t.Id);
                //        var name = t.Name + " (" + count + ")";
                //        acmStandardlist.Add(new AccommodationStandard
                //        {
                //            Id = t.Id,
                //            Name = name
                //        });
                //    }
                //    else
                //    {
                //        var name = t.Name;
                //        acmStandardlist.Add(new AccommodationStandard
                //        {
                //            Id = t.Id,
                //            Name = name
                //        });
                //    }

                //}


            return Json(enumerable.OrderBy(o => o.Name).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddReviewComment(string userName,string email,string comment,int propertyId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userEmail = User.Identity.Name;
                var user = _userService.GetUser(userEmail, 1);
                if (user != null)
                {
                    _hotelSvc.AddReviewComments(user.UserId, comment, propertyId);
                }
            }
            
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReviewComment(int id)
        {
            var r = new ReviewModel();
            var result = _hotelSvc.GetReview(id);
            if (result != null)
            {
                var user = _userService.GetUser(result.VisitorId);
                if (user != null)
                {
                    r = new ReviewModel
                    {
                        Id = result.Id,
                        Name = string.Format("{0} {1}", user.FirstName, user.LastName),
                        Email = user.Email,
                        Comment = result.Comments
                    };

                }
            }
            
            return Json(r, JsonRequestBehavior.AllowGet);
        }

       public IEnumerable<ReviewModel> GetReviewComments(int propertyId)
       {
           var comments = new List<ReviewModel>();

           var results = _hotelSvc.GetReviewComments(propertyId);
           foreach (var visitorReview in results)
           {
               var user = _userService.GetUser(visitorReview.VisitorId);
               if(user!=null)
               {
                   var r = new ReviewModel
                   {
                       Id =visitorReview.Id,
                       Name= string.Format("{0} {1}",user.FirstName,user.LastName),
                       Email=user.Email,
                       Comment=visitorReview.Comments
                   };
                   comments.Add(r);
               }
            
           }
           return comments;
       }
        public ActionResult Slidertest()
        {
          
            return View();
        }

    }
}
