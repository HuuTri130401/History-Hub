using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
    {
        public async Task<bool> UpdateStatusQuiz(int? quizId, bool status)
        {
            {
                var quiz = await _historyHubContext.Quizzes.FindAsync(quizId);

                if (quiz != null)
                {
                    quiz.Status = status;
                    await _historyHubContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }
    }
}
