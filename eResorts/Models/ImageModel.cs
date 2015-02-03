using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eResorts.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ImageDescription { get; set; }
        public string ImagePath { get; set; }
        public bool DefaultImage { get; set; }
    }
    public class ImageViewModel
    {
        public int Id { get; set; }
          [Required]
        public string ImageDescription { get; set; }
        public bool DefaultImage { get; set; }
        public List<ImageModel> ImageList { get; set; }

       
    }

    
}