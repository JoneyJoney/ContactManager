using ContactManager.Application.DTOs;
using ContactManager.Application.Enums;
using ContactManager.Application.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountController(UserManager<ApplicationUsers> userManager,SignInManager<ApplicationUsers> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }
        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Login(LoginDTO? loginDTO,string? ReturnUrl)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                ApplicationUsers user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if(user != null)
                {
                    if(await _userManager.IsInRoleAsync(user,UserTypeOptions.Admin.ToString()))
                    {
                        return RedirectToAction(nameof(PersonController.Index), "Person");
                    }
                    if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                }
                return View(loginDTO);
            }
            ModelState.AddModelError("Login", "Invalid Login or Password");

            return View(loginDTO);
        }

        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return View(registerDTO);

            }

            ApplicationUsers user = new ApplicationUsers()
            {
                Email = registerDTO.EmailID,
                PersonName = registerDTO.PersonName,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.EmailID

            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if(result.Succeeded)
            {
                //Creating Role in User

                if(await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                {
                    ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.User.ToString() };
                    await _roleManager.CreateAsync(applicationRole);
                }

                await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                //
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(HomeController.Index), "Home");

            }
            return View(registerDTO);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [Authorize("NotAuthorized")]
        public async Task<IActionResult> IsEmailAlreadyExist(string  EmailID)
        {
            ApplicationUsers? user = await _userManager.FindByEmailAsync(EmailID);
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }
    }
}
