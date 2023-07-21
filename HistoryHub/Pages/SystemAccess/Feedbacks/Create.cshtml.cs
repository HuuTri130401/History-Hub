using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HistoryHub.Pages.SystemAccess.Feedbacks
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public CreateModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Description");
            ViewData["TimelineId"] = new SelectList(_context.Timelines, "TimelineId", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Feedbacks == null || Feedback == null)
            {
                return Page();
            }

            _context.Feedbacks.Add(Feedback);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
