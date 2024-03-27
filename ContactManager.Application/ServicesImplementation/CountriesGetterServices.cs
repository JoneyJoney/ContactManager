using ContactManager.Application.DTOs;
using ContactManager.Application.RepoInterface;
using ContactManager.Application.ServicesInterface;
using ContactManager.Domain.Models;

namespace ContactManager.Application.ServicesImplementation
{
    public class CountriesGetterServices : ICountriesGetterService
    {
        private readonly ICountryRepository _countryRepository;
        public CountriesGetterServices(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<List<Country>> GetCountries()
        {
            List<Country> countries = await _countryRepository.GetCountries();
            return countries.ToList();
        }

        public async Task<Country> GetCountryBy(int CID)
        {
            if(CID == null)
            {
                return null;
            }
            Country? country = await _countryRepository.GetCountryByID(CID);
            if(country == null)
            {
                return null;
            }
            return country;
        }

        public async Task<Country> FindCountryName(string CName)
        {
            if (CName == null)
            {
                return null;
            }
            Country? country = await _countryRepository.FindCountryName(CName);
            if (country == null)
            {
                return null;
            }
            return country;
        }
    }
}
