using System;
using System.ComponentModel.DataAnnotations;

namespace eResorts.Models
{
    public class ClientModel
    {
        [Required]
        public int Id { get; set; }
       
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address Name is Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Street is Required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }
        //[Required(ErrorMessage = "ZipCode is Required")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Phone is Required")]
        [RegularExpression(@"^(\(?[0-9]*\)?)?[0-9_\- \(\)]{10,15}$", ErrorMessage = "Invalid format")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Mobile is Required")]
        [RegularExpression(@"^(\(?[0-9]*\)?)?[0-9_\- \(\)]{10,15}$", ErrorMessage = "Invalid format")]
        public string Mobile { get; set; }
        public string Fax { get; set; }

        [Required(ErrorMessage = "Enter your email address."), RegularExpression(@"(?i)\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Invalid email address entered.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your password."), MinLength(6, ErrorMessage = "The password has a minimum length of 6 characters.")]
        public string Password { get; set; }

        public string Remarks { get; set; }
       
        public string Country { get; set; }
      
        public bool IsActive { get; set; }
        //[DataType(DataType.Date)]
        //[Required(ErrorMessage = "Date of Birth is Required")]
        //public DateTime DateOfBirth { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime CreatedDate { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime? ModifiedDate { get; set; }
    }
}