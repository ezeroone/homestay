using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Web;
namespace eZeroOne.MailService
{
    using System;
    using System.Net.Mail;
    using eZeroOne.MailService.Templates;
    
    public static class EmailService
    {
        
        static EmailService()
        {
        }

        #region Public Methods
      
        public static void SendEmail(MailMessage message,bool isAsync)
        {
            // Send message
            if(isAsync)
            {
                
                var emailClient = new SmtpClient();
                var client = new Mvc.Mailer.SmtpClientWrapper(emailClient);
                client.SendCompleted += (sender, e) =>
                {
                    if (e.Error != null || e.Cancelled)
                    {
                        //Handle Error
                    }
                    //Use e.UserState
                };
                client.SendAsync(message, "user state object");
            }
            else
            {
                string user = "navaseelan@ezeroonetech.com";
                string password = "nava1qaz!";

                var loginInfo = new NetworkCredential(user, password);
                var smtpClient = new SmtpClient("mail.ezeroonetech.com", 25)
                {
                    EnableSsl = false,
                    UseDefaultCredentials = false,
                    Credentials = loginInfo,

                };

                smtpClient.Send(message);

                //using (var emailClient = new SmtpClient())
                //{
                //    emailClient.Send(message);
                //}
            }
           
        }
        //public static void SendPasswordResetEmail(string recoveryKey, string userEmail)
        //{
        //    var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        //    var link = helper.Action("ResetPassword", "Account", new { resetKey = recoveryKey }, HttpContext.Current.Request.Url.Scheme);
        //    var resetEmailBody = new PasswordResetEmail()
        //    {
        //        ResetLink = link
        //    };

        //    const string subject = "Coffeelovesmilk Password Recovery";
        //    var from = new MailAddress("mail@coffeelovesmilk.com");
        //    var to = new MailAddress(userEmail);
        //    try
        //    {
        //        var mailMessage = new MailMessage(from, to)
        //        {
        //            Subject = subject,
        //            IsBodyHtml = true,
        //            Body = resetEmailBody.TransformText()
        //        };

        //        SendEmail(mailMessage,false);
               
        //    }
        //    catch (Exception ex) //TODO: handle the exception
        //    {
        //        throw ex;
        //    }

        //}

