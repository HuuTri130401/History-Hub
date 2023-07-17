using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories;

namespace HistoryHub.Pages.SystemAccess.Quizzes
{
    public class DeleteModel : PageModel
    {
        private readonly HistoryHubContext _context;
        private readonly IQuizRepository _quizRepository;

        public DeleteModel(IQuizRepository quizRepository, HistoryHubContext context)
        {
            _quizRepository = quizRepository;
            _context = context;
        }

        [BindProperty]
      public Quiz Quiz { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _quizRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            Quiz = await _context.Quizzes
                .AsNoTracking()
                .Include(u => u.CreatedByNavigation)
                .Include(t => t.Timeline)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (Quiz == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, bool status)
        {
            if (id == null || _context.Quizzes == null)
            {
                return NotFound();
            }
            var updatQuiz = await _quizRepository.UpdateStatusQuiz(id, status);

            if (updatQuiz)
            {
                return RedirectToPage("./AdminManageQuizzes");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
