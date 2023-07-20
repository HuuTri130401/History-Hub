using BusinessObjects.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HistoryHub.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public LoginModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User user = userRepository.getUserByEmailAndPassword(Email, Password);

            if (user == null)
            {
                ViewData["Message"] = "Invalid Email or Password";
                return Page();
            }
            else
            {
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = RememberMe
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // Save user information to session
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetInt32("HasRole", user.RoleId);

                if (user.RoleId == 1)
                {
                    return RedirectToPage("/SystemAccess/AdminDashboard/Home");
                }
                if (user.RoleId == 2)
                {
                    HttpContext.Session.SetInt32("EditorRole", user.RoleId);
                    return RedirectToPage("/EditorAccess/Timelines/EditorManageTimelines");
                }
                if (user.RoleId == 3)
                {
                    return RedirectToPage("/GuestAccess/Home");
                }
                ViewData["Message"] = "Invalid email or password.";
            }
            return Page();

        }

    }
}