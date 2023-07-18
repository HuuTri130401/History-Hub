using BusinessObjects.Models;

namespace Repositories
{
    public interface IFigureRepository : IGenericRepository<Figure>
    {
        Task<bool> UpdateFigureStatus(int? figureId, bool status);
    }
}
