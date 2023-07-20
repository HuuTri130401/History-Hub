using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Repositories;
using System.ComponentModel.DataAnnotations;

namespace HistoryHub.Pages.SystemAccess.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserRepository userRepository;
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public CreateModel(IUserRepository userRepository, HistoryHubContext context)
        {
            this.userRepository = userRepository;
            _context = context;
        }

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        public string FullName { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public bool status = true;
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string Address { get; set; }
        public DateTime cretedDate = DateTime.Now;

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet()
        {
            var roles = _context.Roles.Where(r => r.RoleName == "Editor" || r.RoleName == "Member");
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User user = new User(Email, FullName, Password, User.RoleId, status, Phone, Address, cretedDate);
            if (userRepository.GetAll().ToList() == null || user == null)
            {
                return Page();
            }
            userRepository.Insert(user);
            TempData["NewUserSuccess"] = "New Successfully";
            if(User.RoleId == 2)
            {
                return RedirectToPage("Editors");
            }
            if (User.RoleId == 3)
            {
                return RedirectToPage("Members");
            }
            return Page();
        }
    }
}
