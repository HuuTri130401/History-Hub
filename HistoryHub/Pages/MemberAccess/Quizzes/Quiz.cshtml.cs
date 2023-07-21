using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;
using static System.Formats.Asn1.AsnWriter;

namespace HistoryHub.Pages.MemberAccess.DoQuiz
{
    public class QuizModel : PageModel
    {
        private readonly HistoryHubContext _historyHubContext;
        private readonly IQuizRepository _quizRepository;

        public QuizModel(HistoryHubContext historyHubContext, IQuizRepository quizRepository)
        {
            _historyHubContext = historyHubContext;
            _quizRepository = quizRepository;
        }

        public Quiz Quiz { get; set; }
        public List<Question> Questions { get; set; }
        public int Score { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            //Quiz = await _historyHubContext.Quizzes.FindAsync(id);
            Quiz = _quizRepository.GetById(id);

            if (Quiz == null)
            {
                return NotFound();
            }

            Questions = await _historyHubContext.Questions.Where(q => q.QuizId == id).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Quiz = _quizRepository.GetById(id);

            if (Quiz == null)
            {
                return NotFound();
            }

            Questions = await _historyHubContext.Questions.Where(q => q.QuizId == id).ToListAsync();

            int correctAnswers = 0;

            for (int i = 0; i < Questions.Count; i++)
            {
                string selectedAnswer = Request.Form["answer_" + Questions[i].QuestionId];

                if (selectedAnswer == Questions[i].CorrectAnswer)
                {
                    correctAnswers++;
                }
            }

            Score = (int)Math.Round((double)correctAnswers / Questions.Count * 100);

            return Page();
        }
    }

}
