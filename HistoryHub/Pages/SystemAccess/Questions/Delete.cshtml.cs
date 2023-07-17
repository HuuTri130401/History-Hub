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
    public class DeleteModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly IQuestionRepository _questionRepository;

        public DeleteModel(HistoryHubContext context, IQuestionRepository questionRepository)
        {
            _context = context;
            _questionRepository = questionRepository;
        }

        [BindProperty]
        public Question Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _questionRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            Question = await _context.Questions
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

        public async Task<IActionResult> OnPostAsync(int? id, bool status)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }
            var updateQuestion = await _questionRepository.UpdateStatusQuestion(id, status);

            if (updateQuestion)
            {
                return RedirectToPage("./AdminManageQuestions");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
