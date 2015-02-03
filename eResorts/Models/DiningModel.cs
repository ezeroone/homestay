using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eResorts.Models
{
    public class DiningModel
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
       //   [Required]
        public string Food { get; set; }
        public int PropertyId { get; set; }
          [Required]
        public decimal Price { get; set; }
          [Required]
        public string MealType { get; set; }
          public string Imagepath { get; set; }
        public List<DiningModel> Meals { get; set; }
    }
}