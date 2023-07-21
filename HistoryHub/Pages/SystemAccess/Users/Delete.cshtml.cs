using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories;

namespace HistoryHub.Pages.SystemAccess.Users
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IUserRepository _userRepository;

        public DeleteModel(HistoryHubContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _userRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            User = await _context.Users
             .AsNoTracking()
             .Include(t => t.Role)
             .FirstOrDefaultAsync(q => q.UserId == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, bool status)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var updateUser = await _userRepository.UpdateStatusUser(id, status);
            User user = _userRepository.GetById(id);

            if (updateUser)
            {
                if (user.RoleId == 2)
                {
                    TempData["EditSuccess"] = "InActive Successfully";
                    return RedirectToPage("./Editors");
                }
                else if (user.RoleId == 3)
                {
                    TempData["EditSuccess"] = "InActive Successfully";
                    return RedirectToPage("./Members");
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        //public async Task<IActionResult> OnPostAsync(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }
        //    var user = await _context.Users.FindAsync(id);

        //    if (user != null)
        //    {
        //        User = user;
        //        _context.Users.Remove(User);
        //        await _context.SaveChangesAsync();
        //    }

        //    if (User.RoleId == 2)
        //    {
        //        TempData["EditSuccess"] = "Delete Successfully";
        //        return RedirectToPage("./Editors");
        //    }
        //    else if (User.RoleId == 3)
        //    {
        //        TempData["EditSuccess"] = "Delete Successfully";
        //        return RedirectToPage("./Members");
        //    }
        //    return Page();
        //}
    }
}