        public static void SendActivationEmail(string emailActivationToken, string userEmail, string userName,Guid?shoppingCartRefId)
        {
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var link = helper.Action("ActivateEmail", "Account", new { shoppingCartRefId = shoppingCartRefId,activationCode = emailActivationToken }, HttpContext.Current.Request.Url.Scheme);
            const string subject = "eShopping Email Activation";

            //link = link + "&shoppingCartRefId=" + shoppingCartRefId;
            var activationEmailBody = new EmailActivationEmail()
            {
                UserName = userName,
                EmailActivationLink = link
            };

            //TODO: for testing purposes, emails will be sent from here
            var from = new MailAddress("donotreply.ezeroone@gmail.com");
            var to = new MailAddress(userEmail);
            try
            {
                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = activationEmailBody.TransformText()
                };

                SendEmail(mailMessage,false);
               

            }
            catch (Exception ex) //TODO: handle the exception
            {
                throw ex;
            }

        }
        public static void SendActivationEmail(string emailActivationToken, string userEmail, string userName)
        {
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var link = helper.Action("ActivateEmail", "Account", new { activationCode = emailActivationToken }, HttpContext.Current.Request.Url.Scheme);
            const string subject = "Besthomestaylanka Email Activation";

            var activationEmailBody = new EmailActivationEmail()
            {
                UserName = userName,
                EmailActivationLink = link
            };

            //TODO: for testing purposes, emails will be sent from here
            var from = new MailAddress("donotreply.ezeroone@gmail.com");
            var to = new MailAddress(userEmail);
            try
            {
                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Priority=MailPriority.High,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
                    Body = activationEmailBody.TransformText()
                };

                SendEmail(mailMessage, false);


            }
            catch (Exception ex) //TODO: handle the exception
            {
                throw ex;
            }

        }

        public static void SendApprovalEmail(string url, string userEmail, string userName,int id)
        {
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var link = helper.Action("PropertyDetail", "Property", new { id = id }, HttpContext.Current.Request.Url.Scheme);
            const string subject = "Besthomestaylanka Property approval";

            url = "http://besthomestaylanka.com/Property/PropertyDetail?id=" + id;
            var emailBody = new ApprovedEmail()
            {
                UserName = userName,
                Link = url,
                
            };

            //TODO: for testing purposes, emails will be sent from here
            var from = new MailAddress("donotreply.ezeroone@gmail.com");
            var to = new MailAddress(userEmail);
            try
            {
                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
                    Body = emailBody.TransformText()
                };

                SendEmail(mailMessage, false);


            }
            catch (Exception ex) //TODO: handle the exception
            {
                throw ex;
            }

        }
        public static void SendPostPropertyEmail(string url, string userEmail, string userName, int id)
        {
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var link = helper.Action("PropertyDetail", "ClientProperty", new { id = id }, HttpContext.Current.Request.Url.Scheme);
            const string subject = "Awaiting property approval";

            url = "http://besthomestaylanka.com/Admin/PropertyAdmin/PropertyDetail?id=" + id;
            var emailBody = new PropertyPosted()
            {
                UserName = userName,
                Link = url,

            };

            //TODO: for testing purposes, emails will be sent from here
            var from = new MailAddress("donotreply.ezeroone@gmail.com");
            //mungoterrain@me.com
            var to = new MailAddress("mungoterrain@me.com");
           // var copy = new MailAddress("Notification_List@contoso.com");
            //message.CC.Add(copy);
            try
            {
                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
                    Body = emailBody.TransformText()
                };

                mailMessage.CC.Add(userEmail);

                SendEmail(mailMessage, false);


            }
            catch (Exception ex) //TODO: handle the exception
            {
                throw ex;
            }

        }

        public static void SendRejectEmail(string url, string userEmail, string userName, string reason,int id)
        {
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var link = helper.Action("PropertyDetail", "Property", new { id = id }, HttpContext.Current.Request.Url.Scheme);
            const string subject = "Besthomestaylanka Property Reject";

            url = "http://besthomestaylanka.com/Clients/ClientProperty/PropertyDetail?id=" + id;

            var emailBody = new RejectedEmail()
            {
                UserName = userName,
                Link = url,
                Reason = reason
            };

            //TODO: for testing purposes, emails will be sent from here
            var from = new MailAddress("donotreply.ezeroone@gmail.com");
            var to = new MailAddress(userEmail);
            try
            {
                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
                    Body = emailBody.TransformText()
                };

                SendEmail(mailMessage, false);


            }
            catch (Exception ex) //TODO: handle the exception
            {
                throw ex;
            }

        }
        public static void SendbookingEmail(string url, string userEmail, string userName, string clientName, int id,string fromDate,string toDate,int noofDays,string amount)
        {
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var link = helper.Action("PropertyDetail", "Property", new { id = id }, HttpContext.Current.Request.Url.Scheme);
            const string subject = "Besthomestaylanka booking confirmation";

            var emailBody = new BookingEmail()
            {
                UserName = userName,
                Link = link,
                FromDate=fromDate,
                ToDate=toDate,
                Amount=amount,
                Days=noofDays.ToString(),
                ClientName = clientName
                
            };

            //TODO: for testing purposes, emails will be sent from here
            var from = new MailAddress("donotreply.ezeroone@gmail.com");
            var to = new MailAddress(userEmail);
            try
            {
                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
                    Body = emailBody.TransformText()
                };

                SendEmail(mailMessage, false);


            }
            catch (Exception ex) //TODO: handle the exception
            {
                throw ex;
            }

        }
     

        #endregion
        #region Private Methods
        public static void EmailComposer(string toAddress, string copyTo, string subject, string templateName, ListDictionary parameterList)
        {
            var message = new MailMessage();
            try
            {
                var templateDir = @"\Templates";
                ///templateDir = ConfigurationManager.AppSettings["TemplateFolderPath"].ToString();

                // Build message

                message.To.Add(new MailAddress(toAddress));
                message.Subject = subject;
                message.Priority = MailPriority.High;

                // Create plain text mode for alternative view
                AlternateView plainView = AlternateView.CreateAlternateViewFromString("Nothing to display ", null, "text/plain");
                message.AlternateViews.Add(plainView);

                // Create HTML email version
                MailDefinition mailDef = new MailDefinition();
                mailDef.BodyFileName = string.Format(@"{0}\{1}", templateDir, templateName);
                mailDef.IsBodyHtml = true;
                mailDef.Subject = subject;

                // It's only use to determine where the access templates from as a relative base.
                MailMessage msgHtml = mailDef.CreateMailMessage(toAddress, parameterList, new System.Web.UI.Control());
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msgHtml.Body, null, "text/html");

                // Add HTML view
                message.AlternateViews.Add(htmlView);

                //sending emails....
                SendEmail(message,true);

            }

            catch (Exception ex)
            {
                string v = ex.Message;
            }

            finally
            {
                message.Dispose();
            }
        }
        public static void SendOrderConfirmation(string title, string jobCategory, string email, string userName, string amount, string addedDate, string renewalDate)
        {
            //const string subject = "Order Confirmation";
            //var order = new OrderConfirmation()
            //{
            //    UserName = userName,
            //    Title = title,
            //    JobCategory = jobCategory,
            //    Email = email,
            //    Amount = amount,
            //    AddedDate = addedDate,
            //    RenewalDate = renewalDate
            //};

            //var from = new MailAddress("do-not-reply@coffeelovesmilk.com");
            //var to = new MailAddress(email);
            //try
            //{
            //    var mailMessage = new MailMessage(from, to)
            //    {
            //        Subject = subject,
            //        IsBodyHtml = true,
            //        Priority = MailPriority.High,
            //        Body = order.TransformText()
            //    };

            //    SendEmail(mailMessage,true);
            //}
            //catch (Exception ex) 
            //{
            //    throw ex;
            //}

        }
        public static void Send(MailMessage message, SmtpClient smtpClient,out bool isSent)
        {
            isSent = true;
            //var client = new Mvc.Mailer.SmtpClientWrapper(smtpClient);
            //client.SendCompleted += (sender, e) =>
            //{
            //    if (e.Error != null || e.Cancelled)
            //    {
            //        //Handle Error
            //    }
            //    //Use e.UserState
            //};
            //client.SendAsync(message, "user state object");

            try
            {
                smtpClient.Send(message);
            }
            catch (SmtpException ex)
            {
                //Error, could not send the message
                isSent = false;
            }
        }

        public static void SendInvoiceDetails(string emailAddress, EmailInvoiceDetails invoice)
        {
            var subject = "Globalspices invoice";
            var from = new MailAddress("donotreply.ezeroone@gmail.com");
            var to = new MailAddress(invoice.Email);

            var copyTo = new MailAddress(invoice.CompanyEmail);

            var mailTemplate = new CustomerInvoice() { Invoice = invoice };
            var message = new MailMessage(from, to)
            {
                Subject = subject,
                IsBodyHtml = true,
                Priority = MailPriority.High,
                Body = mailTemplate.TransformText()
            };

            message.CC.Add(copyTo);
            SendEmail(message, false);
            
        }

        public static void SendInvoiceDetails(string emailAddress,  string body)
        {
            var subject = "Globalspices invoice";
            var from = new MailAddress("donotreply.ezeroone@gmail.com");
            var to = new MailAddress(emailAddress);
            
            var message = new MailMessage(from, to)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            };

           
            SendEmail(message, false);

        }

        public static void ContactUs(string emailAddress, string body)
        {
            var subject = "Contact us Inquiry";
            var from = new MailAddress("donotreply.ezeroone@gmail.com");
            var to = new MailAddress(emailAddress);

            var message = new MailMessage(from, to)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            };


            SendEmail(message, false);

        }


        #endregion
    }
}