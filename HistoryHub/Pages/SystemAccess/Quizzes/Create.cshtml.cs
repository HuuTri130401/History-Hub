using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Repositories;

namespace HistoryHub.Pages.SystemAccess.Quizzes
{
    public class CreateModel : PageModel
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITimelineRepository _timelineRepository;

        public CreateModel(IQuizRepository quizRepository, IUserRepository userRepository, ITimelineRepository timelineRepository)
        {
            _quizRepository = quizRepository;
            _userRepository = userRepository;
            _timelineRepository = timelineRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["CreatedBy"] = new SelectList(_userRepository.GetAll().ToList(), "UserId", "Email");
            ViewData["TimelineId"] = new SelectList(_timelineRepository.GetAll().ToList(), "TimelineId", "Title");
            return Page();
        }

        [BindProperty]
        public Quiz Quiz { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _quizRepository.GetAll() == null || Quiz == null)
            {
                return Page();
            }

            _quizRepository.Insert(Quiz);

            return RedirectToPage("./AdminManageQuizzes");
        }
    }
}
