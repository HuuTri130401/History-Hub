using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Favorite
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int TimelineId { get; set; }
        public int QuizId { get; set; }

        public virtual Quiz? Quiz { get; set; } = null!;
        public virtual Timeline? Timeline { get; set; } = null!;
        public virtual User? User { get; set; } = null!;
    }
}
