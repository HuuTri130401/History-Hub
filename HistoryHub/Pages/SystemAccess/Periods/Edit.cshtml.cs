namespace HistoryHub.Pages.SystemAccess.Periods
{
    public class EditModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public EditModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Period Period { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Periods == null)
            {
                return NotFound();
            }

            var period = await _context.Periods.FirstOrDefaultAsync(m => m.PeriodId == id);
            if (period == null)
            {
                return NotFound();
            }
            Period = period;
            ViewData["CreateBy"] = new SelectList(_context.Users, "UserId", "Email");
            ViewData["TimelineId"] = new SelectList(_context.Timelines, "TimelineId", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Period).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodExists(Period.PeriodId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./AdminManagePeriods");
        }

        private bool PeriodExists(int id)
        {
            return (_context.Periods?.Any(e => e.PeriodId == id)).GetValueOrDefault();
        }
    }
}
