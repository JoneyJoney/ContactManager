using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please Enter EmailID")]
        [EmailAddress(ErrorMessage = "Please Enter Valid EmailID")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
