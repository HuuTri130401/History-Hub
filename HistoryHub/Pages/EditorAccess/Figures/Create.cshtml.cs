using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;

namespace HistoryHub.Pages.EditorAccess.Figures
{
    public class CreateModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IPeriodRepository _periodRepository;
        private readonly IFigureRepository _figureRepository;

        public CreateModel(IUserRepository userRepository, IPeriodRepository periodRepository, IFigureRepository figureRepository)
        {
            _userRepository = userRepository;
            _periodRepository = periodRepository;
            _figureRepository = figureRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["CreateBy"] = new SelectList(_userRepository.GetAll().ToList(), "UserId", "Email");
            ViewData["PeriodId"] = new SelectList(_periodRepository.GetAll().ToList(), "PeriodId", "PeriodName");
            return Page();
        }

        [BindProperty]
        public Figure Figure { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _figureRepository.GetAll() == null || Figure == null)
            {
                return Page();
            }

            _figureRepository.Insert(Figure);


            return RedirectToPage("/EditorAccess/Figures/EditorManageFigures");
        }
    }
}
