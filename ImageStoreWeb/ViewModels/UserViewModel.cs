using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageStoreWeb.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        [MaxLength(100,ErrorMessage ="The {0} can only be in {1} characters long")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password and Confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(100, ErrorMessage = "The {0} can only be in {1} characters long")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(100, ErrorMessage = "The {0} can only be in {1} characters long")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100, ErrorMessage = "The {0} can only be in {1} characters long")]
        public string Email { get; set; }

        [Phone]
        [MaxLength(20, ErrorMessage = "The {0} can only be in {1} characters long")]
        public string Phone { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} can only be in {1} characters long")]
        public string Country { get; set; }

        [MaxLength(200, ErrorMessage = "The {0} can only be in {1} characters long")]
        public string Street { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} can only be in {1} characters long")]
        public string City { get; set; }

        [MaxLength(60, ErrorMessage = "The {0} can only be in {1} characters long")]
        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        [MaxLength(10, ErrorMessage = "The {0} can only be in {1} characters long")]
        public string Zip { get; set; }


    }
}