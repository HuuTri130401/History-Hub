using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;

namespace HistoryHub.Pages.SystemAccess.Timelines
{
    public class CreateModel : PageModel
    {

        private readonly IUserRepository _userRepository;
        private readonly ITimelineRepository _timelineRepository;

        public CreateModel(IUserRepository userRepository, ITimelineRepository timelineRepository)
        {
            _userRepository = userRepository;
            _timelineRepository = timelineRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["CreatedBy"] = new SelectList(_userRepository.GetAll().ToList(), "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Timeline Timeline { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _timelineRepository.GetAll() == null || Timeline == null)
            {
                return Page();
            }

            _timelineRepository.Insert(Timeline);


            return RedirectToPage("/SystemAccess/Timelines/AdminManageTimelines");
        }
    }
}
