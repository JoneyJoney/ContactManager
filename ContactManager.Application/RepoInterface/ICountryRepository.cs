using ContactManager.Application.DTOs;
using ContactManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.RepoInterface
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountries();
        Task<Country> GetCountryByID(int CID);
        Task<Country> FindCountryName(string CName);
        Task<Country> AddCountry(Country country);
        Task<Country> UpdateCountry(Country country);
        Task<bool> DeleteCountry(int countryId);

        Task<string> UploadCountry(DataTable dtcountry);
        Task<string> GetCountryExist(DataTable dtcountry);
    }
}
