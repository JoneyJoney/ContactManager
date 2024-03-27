using ContactManager.Application.DTOs;
using ContactManager.Application.RepoInterface;
using ContactManager.Application.ServicesInterface;
using ContactManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.ServicesImplementation
{
    public class CountriesAdderServices : ICountriesAdderServices
    {
        private readonly ICountryRepository _countryRepository;
        public CountriesAdderServices(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<CountryResponseDTO> AddCountry(AddCountryDTO addCountryDTO)
        {
            Country country = addCountryDTO.ToCountry();
            await _countryRepository.AddCountry(country);
            return country.ToCountryResponse();
        }
    }
}
