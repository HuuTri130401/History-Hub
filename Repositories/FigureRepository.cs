using BusinessObjects.Models;

namespace Repositories
{
    public class FigureRepository : GenericRepository<Figure>, IFigureRepository
    {
        public async Task<bool> UpdateFigureStatus(int? figureId, bool status)
        {
            {
                var figure = await _historyHubContext.Figures.FindAsync(figureId);

                if (figure != null)
                {
                    figure.Status = status;
                    await _historyHubContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }
    }
}
