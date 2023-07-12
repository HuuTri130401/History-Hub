using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public string Question1 { get; set; } = null!;
        public string FalseAnswer1 { get; set; } = null!;
        public string FalseAnswer2 { get; set; } = null!;
        public string FalseAnswer3 { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public int NumberOfQuestion { get; set; }
        public int CreatedBy { get; set; }
        public int QuizId { get; set; }
        public bool Status { get; set; }

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual Quiz Quiz { get; set; } = null!;
    }
}
