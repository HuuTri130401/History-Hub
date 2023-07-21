using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.MemberAccess
{
    public class UsersProfileModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IUserRepository _userRepository;

        public UsersProfileModel(BusinessObjects.Models.HistoryHubContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public bool status = true;
        public DateTime cretedDate = DateTime.Now;
        [BindProperty]
        public User User { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (_context.Users != null)
            {
                User = _userRepository.GetById(userId);
            }

            if (id == null || _context.Users == null)
            {
                id = userId;
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return Page();


        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            User = new User(User.UserId, User.Email, User.FullName, User.Password, User.RoleId, status, User.Phone, User.Address, cretedDate);
            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Page();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
