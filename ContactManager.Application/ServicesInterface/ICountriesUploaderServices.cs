using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.ServicesInterface
{
    public interface ICountriesUploaderServices
    {
        Task<string> UploadCountry(DataTable dtcountry);
    }
}
