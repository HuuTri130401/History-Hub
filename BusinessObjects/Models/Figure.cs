using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Figure
    {
        public int FigureId { get; set; }
        public string FigureName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PeriodId { get; set; }
        public string Image { get; set; } = null!;
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User CreateByNavigation { get; set; } = null!;
        public virtual Period Period { get; set; } = null!;
    }
}
