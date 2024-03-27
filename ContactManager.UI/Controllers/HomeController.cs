using ContactManager.Application.ServicesInterface;
using ContactManager.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactManager.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IErrorHandleService _errorHandleService;

        public HomeController(ILogger<HomeController> logger,IErrorHandleService errorHandleService)
        {
            _logger = logger;
            _errorHandleService = errorHandleService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception or perform any necessary actions
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                _errorHandleService.InsertLogIntoDatabaseTable(ex);
                // Optionally, you might choose to rethrow the exception if needed
                throw;
            }
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if(errorDetails != null && errorDetails.Error != null)
            {
                ViewBag.ErrorMessage = errorDetails.Error;
            }
            return View();
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
