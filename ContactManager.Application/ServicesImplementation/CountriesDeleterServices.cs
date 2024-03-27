using ContactManager.Application.RepoInterface;
using ContactManager.Application.ServicesInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.ServicesImplementation
{
    public class CountriesDeleterServices : ICountriesDeleteServices
    {
        private readonly ICountryRepository _countryRepository;
        public CountriesDeleterServices(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<bool> DeleteCountry(int countryId)
        {
            Boolean check =  await _countryRepository.DeleteCountry(countryId);
            if(check)
            {
                return true;
            }
            return false;
        }
    }
}
