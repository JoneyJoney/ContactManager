using ContactManager.Application.DTOs;
using ContactManager.Domain.Models;

namespace ContactManager.Application.ServicesInterface
{
    public interface ICountriesGetterService
    {
        Task<List<Country>> GetCountries();

        Task<Country> GetCountryBy(int CID);

        Task<Country> FindCountryName(string CName);
    }
}
