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
    public class DetailsModel : PageModel
    {
        private readonly HistoryHubContext _historyHubContext;
        private readonly IQuizRepository _quizRepository;

        public DetailsModel(HistoryHubContext historyHubContext, IQuizRepository quizRepository)
        {
            _historyHubContext = historyHubContext;
            _quizRepository = quizRepository;
        }

        public Quiz Quiz { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _quizRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            Quiz = await _historyHubContext.Quizzes
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
    }
}
