using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Timeline
    {
        public Timeline()
        {
            Favorites = new HashSet<Favorite>();
            Feedbacks = new HashSet<Feedback>();
            Periods = new HashSet<Period>();
            Quizzes = new HashSet<Quiz>();
        }

        public int TimelineId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int NumberOfFavorite { get; set; }
        public bool Status { get; set; }
        public virtual User? CreatedByNavigation { get; set; } = null!;
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Period> Periods { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
