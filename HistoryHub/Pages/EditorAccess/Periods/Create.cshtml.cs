using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HistoryHub.Pages.EditorAccess.Periods
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
            ViewData["CreateBy"] = new SelectList(_context.Users, "UserId", "Email");
            ViewData["TimelineId"] = new SelectList(_context.Timelines, "TimelineId", "Title");
            return Page();
        }

        [BindProperty]
        public Period Period { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || _context.Periods == null || Period == null)
            //{
            //    return Page();
            //}

            _context.Periods.Add(Period);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
