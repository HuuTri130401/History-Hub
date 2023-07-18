using BusinessObjects.Models;

namespace Repositories
{
    public interface ITimelineRepository : IGenericRepository<Timeline>
    {
        Task<bool> UpdateTimlineStatus(int? timlineId, bool status);
    }
}
