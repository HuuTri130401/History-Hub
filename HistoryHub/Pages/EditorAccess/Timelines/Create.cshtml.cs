using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HistoryHub.Pages.EditorAccess.Timelines
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
            ViewData["CreatedBy"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Timeline Timeline { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || _context.Timelines == null || Timeline == null)
            //  {
            //      return Page();
            //  }

            _context.Timelines.Add(Timeline);
            await _context.SaveChangesAsync();

            return RedirectToPage("./EditorManageTimlines");
        }
    }
}
