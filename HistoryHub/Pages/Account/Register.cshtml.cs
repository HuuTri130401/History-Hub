using BusinessObjects.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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
        [MaxLength(50, ErrorMessage = "Your FullName can be up to 50 characters long")]
        public string FullName { get; set; }
        [BindProperty]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int roleId = 3;

        public bool status = true;
        [BindProperty]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number format.")]
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
