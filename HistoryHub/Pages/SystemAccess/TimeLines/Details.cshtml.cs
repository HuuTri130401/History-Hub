﻿using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace HistoryHub.Pages.SystemAccess.Timelines
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;
        private readonly ITimelineRepository _timelineRepository;

        public DetailsModel(BusinessObjects.Models.HistoryHubContext context, ITimelineRepository timelineRepository)
        {
            _context = context;
            _timelineRepository = timelineRepository;
        }

        public Timeline Timeline { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _timelineRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            Timeline = await _context.Timelines
                .AsNoTracking()
                .Include(u => u.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.TimelineId == id);
            if (Timeline == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
