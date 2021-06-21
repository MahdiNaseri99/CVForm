using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CVForm.Models
{
    public class EditUserBase
    {
        [Required(ErrorMessage ="Please Enter First Name."),
         StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } 
        
        [Required(ErrorMessage ="Please Enter First Name."),
         StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime Birthdate { get; set; }
        
        [Required(ErrorMessage = "Please Enter Your Email.")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please Confirm Your Email.")]
        [EmailAddress]
        [Display(Name = "Confirm Email")]
        [Compare("Email")]
        public string CEmail { get; set; }
        
        public bool? Gender { get; set; }
        
        [Required(ErrorMessage = "Please Type a Password.")]
        [DataType(DataType.Password)]
        [StringLength(16,
            ErrorMessage =
                "Maximum length is {1}, Minimum length is 8, should contain Digits, Letters, and Symbols")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please Enter a Phone Number")]
        [Phone(ErrorMessage = "Not a valid phone number.")]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Please choose profile image")]  
        [Display(Name = "Profile Picture")]  
        public IFormFile ProfileImage { get; set; } 
    }
}