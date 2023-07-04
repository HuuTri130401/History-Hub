using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class QuizAttempt
    {
        public int AttemptId { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int Score { get; set; }
        public DateTime CompletionTime { get; set; }

        public virtual Quiz Quiz { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
