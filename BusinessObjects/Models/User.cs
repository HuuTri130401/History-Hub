using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class User
    {
        public User()
        {
            Favorites = new HashSet<Favorite>();
            Feedbacks = new HashSet<Feedback>();
            Figures = new HashSet<Figure>();
            Periods = new HashSet<Period>();
            Questions = new HashSet<Question>();
            QuizAttempts = new HashSet<QuizAttempt>();
            Quizzes = new HashSet<Quiz>();
            Timelines = new HashSet<Timeline>();
        }

        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public int Score { get; set; }
        public bool Status { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Figure> Figures { get; set; }
        public virtual ICollection<Period> Periods { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Timeline> Timelines { get; set; }
    }
}
