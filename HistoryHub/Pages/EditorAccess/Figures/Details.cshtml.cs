﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace HistoryHub.Pages.EditorAccess.Figures
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.Models.HistoryHubContext _context;

        public DetailsModel(BusinessObjects.Models.HistoryHubContext context)
        {
            _context = context;
        }

      public Figure Figure { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Figures == null)
            {
                return NotFound();
            }

            var figure = await _context.Figures.FirstOrDefaultAsync(m => m.FigureId == id);
            if (figure == null)
            {
                return NotFound();
            }
            else 
            {
                Figure = figure;
            }
            return Page();
        }
    }
}
