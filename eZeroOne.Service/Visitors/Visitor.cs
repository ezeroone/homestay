using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using eZeroOne.Domain;
using eZeroOne.Service.Repository;

namespace eZeroOne.Service.Visitors
{
    public class Visitor : IVisitor
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Visitor(IRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public Domain.Visitor SaveEmployee(Domain.Visitor employee, out bool isExist)
        {
            isExist = false;

            if (!string.IsNullOrWhiteSpace(employee.FirstName) || !string.IsNullOrWhiteSpace(employee.LastName))
            {
                var emp = _repository.Single<Domain.Visitor>(t => t.FirstName == employee.FirstName.Trim() && t.LastName == employee.LastName.Trim());
                if (emp != null)
                {
                    employee = emp;
                    isExist = true;
                    return employee;
                }
            }

            var nemEmp = new eZeroOne.Domain.Visitor
                                 {
                                     Title = employee.Title,
                                     FirstName = employee.FirstName,
                                     LastName = employee.LastName,
                                     //Occupation = employee.Occupation,
                                     //Organisation = employee.Organisation,
                                     //Address = employee.Address,
                                     //Street = employee.Street,
                                     //City = employee.City,
                                     //Phone = employee.Phone,
                                     //Mobile = employee.Mobile,
                                     //Fax = employee.Fax,
                                     Email = employee.Email,
                                     //Website = employee.Website,
                                     //Remarks = employee.Remarks,
                                     //Country = employee.Country,
                                     Created = System.DateTime.UtcNow,
                                     Gender=employee.Title==1?1:2,
                                     Active = true
                                 };

            _repository.Add(nemEmp);
            _unitOfWork.Commit();
            employee = nemEmp;

            return employee;
        }

        public bool UpdateEmployee(eZeroOne.Domain.Visitor employee)
        {
            var emp = _repository.Single<Domain.Visitor>(t => t.VisitorId == employee.VisitorId);
            if (emp != null)
            {
                emp.FirstName = employee.FirstName;
                emp.Title = employee.Title;
                emp.LastName = employee.LastName;
                emp.Email = employee.Email;
            }

            _unitOfWork.Commit();
                
            
            return true;
        }
        
        
        public void SaveEmailNotifications(Guid id, int empId, int? groupId, int? companyId, int userId)
        {
            //var repository = new Repository<eZeroOne.Domain.EmailNotification>(_dataContext);
            ////var repositoryDesigEmp = new Repository<eZeroOne.Domain.EmployeeVsDesignation>(_dataContext);

            //var empExist = (from e in repository.GetAll()
            //               where e.UniqueId == id && e.EmployeeId==empId && e.GroupId==groupId && e.CompanyId==companyId && e.IsSend==false
            //               select e).FirstOrDefault();
            //if (empExist == null)
            //{
            //    var email = new EmailNotification
            //                    {
            //                        EmployeeId = empId,
            //                        UniqueId = id,
            //                        IsSend = false,
            //                        GroupId = groupId,
            //                        CompanyId = companyId,
            //                        DivisionId = 0,
            //                        DepartmentId = 0,
            //                        SectionId = 0,
            //                        CreatedDate = DateTime.UtcNow,
            //                        UserId = userId
            //                    };

            //    repository.Add(email);
            //    repository.SaveChanges();
           // }
        }
        public void UpdateEmailNotifications(Guid id, int empId)
        {
            //var repository = new Repository<eZeroOne.Domain.EmailNotification>(_dataContext);
            //var email = new EmailNotification
            //{
            //    EmployeeId = empId,
            //    UniqueId = id,
            //    IsSend = false
            //};

            //repository.Add(email);
            //repository.SaveChanges();
        }
      
        public bool UpdateEmailNotifications(Guid id, int empId,string fromAdd,string toAdd,string subject,string body,string userName,string password,int port)
        {
            //var repository = new Repository<eZeroOne.Domain.EmailNotification>(_dataContext);
            //var emp  = repository.Single(t => t.EmployeeId==empId && t.UniqueId==id && t.IsSend==false);

            ////var enc = SecurityService.EncryptString(password);
            ////var dec = SecurityService.DecryptString(enc);

            //if (emp!=null)
            //{
            //    emp.FromAddress = fromAdd;
            //    emp.ToAddress = toAdd;
            //    emp.Subject = subject;
            //    emp.FromUserName = userName;
            //    emp.FromPassword = SecurityService.EncryptString(password);
            //    emp.MailPort = port;
            //    emp.Message = body;
            //    emp.CreatedDate = DateTime.UtcNow;
            //    repository.SaveChanges();
            //}
            return true;
        }
       // public bool SendEmailNotifications(Guid id)
       // {
       //     try
       //     {
       //         var repository = new Repository<eZeroOne.Domain.EmailNotification>(_dataContext);
       //         var emp = repository.Single(t => t.UniqueId == id && t.IsSend == false);
               
       //         if (emp != null)
       //         {
       //             var basicCredential = new NetworkCredential(emp.FromUserName, SecurityService.DecryptString(emp.FromPassword));
       //             var fromAddress = new MailAddress(emp.FromAddress);
                    
       //             var isSent = true;
       //             using (var message = new MailMessage())
       //             {
       //                // message.To.Add(toAddresses);
       //                 AddToAddress(id, message);
       //                 message.From = fromAddress;
       //                 message.Subject = emp.Subject;

       //                 var htmlDocument = new HtmlDocument();
       //                 htmlDocument.LoadHtml(emp.Message);
       //                 var nodes = htmlDocument.DocumentNode.SelectNodes("//img");
       //                 // if nodes found
       //                 var bodyMessage = emp.Message;
       //                 if (nodes!=null)
       //                 {
       //                     foreach (var node in nodes)
       //                     {
       //                         var att = node.Attributes["src"];
       //                         var url = (att.Value);
       //                         string fileName = Path.GetFileName(att.Value);

       //                         var imageFolderPath = System.Configuration.ConfigurationManager.AppSettings["ImageFolderPath"].ToString(CultureInfo.InvariantCulture);
       //                         //var imageFolder = url.Replace(imageFolderPath, "");
       //                         //var imgAtt = new Attachment(imageFolder) { ContentId = fileName };
       //                         var filePath = att.Value.Replace("../../", imageFolderPath);
       //                         var imgAtt = new Attachment(filePath) { ContentId = fileName };

       //                         //var imgAtt = new Attachment(imageFolderPath + "/" + fileName) { ContentId = fileName };

       //                         //give it a content id that corresponds to the src we added in the body img tag
       //                         //add the attachment to the email
       //                         message.Attachments.Add(imgAtt);
       //                         var replaceCid = "cid:" + fileName;
       //                         bodyMessage = bodyMessage.Replace(url, replaceCid);

       //                     }
       //                 }


       //                 message.Body = bodyMessage;
                       
       //                 message.Priority = MailPriority.High;
       //                 message.IsBodyHtml = true;
                       
       //                 message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
       //                 message.ReplyToList.Add(new MailAddress(emp.FromAddress));
                        
       //                 try
       //                 {
       //                     foreach (var imgScr in Attachments(id))
       //                     {
       //                        //message.Attachments.Add(new Attachment(new MemoryStream(imgScr.Attachment), imgScr.AttachmentPath, MediaTypeNames.Image.Jpeg));
       //                         message.Attachments.Add(new Attachment(new MemoryStream(imgScr.Attachment), imgScr.AttachmentPath));
       //                     }
       //                 }
       //                 catch (Exception e)
       //                 {
       //                     throw;
       //                 }

       //                 var smtpClient = new SmtpClient
       //                 {
       //                     Host = "smtp.gmail.com",
       //                     Port = 587,
       //                     UseDefaultCredentials = false,
       //                     Timeout = 100000,
       //                     Credentials = basicCredential,
       //                     DeliveryMethod = SmtpDeliveryMethod.Network
       //                 };
       //                 eZeroOne.MailService.EmailService.Send(message, smtpClient, out isSent);

       //                 UpdateMailSent(id, isSent);
                       
       //             }
       //         }
       //     }
       //     catch (Exception ex)
       //     {
       //         return false;
       //     }
           
          
       //    return true;
       //}
       // public bool SendEmailNotifications(Guid id, int empId, string path)
       // {
       //     //var message = new MailMessage();
       //     try
       //     {
       //         var removeUrl = System.Configuration.ConfigurationManager.AppSettings["urlPath"].ToString();
       //         var repository = new Repository<eZeroOne.Domain.EmailNotification>(_dataContext);
       //         var emp = repository.Single(t => t.EmployeeId == empId && t.UniqueId == id && t.IsSend == false);
       //         if (emp != null && !string.IsNullOrWhiteSpace(emp.ToAddress))
       //         {
       //             var smtpClient = new SmtpClient();
       //             var basicCredential = new NetworkCredential(emp.FromUserName, SecurityService.DecryptString(emp.FromPassword));
       //             var fromAddress = new MailAddress(emp.FromAddress);

       //             smtpClient.Host = "smtp.gmail.com";
       //             smtpClient.Port = 587;
       //             smtpClient.UseDefaultCredentials = false;
       //             smtpClient.Credentials = basicCredential;
       //             var isSent = true;
       //             using (var message = new MailMessage())
       //             {
       //                 message.From = fromAddress;
       //                 message.To.Add(new MailAddress(emp.ToAddress));
       //                 message.Subject = emp.Subject;

       //                 var htmlDocument = new HtmlDocument();
       //                 htmlDocument.LoadHtml(emp.Message);
       //                 var nodes = htmlDocument.DocumentNode.SelectNodes("//img");
       //                 // if nodes found
       //                 var bodyMessage = emp.Message;
       //                 foreach (var node in nodes)
       //                 {
       //                     var att = node.Attributes["src"];
       //                     var url = (att.Value);
       //                     string fileName = Path.GetFileName(att.Value);

       //                     var imageFolderPath = System.Configuration.ConfigurationManager.AppSettings["ImageFolderPath"].ToString(CultureInfo.InvariantCulture);
       //                     //var imageFolder = url.Replace(imageFolderPath, "");
       //                     //var imgAtt = new Attachment(imageFolder) { ContentId = fileName };
       //                     var filePath = att.Value.Replace("../../", imageFolderPath);
       //                     var imgAtt = new Attachment(filePath) { ContentId = fileName };

       //                     //var imgAtt = new Attachment(imageFolderPath + "/" + fileName) { ContentId = fileName };

       //                     //give it a content id that corresponds to the src we added in the body img tag
       //                     //add the attachment to the email
       //                     message.Attachments.Add(imgAtt);
       //                     var replaceCid = "cid:" + fileName;
       //                     bodyMessage = bodyMessage.Replace(url, replaceCid);

       //                 }

       //                 message.Body = bodyMessage;

       //                 //message.Body = "<br/> <img src=\"cid:Desert.jpg\">" + emp.Message +
       //                 //               "<br/> <img src=\"cid:Desert.jpg\">";

       //                 message.Priority = MailPriority.High;
       //                 message.IsBodyHtml = true;

       //                 message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
       //                 message.ReplyToList.Add(new MailAddress(emp.FromAddress));

       //                 //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

       //                 try
       //                 {
       //                     foreach (var imgScr in Attachments(id))
       //                     {
       //                         // message.Attachments.Add(new Attachment(new MemoryStream(imgScr.Attachment), imgScr.AttachmentPath, MediaTypeNames.Image.Jpeg));
       //                         message.Attachments.Add(new Attachment(new MemoryStream(imgScr.Attachment), imgScr.AttachmentPath));
       //                     }
       //                 }
       //                 catch (Exception e)
       //                 {
       //                     throw;
       //                 }

       //                 EmailService.EmailService.Send(message, smtpClient, out isSent);

       //                 emp.IsSend = isSent;
       //                 emp.SentDate = DateTime.UtcNow;
       //                 repository.SaveChanges();
       //             }
       //         }
       //     }
       //     catch (Exception ex)
       //     {
       //         return false;
       //     }


       //     return true;
       // }
       
    
       private MailMessage AddToAddress(Guid id, MailMessage message)
       {
           //var repository = new Repository<eZeroOne.Domain.EmailNotification>(_dataContext);
           //var emplist = (from e in repository.GetAll()
           //               where e.UniqueId == id && e.ToAddress!=null &&e.IsSend==false
           //               select new
           //                          {
           //                              Toaddress=e.ToAddress
           //                          }
           //               ).Distinct().ToList();

           //foreach (var add in emplist)
           //{
           //    if (!string.IsNullOrWhiteSpace(add.Toaddress))
           //    message.To.Add(new MailAddress(add.Toaddress));
           //}
           return message;
       }

       private  void UpdateMailSent(Guid id, bool isSent)
       {
           //var repository = new Repository<eZeroOne.Domain.EmailNotification>(_dataContext);
           //var emplist = (from e in repository.GetAll()
           //               where e.UniqueId == id && e.ToAddress != null && e.IsSend == false
           //               select e).Distinct().ToList();
           //foreach (var emailNotification in emplist)
           //{
           //    emailNotification.IsSend = isSent;
           //    emailNotification.SentDate = DateTime.UtcNow;
           //}
           
           //repository.SaveChanges();
           
       }
      
        public List<Guid> GetEmailNotificationsIds()
        {
           // var repository = new Repository<eZeroOne.Domain.EmailNotification>(_dataContext);
            var ids = new List<Guid>();
            //var emplist = (from e in repository.GetAll()
            //               where e.IsSend == false && e.ToAddress != null
            //               select e).Distinct().ToList();
            //foreach (var id in emplist)
            //{
            //    ids.Add(id.UniqueId);
            //}
            return ids.Distinct().ToList();
;
        }
        
        private List<EmailAttachment>Attachments(Guid id)
        {
            //var repository = new Repository<eZeroOne.Domain.EmailAttachment>(_dataContext);
            var attachments = (from e in _repository.All<Domain.EmailAttachment>()
                               where e.UniqueId == id
                           select e)
                          .ToList();
            return attachments;
        }

        
        public bool DeleteNotifications(Guid id)
        {
            //var repository = new Repository<eZeroOne.Domain.EmailNotification>(_dataContext);
            //var emps = (from e in repository.GetAll()
            //            where e.UniqueId == id
            //            select e).ToList();

            //foreach (var rec in emps)
            //{
            //    repository.Delete(rec);

            //}
            //repository.SaveChanges();

            return true;
        }
    }
}
