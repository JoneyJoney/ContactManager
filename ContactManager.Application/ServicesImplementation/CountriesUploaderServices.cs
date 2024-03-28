using ContactManager.Application.RepoInterface;
using ContactManager.Application.ServicesInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.ServicesImplementation
{
    public class CountriesUploaderServices : ICountriesUploaderServices
    {
        private readonly ICountryRepository _countryRepository;
        public CountriesUploaderServices(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository; 
        }

        public async Task<string> GetCountryExist(DataTable dtcountry)
        {
            return await _countryRepository.GetCountryExist(dtcountry);
        }

        public async Task<string> UploadCountry(DataTable dtcountry)
        {
            return await _countryRepository.UploadCountry(dtcountry);
        }
    }
}
