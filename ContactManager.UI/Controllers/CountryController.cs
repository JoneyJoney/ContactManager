using ContactManager.Application.DTOs;
using ContactManager.Application.RepoInterface;
using ContactManager.Application.ServicesInterface;
using ContactManager.Domain.Models;
using ContactManager.UI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;

namespace ContactManager.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountryController : Controller
    {
       
        private readonly ICountriesGetterService _countryRepository;
        private readonly ICountriesAdderServices _countriesAdderServices;
        private readonly ICountriesUpdateService _countriesUpdateServices;
        private readonly ICountriesDeleteServices _countriesDeleteServices;
        private readonly ICountriesUploaderServices _countriesUploaderServices;
        private readonly IWebHostEnvironment _environment;
        public CountryController(ICountriesGetterService countryRepository,ICountriesAdderServices countriesAdderServices, 
                                ICountriesUpdateService countriesUpdateService,ICountriesDeleteServices countriesDeleteServices,
                                ICountriesUploaderServices countriesUploaderServices,
                                IWebHostEnvironment environment) 
        { 
            _countryRepository = countryRepository;
            _countriesAdderServices = countriesAdderServices;
            _countriesUpdateServices = countriesUpdateService;
            _countriesDeleteServices = countriesDeleteServices;
            _countriesUploaderServices = countriesUploaderServices;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCountry()
        {
            // Convert DataTable to a List of dictionaries
            List<Country> dataList = await _countryRepository.GetCountries();
            return Json(dataList);
        }

        [HttpGet]
        public IActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(AddCountryDTO addCountryDTO)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Error = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                return View(addCountryDTO);
            }

            CountryResponseDTO addCountryDTORespose = await _countriesAdderServices.AddCountry(addCountryDTO);
            if(addCountryDTORespose == null)
            {
                TempData["ErrorMessage"] = "Error in Saving Data";
                return RedirectToAction(nameof(CountryController.Index), "Country");
            }
            TempData["SuccessMessage"] = "Data Saved Successfully";
            return RedirectToAction(nameof(CountryController.Index),"Country");
        }

        [HttpGet]
        public async Task<IActionResult> EditCountry(int CID)
        {
            Country country = await _countryRepository.GetCountryBy(CID);

            if(country == null)
            {
                TempData["ErrorMessage"] = "Issue in fetching Country";
                return RedirectToAction(nameof(CountryController.Index), "Country");
            }

            CountryResponseDTO countryResponseDTO = new CountryResponseDTO
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };

            return View(countryResponseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCountry(CountryResponseDTO countryResponseDTO)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Error = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                return View(countryResponseDTO);
            }

            Country country = new Country
            {
                CountryID = countryResponseDTO.CountryID,
                CountryName = countryResponseDTO.CountryName
            };

            CountryResponseDTO countryResponse = await _countriesUpdateServices.UpdateCountry(country);
            if(countryResponse == null)
            {
                TempData["ErrorMessage"] = "Error in Updating Data";
                return RedirectToAction(nameof(CountryController.Index), "Country");
            }
            TempData["SuccessMessage"] = "Data Updating Successfully";
            return RedirectToAction(nameof(CountryController.Index), "Country");

        }

        public async Task<IActionResult> DeleteCountry(int CID)
        {
            Boolean delcountry = await _countriesDeleteServices.DeleteCountry(CID);
            if(delcountry == false)
            {
                TempData["ErrorMessage"] = "Error in Deleting Data";
                return RedirectToAction(nameof(CountryController.Index), "Country");
            }
            TempData["SuccessMessage"] = "Data Deleted Successfully";
            return RedirectToAction(nameof(CountryController.Index), "Country");
            
        }


        public async Task<IActionResult> IsCountryAlreadyExist(string CountryName)
        {
            Country? country = await _countryRepository.FindCountryName(CountryName);
            if (country == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }

        public IActionResult UploadExcel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            var formFile = Request.Form.Files[0]; // Assuming only one file is uploaded

            if (formFile != null && formFile.Length > 0)
            {
                string directorypath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(directorypath))
                {
                    Directory.CreateDirectory(directorypath);
                }
                string filePath = Path.Combine(directorypath, formFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                DataSet dsTables = ReadExcelToDataTable.ReadExcelFile(filePath);
                DataTable? dtCountry = dsTables.Tables["CName"];
                if(dtCountry != null)
                {
                    if(dtCountry.Rows.Count > 0)
                    {
                        string checkcountexists = await _countriesUploaderServices.GetCountryExist(dtCountry);
                        if (checkcountexists == "")
                        {
                            TempData["SuccessMessage"] = "File Data Imported Successfully";
                            return Ok();
                        }
                        else
                        {
                            TempData["ErrorMessage"] = $"Country are already in the database {checkcountexists}";
                            return BadRequest("Country are already in the database");
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "No data in the file";
                        return BadRequest("No data in the file.");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Issue in retreiving file data";
                    return BadRequest("Issue in retreiving file data.");
                }

            }
            else
            {
                TempData["ErrorMessage"] = "No file uploaded";
                return BadRequest("No file uploaded.");
            }
        }
    }
}
