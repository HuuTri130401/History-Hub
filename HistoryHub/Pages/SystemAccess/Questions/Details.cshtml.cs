using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories;

namespace HistoryHub.Pages.SystemAccess.Questions
{
    public class DetailsModel : PageModel
    {
        private readonly HistoryHubContext _historyHubContext;
        private readonly IQuestionRepository _questionRepository;

        public DetailsModel(HistoryHubContext historyHubContext, IQuestionRepository questionRepository)
        {
            _historyHubContext = historyHubContext;
            _questionRepository = questionRepository;
        }

        public Question Question { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _questionRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            Question = await _historyHubContext.Questions
                .AsNoTracking()
                .Include(u => u.CreatedByNavigation)
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(qu => qu.QuestionId == id);

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
