using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public async Task<bool> UpdateStatusQuestion(int? questionId, bool status)
        {
            {
                var question = await _historyHubContext.Questions.FindAsync(questionId);

                if (question != null)
                {
                    question.Status = status;
                    await _historyHubContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }
    }
}
