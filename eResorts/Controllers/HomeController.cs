using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using eResorts.Models;
using eZeroOne.Common;
using eZeroOne.Domain;
using eZeroOne.MailService;
using eZeroOne.Service.Property;
using eZeroOne.Service.Repository;
using eZeroOne.Service.Users;

namespace eResorts.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly IHotelService _hotelSvc;
        private readonly IUserService _userService;
        public HomeController(IRepository repository, IUnitOfWork unitOfWork)
        {
            _hotelSvc = new HotelService(repository, unitOfWork);
            _userService = new UserService(repository, unitOfWork);
        }

        public ActionResult Login()
        {

            return View();
        }

    [HttpGet]
        public ActionResult Prelogin()
        {
            return View();
           //return RedirectToAction("Index", "Home", new { area = "" });
        }

    [HttpPost]
    public ActionResult Prelogin(LoginModel model)
    {
        if (!string.IsNullOrWhiteSpace(model.Email) && model.Email == "navaseelan4u@gmail.com" && !string.IsNullOrWhiteSpace(model.Password) && model.Password == "nava!")
        return RedirectToAction("Index", "Home", new { area = "" });

        return View();
    }
        public ActionResult Index()
        {
            var ip = string.Empty;
            var lat = string.Empty;
            var lon = string.Empty;
            var city = string.Empty;
            
            //IPFunctions.GetIpiInfo(out lat,out lon,out ip,out city);
           
            PreLoadData();
            return View();
        }
        public ActionResult Propertydetails()
        {
            return View();
        }
        public ActionResult Newproperties()
        {
            return View();
        }
        private void PreLoadData()
        {
          
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
                   

                };

            //string[] radiusList = new string[] { "5", "10", "15", "20" };
            var list = new SelectList(radius, "Id", "Name");
            ViewBag.RadiusList = list;

            var cities = _hotelSvc.GetLocationList();
            //ViewBag.LocationList = new SelectList(cities, "Id", "City");

            var properties =  _hotelSvc.GetHotelsList().ToList().Where(q => q.IsApproved).ToList();
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

            ViewBag.AccomodationTypeList = new SelectList(accTypelist, "Id", "Name");

            var acmStandards = _hotelSvc.GetAccomodationStandardList();
            ViewBag.AccomodationStandardList = new SelectList(acmStandards, "Id", "Name");

            //string[] roomsList = new string[] { "1", "2", "3", "4" };
            var rooms = new List<NameValue>
                {
                    new NameValue
                        {
                            Id = 0,
                            Name = "--- Any ---"
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

            ViewBag.POTList = pl;

            var featureList = _hotelSvc.GetFeatureList().ToList();
            var fl = featureList.Select(item => new Features { Id = item.Id, Name = item.Name }).ToList();
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
                        Id=i,
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

            var results = _hotelSvc.GetHotelsList().ToList().Take(3).ToList();//.Where(q => q.IsApproved && q.ActiveTo != null ).OrderByDescending(o => o.ActiveTo).Take(3).ToList();
            var model = new List<PropertyModel>();
            foreach (var p in results)
            {
                var property = new PropertyModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    DisplayPrice = p.DisplayPrice,
                    Discount = p.Discount ?? 0,
                    NoOfRooms = p.NoOfRooms,
                    Squarefeet = (decimal)p.Squarefeet,
                    PropertyUrl = string.Format("/{0}/{1}", "Property", "PropertyDetail?id=" + p.Id),
                    Images = new List<ImageModel>(),
                    Features = new List<Features>(),
                    Places = new List<PlaceModel>(),
                    Meals = new List<DiningModel>(),
                    PoIs = new List<eResorts.Models.PointOfInterest>(),
                    MobilePrimary = p.MobilePrimary,
                    MobileSecondary = p.MobileSecondary,
                    LandLinePrimary = p.LandLinePrimary,
                    LandLineSecondary = p.LandLineSecondary,
                    ContactPerson2 = p.ContactPerson2,
                    Adderss = p.Adderss,
                    Street = p.Street,
                    City = p.City,
                    FeatureList = p.Features,
                    PoiList = p.Interests,
                    MainCityName = _hotelSvc.GetDistrict((int) p.MainCity).City
                };

                if (property.FeatureList != null)
                {
                    var features = property.FeatureList.Split(',').Select(s => s.ToLower()).Take(3).ToArray();
                    property.FeatureList = string.Join(",", features);
                }

                var images = _hotelSvc.Images(p.Id).FirstOrDefault();
                property.DefaultImageUrl = "/Content/img/demo/apt/apt1.jpg";
                if (images != null)
                {
                    var path = "/PropertyImages/" + p.Id + "/thumbs/" + images.ImagePath;

                    property.DefaultImageUrl = path;
                }
                model.Add(property);
            }

            ViewBag.RecentlyAdded = model;

            ViewBag.YoutubeUrls= _hotelSvc.GetYoutubeUrls();

            ViewBag.MinPriceList = new SelectList(minPrice, "Id", "Name");
            ViewBag.MaxPiceList = new SelectList(maxPrice, "Id", "Name");

            ViewBag.SliderImages = _hotelSvc.GetSliderImages().OrderBy(o=>o.DisplayOrder).ToList();

            ViewBag.Testimonials = _hotelSvc.GetRecommandations().OrderBy(o => o.Name).ToList();


            var designation = _hotelSvc.GetDesignationList();
            ViewBag.Designations = new SelectList(designation, "Id", "Name");

            ViewBag.ReviewComments = GetReviewComments();

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
                        var count = properties.Count(q => q.MainCity == distriictId && q.AccommodationType==t.Id);
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

        public JsonResult GetFileredAccomodationStandard(int distriictId,int accoType)
        {
            var properties = _hotelSvc.GetHotelsList().ToList().Where(q => q.IsApproved).ToList();
            //var acmTypes = _hotelSvc.GetAccomodationTypeList();
            var acmStandards = _hotelSvc.GetAccomodationStandardList();
            var acmStandardlist = new List<AccommodationStandard>();

            if (distriictId == 0 && accoType>0)
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

        public ActionResult GetFilteredHotelData(HotelSearchModel filter, string poiList, string featureList)
        {
            var result = new List<Property>();

            if (filter.LocationId2 > 0)
                filter.LocationId = filter.LocationId2;

            if (filter.DesignationId2 > 0)
                filter.DesignationId = filter.DesignationId2;


            var query = filter.Location;
            if (!string.IsNullOrWhiteSpace(query))
                query = query.ToLower();

            result = _hotelSvc.GetHotelsList().ToList().Where(q => q.IsApproved).ToList();

            if (filter.AccomodationTypeId > 0)
            result = result.Where(q => q.AccommodationType == filter.AccomodationTypeId).ToList();

            if (filter.AccomodationStandardId > 0 )
                result = result.Where(q => q.AccommodationStandard == filter.AccomodationStandardId
                       ).ToList();

            if (!string.IsNullOrWhiteSpace(query))
                result = result.Where(q => (q.Description != null && q.Description.ToLower().Contains(query))
                    || (q.Name!=null && q.Name.ToLower().Contains(query))
                    || (q.Adderss != null && q.Adderss.ToLower().Contains(query))
                    || (q.Street != null && q.Street.ToLower().Contains(query))
                    || (q.City != null && q.City.ToLower().Contains(query))).ToList();
            
            if (filter.LocationId>0)
                result = result.Where(q =>  q.MainCity == filter.LocationId).ToList();
   
            if (filter.No_ofRooms > 0)
                    result = result.Where(q => q.NoOfRooms >= filter.No_ofRooms
                 ).ToList();


            if (filter.PriceTo != 0)
                result = result.Where(q => (q.DisplayPrice >= filter.PriceFrom && q.DisplayPrice <= filter.PriceTo)).ToList();

            var desig = _hotelSvc.GetDesignation(filter.DesignationId);
            if (desig != null)
            {
                result = result.Where(q => (q.OwnerDesignation != null && q.OwnerDesignation.Contains(desig.Name))).ToList();
            }
          

            var cityObj = _hotelSvc.GetLocationList().Where(x => x.Id == filter.LocationId).FirstOrDefault();

            //use this for load the result partial view
            TempData["FilteredResults"] = result;

            return Json(new { city = cityObj, hotels = result, radius  = filter.Radius == 52 ? 100 : filter.Radius });
        }
        public ActionResult SearchResults(int? page)
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
                               Squarefeet=(decimal) p.Squarefeet,
                               PropertyUrl = string.Format("/{0}/{1}", "Property", "PropertyDetail?id=" + p.Id),
                               Images = new List<ImageModel>(),
                               Features = new List<Features>(),
                               Places = new List<PlaceModel>(),
                               Meals = new List<DiningModel>(),
                               PoIs = new List<eResorts.Models.PointOfInterest>(),
                               MobilePrimary = p.MobilePrimary,
                               MobileSecondary = p.MobileSecondary,
                               LandLinePrimary = p.LandLinePrimary,
                               LandLineSecondary = p.LandLineSecondary,
                               ContactPerson2 = p.ContactPerson2,
                               Adderss = p.Adderss,
                               Street = p.Street,
                               City = p.City,
                               FeatureList = p.Features,
                               PoiList = p.Interests,
                               MainCityName = _hotelSvc.GetDistrict((int)p.MainCity).City
                           };

                        if (property.FeatureList != null)
                        {
                            var features = property.FeatureList.Split(',').Select(s => s.ToLower()).Take(3).ToArray();
                            property.FeatureList = string.Join(",", features);
                        }
                        
                        var images = _hotelSvc.Images(p.Id).FirstOrDefault();
                        property.DefaultImageUrl = "/Content/img/demo/apt/apt1.jpg";
                        if (images!=null)
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

        public JsonResult GetLocation(int id)
        {
            var loc = string.Empty;
            var r = _hotelSvc.GetLocationList().Where(q => q.Id == id).FirstOrDefault();
            if (r != null)
                loc = r.Description??"Sorry !, No information available right now...";

            return Json(loc, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetRecommandation(int id)
        {
            var r = _hotelSvc.GetRecommandation(id);
            return Json(r, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetRecentlyAdded()
        {
            var results = _hotelSvc.GetHotelsList().ToList().Take(3).ToList();//.Where(q => q.IsApproved && q.ActiveTo != null ).OrderByDescending(o => o.ActiveTo).Take(3).ToList();
            var model = new List<PropertyModel>();
            foreach (var p in results)
            {
                var property = new PropertyModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    DisplayPrice = p.DisplayPrice,
                    
                    Discount = p.Discount ?? 0,
                    NoOfRooms = p.NoOfRooms,
                    Squarefeet = (decimal)p.Squarefeet,
                    PropertyUrl = string.Format("/{0}/{1}", "Property", "PropertyDetail?id=" + p.Id),
                    Images = new List<ImageModel>(),
                    Features = new List<Features>(),
                    Places = new List<PlaceModel>(),
                    Meals = new List<DiningModel>(),
                    PoIs = new List<eResorts.Models.PointOfInterest>(),
                    MobilePrimary = p.MobilePrimary,
                    MobileSecondary = p.MobileSecondary,
                    LandLinePrimary = p.LandLinePrimary,
                    LandLineSecondary = p.LandLineSecondary,
                    ContactPerson2 = p.ContactPerson2,
                    Adderss = p.Adderss,
                    Street = p.Street,
                    City = p.City,
                    FeatureList = p.Features,
                    PoiList = p.Interests,
                    Latitude=(decimal) p.Latitude,
                    Longitude=(decimal) p.Longitude,
                    MainCityName = _hotelSvc.GetDistrict((int)p.MainCity).City
                };

                if (property.FeatureList != null)
                {
                    var features = property.FeatureList.Split(',').Select(s => s.ToLower()).Take(3).ToArray();
                    property.FeatureList = string.Join(",", features);
                }

                var images = _hotelSvc.Images(p.Id).FirstOrDefault();
                property.DefaultImageUrl = "/Content/img/demo/apt/apt1.jpg";
                if (images != null)
                {
                    var path = "/PropertyImages/" + p.Id + "/thumbs/" + images.ImagePath;

                    property.DefaultImageUrl = path;
                }
                model.Add(property);
            }

            var cityObj = _hotelSvc.GetLocationList().Where(x => x.Id == 0).FirstOrDefault();

            return Json(new { city = cityObj, hotels = model, radius = 5 });
        }

        public ActionResult aboutus()
        {
            return View();
        }
        [HttpGet]
        public ActionResult contactus()
        {
            ViewBag.ContactUs = string.Empty;
            var model = new ContactUs();
            return View(model);
        }
        [HttpPost]
        public ActionResult contactus(ContactUs model)
        {
            if (ModelState.IsValid)
            {
                var s = new StringBuilder();
                s.Append("<div><p>");
                s.Append("Dear sir,<br/>");
                s.Append("Name : ");
                s.Append(model.FirstName);
                s.Append(" ");
                s.Append(model.LastName);
                s.Append("<br/>");
                s.Append("E-mail : ");
                s.Append(model.Email);
                s.Append("<br/>");
                s.Append("</p>");
                s.Append("<p>");
                s.Append("Contact : ");
                s.Append(model.ContactNumber ?? "Not given");
                s.Append("<br/>");
                s.Append("Comments : ");
                s.Append(model.Comments);
                s.Append("<br/>");
                s.Append("</p></div>");

                EmailService.ContactUs("mungoterrain@mac.com", s.ToString());
                //EmailService.ContactUs("navaseelan4u@gmail.com", s.ToString());
                ViewBag.ContactUs = "We will respond you as soon possible";
            }

            //return View();
            return RedirectToAction("Index");
        }
        public IEnumerable<ReviewModel> GetReviewComments()
        {
            var comments = new List<ReviewModel>();
            var path = "/Content/img/demo/apt/apt1.jpg";
            var results = _hotelSvc.GetReviewComments().Take(4);
            foreach (var visitorReview in results)
            {
                var user = _userService.GetUser(visitorReview.VisitorId);
                if (user != null)
                {
                    var s = _hotelSvc.Images(visitorReview.PropertyId).FirstOrDefault();
                    if (s != null && !string.IsNullOrWhiteSpace(s.ImagePath))
                        path = "/PropertyImages/" + visitorReview.PropertyId + "/thumbs/" + s.ImagePath;

                    var r = new ReviewModel
                    {
                        Id = visitorReview.Id,
                        Name = string.Format("{0} {1}", user.FirstName, user.LastName),
                        Email = user.Email,
                        Comment = visitorReview.Comments,
                        ImageUrl = path
                       
                    };
                    comments.Add(r);
                }

            }
            return comments;
        }
    }
}
