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
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IUserRepository _userRepository;

        public DetailsModel(HistoryHubContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public User User { get; set; } = default!;

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    else 
        //    {
        //        User = user;
        //    }
        //    return Page();
        //}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _userRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            User = await _context.Users
                .AsNoTracking()
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
