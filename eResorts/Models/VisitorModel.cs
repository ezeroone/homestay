using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eResorts.Models
{
    public class VisitorModel
    {
        [Required]
        public int Id { get; set; }
       // [Required]
        public int TitleId { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
       // [Required(ErrorMessage = "Address Name is Required")]
        public string Address { get; set; }
        //[Required(ErrorMessage = "Address2 is Required")]
        public string Street { get; set; }
       // [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }
        //[Required(ErrorMessage = "ZipCode is Required")]
        public string Zip { get; set; }
       // [Required(ErrorMessage = "Phone is Required")]
        [RegularExpression(@"^(\(?[0-9]*\)?)?[0-9_\- \(\)]{10,15}$", ErrorMessage = "Invalid format")]
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessage = "Enter your email address."), RegularExpression(@"(?i)\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Invalid email address entered.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your password."), MinLength(6, ErrorMessage = "The password has a minimum length of 6 characters.")]
        public string Password { get; set; }
        public string Remarks { get; set; }
        //[Required]
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.Date)]
        //[Required(ErrorMessage = "Date of Birth is Required")]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }

    public class ReviewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
    }
}