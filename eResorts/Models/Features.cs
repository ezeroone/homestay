using System.Collections.Generic;

namespace eResorts.Models
{
    public class Features
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int GroupId { get; set; }
    }

    public class FeatureList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int DisplayOrder { get; set; }
        public int GroupId { get; set; }
    }

    public class FeatureGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<Features> Features { get; set; }
    }
}