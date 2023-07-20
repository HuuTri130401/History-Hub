using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.ComponentModel.DataAnnotations;

namespace HistoryHub.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public RegisterModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        public string FullName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public int roleId = 3;
        public bool status = true;
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string Address { get; set; }
        public DateTime cretedDate = DateTime.Now;

        public async Task<IActionResult> OnPostAsync()
        {
            User user = new User(Email, FullName, Password, roleId, status, Phone, Address, cretedDate);
            if (!ModelState.IsValid || userRepository.GetAll().ToList() == null || user == null)
            {
                return Page();
            }

            userRepository.Insert(user);
            TempData["RegisterSuccess"] = "Register Successfully";
            return RedirectToPage("/Account/Login");
        }
    }
}
