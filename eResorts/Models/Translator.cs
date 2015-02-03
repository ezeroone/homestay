using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eResorts.Models
{
    public class TranslatorModel
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Landline { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public string ImageName { get; set; }
        public List<TranslatorModel> Translators { get; set; }
    }
}