using ContactManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.DTOs
{
    public class AddCountryDTO
    {
        [Required(ErrorMessage = "Please Enter CountryName")]
        [Remote(action: "IsCountryAlreadyExist", controller: "Country", ErrorMessage = "Country alredy exists")]
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country() {CountryName  = CountryName}; 
        }
    }
}
