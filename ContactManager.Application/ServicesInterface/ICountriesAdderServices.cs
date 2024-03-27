using ContactManager.Application.DTOs;
using ContactManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.ServicesInterface
{
    public interface ICountriesAdderServices
    {
        Task<CountryResponseDTO> AddCountry(AddCountryDTO addCountryDTO);
    }
}
