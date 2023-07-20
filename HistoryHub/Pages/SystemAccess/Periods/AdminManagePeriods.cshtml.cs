namespace HistoryHub.Pages.SystemAccess.Periods
{
    public class AdminManagePeriodsModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public AdminManagePeriodsModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

        public IList<Period> Period { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Periods != null)
            {
                Period = await _context.Periods
                .Include(p => p.CreateByNavigation)
                .Include(p => p.Timeline).ToListAsync();
            }
        }
    }
}
