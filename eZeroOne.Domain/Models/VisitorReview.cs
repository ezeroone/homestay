namespace eZeroOne.Domain
{
    public class VisitorReview
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public int PropertyId { get; set; }
        public string Comments { get; set; }

    }
}