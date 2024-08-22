using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.IO;
using Elfie.Serialization;
using Microsoft.AspNetCore.Http;
using HackatonGroup3_Web.Controllers;
using HackatonGroup3_Web.Models;
using Microsoft.AspNetCore.Identity;


namespace HackatonGroup3_Web.Controllers
{

    public class AccountController : Controller
    {
        #region Proprietés
        private readonly IEmailSender _sender;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
       
        private readonly UrlEncoder _urlEncoder;
        public object Enconding { get; private set; }
       
        #endregion

       
		#region CTOR
		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender,
			UrlEncoder urlEncoder, IEmailSender sender
            )
		{

			_userManager = userManager;
			_signInManager = signInManager;
			_urlEncoder = urlEncoder;
            _sender = sender;
        }
		#endregion
        //Inscription of user
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {

			return View();
        }

       
		[HttpPost]
        
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnurl = null)
        {        
           
          
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    UserName = model.Email,
                    Email = model.Email,
                   //Password = model.Password,
                   //ConfirmPassword=model.Password
                };

                var PasswordError = new List<string>();

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded == false)
                {
                    foreach (var err in result.Errors)
                    {
                        PasswordError.Add(err.Description);
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }

                if (result.Succeeded)
                {
              
					try
					{
                       
                        return RedirectToAction(nameof(Login));
     
                    }
                    catch (HttpRequestException ex)
                    {
                      
						return View("Internet");


                    }
                }


            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Internet()
        {

            return View();
        }
        [HttpGet]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }

        public string statusMessage { get; set; }
        [HttpGet]
        // Confirmation de mail aucours de l'inscription
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {

			var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {

				return View("Error");
            }
			code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
			var result = await _userManager.ConfirmEmailAsync(user, code);
            statusMessage = result.Succeeded ? "ConfirmEmail" : "Error confirm your mail";
			return View();

        }
        


        #region LOgin
        [HttpGet]
        [AllowAnonymous]
        //[EnableCors("securePolicy")]
        public IActionResult Login(string? returnurl = null)
        {
			
			ViewData["ReturnUrl"] = returnurl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnurl)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnurl) && Url.IsLocalUrl(returnurl))
                    {
                        return Redirect(returnurl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (result.IsLockedOut)
                {
                    // Handle locked out scenario
                    ModelState.AddModelError("", "This account has been locked out. Please try again later.");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed; redisplay the form
            return View(model);
        }

        #endregion

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
		

			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(AccountController.Login));

        }
    }
}