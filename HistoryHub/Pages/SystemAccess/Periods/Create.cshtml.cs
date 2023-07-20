using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;

namespace HistoryHub.Pages.SystemAccess.Periods
{
    public class CreateModel : PageModel
    {

        private readonly IUserRepository _userRepository;
        private readonly ITimelineRepository _timelineRepository;
        private readonly IPeriodRepository _periodRepository;

        public CreateModel(IUserRepository userRepository, ITimelineRepository timelineRepository, IPeriodRepository periodRepository)
        {
            _userRepository = userRepository;
            _timelineRepository = timelineRepository;
            _periodRepository = periodRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["CreateBy"] = new SelectList(_userRepository.GetAll().ToList(), "UserId", "Email");
            ViewData["TimelineId"] = new SelectList(_timelineRepository.GetAll().ToList(), "TimelineId", "Title");
            return Page();
        }

        [BindProperty]
        public Period Period { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _periodRepository.GetAll() == null || Period == null)
            {
                return Page();
            }

            _periodRepository.Insert(Period);


            return RedirectToPage("./AdminManagePeriods");
        }
    }
}
