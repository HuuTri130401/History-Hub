using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.ComponentModel.DataAnnotations;

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
        public void OnGet()
        {
        }
        public IActionResult OnPost()
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
                // Save user information to session
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("UserEmail", user.Email);
                if (user.RoleId == 1)
                {
                    HttpContext.Session.SetInt32("AdminRole", user.RoleId);
                    return RedirectToPage("/SystemAccess/AdminDashboard/Home");
                }
                if (user.RoleId == 2)
                {
                    HttpContext.Session.SetInt32("EditorRole", user.RoleId);
                    return RedirectToPage("/EditorAccess/Timelines/EditorManageTimelines");
                }
                if (user.RoleId == 3)
                {
                    HttpContext.Session.SetInt32("MemberRole", user.RoleId);
                    return RedirectToPage("/MemberAccess");
                }
                ViewData["Message"] = "Invalid email or password.";
            }
            return Page();

        }

    }
}
