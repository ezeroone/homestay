namespace eZeroOne.MailService.Templates
{
    public partial class BookingEmail
    {
        public string Link { get; set; }
        public string UserName { get; set; }
        public string ClientName { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Amount { get; set; }
        public string Days { get; set; }
    }
}