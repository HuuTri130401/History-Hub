using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.MemberAccess
{
    public class FeedbackModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackModel(BusinessObjects.Models.HistoryHubContext context, IFeedbackRepository feedbackRepository)
        {
            _context = context;
            _feedbackRepository = feedbackRepository;
        }

        public IList<Timeline> Timeline { get; set; }
        public IList<Feedback> Feedbacks { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return NotFound();
            }

            Feedbacks = await _context.Feedbacks.Where(u => u.UserId == userId).ToListAsync();

            foreach (var feedback in Feedbacks)
            {
                Timeline = await _context.Timelines.Where(t => t.TimelineId == feedback.TimelineId).ToListAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _feedbackRepository.Delete(id);


            return RedirectToPage("/MemberAccess/Timeline");


        }
    }
}
