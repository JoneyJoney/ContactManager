using ContactManager.Application.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Please Enter PersonName")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Please Enter EmailID")]
        [EmailAddress(ErrorMessage = "Please Enter Valid EmailID")]
        [Remote(action: "IsEmailAlreadyExist",controller:"Account",ErrorMessage = "EmailID alredy exists")]
        public string? EmailID { get; set; }


        [Required(ErrorMessage = "Please Enter PhoneNumber")]
        [RegularExpression("^[0-9]*$",ErrorMessage = "Please Enter Valid Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please Enter ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password does not match with the Confirm Password")]
        public string? ConfirmPassword { get; set; }

        public UserTypeOptions UserRole { get; set; } = UserTypeOptions.User;
    }
}
