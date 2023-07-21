using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.MemberAccess
{
    public class FavouriteModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IFavoriteRepository _favoriteRepository;

        public FavouriteModel(BusinessObjects.Models.HistoryHubContext context, IFavoriteRepository favoriteRepository)
        {
            _context = context;
            _favoriteRepository = favoriteRepository;
        }

        public IList<Timeline> Timeline { get; set; }
        public IList<Favorite> Favorite { get; set; }
        int i = 0;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return NotFound();
            }

            Favorite = await _context.Favorites.Where(u => u.UserId == userId).ToListAsync();

            Timeline = new List<Timeline>(); // Khởi tạo danh sách Timeline

            foreach (var favorite in Favorite)
            {
                Timeline timeline = await _context.Timelines.FirstOrDefaultAsync(t => t.TimelineId == favorite.TimelineId);
                if (timeline != null)
                {
                    Timeline.Add(timeline);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Favorite favorite)
        {
            if (favorite == null)
            {
                return NotFound();
            }

            _favoriteRepository.Delete(favorite.FavoriteId);

            return Page();
        }

    }
}