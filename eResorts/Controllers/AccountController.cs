using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eResorts.Models;
using eZeroOne.Common;
using eZeroOne.Domain;
using eZeroOne.MailService;
using eZeroOne.Service.Common;
using eZeroOne.Service.Customers;
using eZeroOne.Service.Repository;
using eZeroOne.Service.Users;
using Common = eZeroOne.Service.Common.Common;

namespace eResorts.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly ICommon _common;
        public AccountController(IRepository repository, IUnitOfWork unitOfWork)
        {
            _userService = new UserService(repository, unitOfWork);
            _common = new eZeroOne.Service.Common.Common(repository, unitOfWork);
            _customerService = new CustomerService(repository, unitOfWork);
        }

        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
           if (ModelState.IsValid) //All the field passed the validation step
            {
                try
                {
                    var user = _userService.ValidateUser(model.Email, model.Password, 1);
                    if (user == null)
                    {
                       
                        return View();
                    }

                    FormsAuthentication.SetAuthCookie(model.Email, model.KeepMeSignedIn);

                    //if there is a return url, then navigate to it
                    var returnUrl = Session["ReturnUrl"] as string;
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                        return Redirect(returnUrl);

                    switch (user.RoleId)
                    {
                        case (int)UserRoles.Admin:
                            return RedirectToAction("AddProperty", "PropertyAdmin", new { area = "Admin" });
                        case (int)UserRoles.Client:
                            return RedirectToAction("AddProperty", "ClientProperty", new { area = "Clients" });
                        //case (int)UserRoles.Visitor:
                        //    return RedirectToAction("Index", "Visitors", new { area = "" });
                        //case (int)UserRoles.All:
                        //    return RedirectToAction("Index", "Home", new { area = "" });
                        default:
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }

                }
                catch (Exception e)
                {
                    //Logger.Error("Account(Login)", e);
                    return this.View("Error");
                }
            }

            return View(model);
        }

        public PartialViewResult UserLogonInfo()
        {
            var userModel = new LoginUserDetails { UserName = string.Empty };
            if (User.Identity.IsAuthenticated)
            {
                var userEmail = User.Identity.Name;
                var user = _userService.GetUser(userEmail, 1);
                if (user != null)
                    userModel.UserName = string.Format("{0} {1}", user.FirstName.ToUpper(), user.LastName.ToUpper());

            }
            return PartialView(userModel);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult VisitorRegistration()
        {
            PreloadData();
            var model = new VisitorModel();
            return View(model);
        }

        [AyahValidation]
        [HttpPost]
        public ActionResult VisitorRegistration(VisitorModel model)
        {
            PreloadData();
            var isPass = true;
            if (model.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Minimum length is 6 characters");
                isPass = false;
                return View(model);

            }

            if (isPass && ModelState.IsValid)
            {
                var newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    CompanyId=1,
                    Created=DateTime.Now,
                    RoleId=3
                };
                
                var result = _customerService.SaveCustomerLogin(newUser);
                if (result)
                {
                  
                    //send the activation email to the user
                    var activationToken = new EmailActivationToken
                    {
                        Email = model.Email,
                        ExpirationDate = DateTime.Now.AddHours(2)
                    };
                    SecurityService.EncryptActivationToken(activationToken);

                    EmailService.SendActivationEmail(activationToken.EncryptedForm, model.Email, model.FirstName);

                    return RedirectToAction("ActivationEmailConfirmation", "Account");
                }
                else
                {
                    ModelState.AddModelError("InvalidEmailPass", "E-mail is already existing...");
                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ClientRegistration()
        {
            PreloadData();
            var model = new ClientModel();
            return View(model);
            
        }

        [AyahValidation]
        [HttpPost]
        public ActionResult ClientRegistration(ClientModel model)
        {
            PreloadData();
            var isPass = true;
            if (model.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Minimum length is 6 characters");
                isPass = false;
                return View(model);

            }

            if (isPass &&ModelState.IsValid)
            {
                var newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    CompanyId = 1,
                    Created = DateTime.Now,
                    RoleId = 2
                };

                var client = new Client
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Address = model.Address,
                    Street = model.Street,
                    City = model.City,
                    Zip = model.Zip,
                    Phone = model.Phone,
                    Mobile = model.Mobile,
                    Country = 210,//srilanka
                    Fax = model.Fax,
                };

                var result = _customerService.SaveCustomerLogin(newUser);
                if (result)
                {
                    var user = _userService.GetUser(newUser.Email, 1);
                    client.ClientId = user.UserId;
                    _customerService.AddClient(client);


                    //send the activation email to the user
                    var activationToken = new EmailActivationToken
                    {
                        Email = model.Email,
                        ExpirationDate = DateTime.Now.AddHours(2)
                    };
                    SecurityService.EncryptActivationToken(activationToken);

                    EmailService.SendActivationEmail(activationToken.EncryptedForm, model.Email, model.FirstName);

                    return RedirectToAction("ActivationEmailConfirmation", "Account");
                }
                else
                {
                    ModelState.AddModelError("InvalidEmailPass", "E-mail is already existing...");
                    return View(model);
                }

            }
           
            return View(model);
        }
        public ActionResult ActivationEmailConfirmation()
        {
           return View();
        }
        public ActionResult ActivateEmail(string activationCode, string shoppingCartRefId)
        {
            var token = new EmailActivationToken { EncryptedForm = activationCode };
            SecurityService.DecryptActivationToken(token);

            ///* an issued activation token is expired if the date associated with it is less then the current date */
            //if (token.ExpirationDate.da < DateTime.Now)
            //{
            //    return View("Error");
            //}

            /* we check if the user does exist in the database or not, */
            var currentUser = _userService.GetUser(token.Email, 1);
            if (currentUser == null)
            {
                return View("Error");
            }
            TempData["UserName"] = currentUser.FirstName + " " + currentUser.LastName;
            
            // currentUser.IsEmailActivated = true;
            if (_common.ActivateEmail(token.Email))
                FormsAuthentication.SetAuthCookie(currentUser.Email, true);

            return View();
        }

        private void PreloadData()
        {
            var title = _common.LoadTitles();
            ViewBag.Titles = new SelectList(title, "Id", "Name");
            
        }
    }
}
