using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HistoryHub.Pages.EditorAccess.Figures
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
            ViewData["PeriodId"] = new SelectList(_context.Periods, "PeriodId", "PeriodName");
            return Page();
        }

        [BindProperty]
        public Figure Figure { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || _context.Figures == null || Figure == null)
            //{
            //    return Page();
            //}

            _context.Figures.Add(Figure);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
