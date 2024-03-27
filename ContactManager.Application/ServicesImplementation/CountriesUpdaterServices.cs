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
    public class CountriesUpdaterServices : ICountriesUpdateService
    {
        private readonly ICountryRepository _countryRepository;
        public CountriesUpdaterServices(ICountryRepository countryRepository)
        {
             _countryRepository = countryRepository;
        }
        public async Task<CountryResponseDTO> UpdateCountry(Country country)
        {
            await _countryRepository.UpdateCountry(country);
            return country.ToCountryResponse();
        }
    }
}
