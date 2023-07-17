using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int TimelineId { get; set; }
        public int QuizId { get; set; }
        public string Comment { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Quiz? Quiz { get; set; } = null!;
        public virtual Timeline? Timeline { get; set; } = null!;
        public virtual User? User { get; set; } = null!;
    }
}
