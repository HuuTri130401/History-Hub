using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            Favorites = new HashSet<Favorite>();
            Feedbacks = new HashSet<Feedback>();
            Questions = new HashSet<Question>();
            QuizAttempts = new HashSet<QuizAttempt>();
        }

        public int QuizId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int MaxScore { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CompletionTime { get; set; }
        public int NumberOfFeedback { get; set; }
        public int NumberOfFavorite { get; set; }
        public int NumberOfQuestion { get; set; }
        public int TimelineId { get; set; }

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual Timeline Timeline { get; set; } = null!;
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
    }
}
