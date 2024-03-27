using ContactManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ContactManager.Application.DTOs
{
    public class CountryResponseDTO
    {
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please Enter CountryName")]
        [Remote(action: "IsCountryAlreadyExist", controller: "Country", ErrorMessage = "Country alredy exists")]
        public string? CountryName { get; set; }
       
    }
    public static class CountryExtension
    {
        public static CountryResponseDTO ToCountryResponse(this Country country)
        {
            return  new CountryResponseDTO() { CountryID = country.CountryID,CountryName = country.CountryName};
        }
    }
}
