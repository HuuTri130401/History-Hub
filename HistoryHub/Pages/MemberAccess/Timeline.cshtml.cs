using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.MemberAccess
{
    public class TimelineModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private ITimelineRepository _timelineRepository;
        private IPeriodRepository _periodRepository;
        private IFeedbackRepository _feedbackRepository;

        public TimelineModel(BusinessObjects.Models.HistoryHubContext context, ITimelineRepository timelineRepository, IPeriodRepository periodRepository, IFeedbackRepository feedbackRepository)
        {
            _context = context;
            _timelineRepository = timelineRepository;
            _periodRepository = periodRepository;
            _feedbackRepository = feedbackRepository;
        }
        public IList<Timeline> Timelines { get; set; } = default!;
        public Timeline Timeline { get; set; } = default;
        public IList<Period> Period { get; set; } = default!;

        public IList<Period> AllPeriod { get; set; } = default!;

        public IList<Figure> Figures { get; set; } = default!;




        public async Task<IActionResult> OnGetAsync(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            // Lưu giá trị UserId vào biến View Data
            ViewData["UserId"] = userId;

            if (_context.Timelines != null)
            {
                Timelines = await _context.Timelines
                .Include(t => t.CreatedByNavigation).Where(s => s.Status == true)
                .ToListAsync();
            }

            if (_context.Periods != null)
            {
                AllPeriod = await _context.Periods
                .Include(t => t.CreateByNavigation).Where(s => s.Status == true).ToListAsync();
            }


            if (_context.Periods != null)
            {
                Period = await _context.Periods
                .Where(p => p.TimelineId == id).Where(s => s.Status == true).ToListAsync();

            }

            if (_context.Figures != null)
            {
                Figures = await _context.Figures
                .Include(t => t.CreateByNavigation).Where(s => s.Status == true).ToListAsync();

            }

            if (_timelineRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }
            if (id == 0)
            {
                id = 1;
            }

            Timeline = await _context.Timelines
                .AsNoTracking()
                .Include(u => u.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.TimelineId == id);

            if (Timeline == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int timelineId, int userId, string comment)
        {



            Feedback feedback = new Feedback
            {
                TimelineId = timelineId,
                UserId = userId,
                Comment = comment,
                Status = false,
                CreateDate = DateTime.Now
            };

            _feedbackRepository.Insert(feedback);
            return Page();

        }
    }
}
