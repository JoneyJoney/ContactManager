using ContactManager.Application.RepoInterface;
using ContactManager.Domain.Models;
using ContactManager.InfraStructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.InfraStructure.RepoImplementation
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public CountryRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public async Task<Country> AddCountry(Country country)
        {
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
            return country; 
        }

        public async Task<bool> DeleteCountry(int countryId)
        {
            Country country = await _db.Countries.FirstOrDefaultAsync(x => x.CountryID == countryId);
            if(country != null)
            {
                _db.Countries.Remove(country);
                await _db.SaveChangesAsync();
                return true;

            }
           return false;
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _db.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryByID(int CID)
        {
            return await _db.Countries.FirstOrDefaultAsync(x => x.CountryID == CID);
        }

        public async Task<Country> FindCountryName(string CName)
        {
            return await _db.Countries.FirstOrDefaultAsync(x => x.CountryName == CName);
        }

        public async Task<Country> UpdateCountry(Country country)
        {
            _db.Countries.Update(country);
            await _db.SaveChangesAsync();
            return country;
        }

        public Task<string> UploadCountry(DataTable dtcountry)
        {
            string constring = _configuration.GetConnectionString("");
            return "";

        }
    }
}
