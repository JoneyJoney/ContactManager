using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.ServicesInterface
{
    public interface ICountriesDeleteServices
    {
        Task<bool> DeleteCountry(int countryId);
    }
}
