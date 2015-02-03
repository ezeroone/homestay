using System.Web.Mvc;

namespace eZeroOne.MailService.Templates
{
    public partial class RejectedEmail
    {
        public string Link { get; set; }
        public string UserName { get; set; }
        [AllowHtml]
        public string Reason { get; set; }
    }
}