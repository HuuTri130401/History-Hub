using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace HistoryHub.Pages.EditorAccess.Favorites
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public DetailsModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

      public Favorite Favorite { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Favorites == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorites.FirstOrDefaultAsync(m => m.FavoriteId == id);
            if (favorite == null)
            {
                return NotFound();
            }
            else 
            {
                Favorite = favorite;
            }
            return Page();
        }
    }
}
