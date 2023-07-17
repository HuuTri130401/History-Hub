using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Period
    {
        public Period()
        {
            Figures = new HashSet<Figure>();
        }

        public int PeriodId { get; set; }
        public string PeriodName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TimelineId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public bool Status { get; set; }

        public virtual User? CreateByNavigation { get; set; } = null!;
        public virtual Timeline? Timeline { get; set; } = null!;
        public virtual ICollection<Figure> Figures { get; set; }
    }
}
