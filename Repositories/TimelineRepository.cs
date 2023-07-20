using BusinessObjects.Models;

namespace Repositories
{
    public class TimelineRepository : GenericRepository<Timeline>, ITimelineRepository
    {
        public async Task<bool> UpdateTimlineStatus(int? timlineId, bool status)
        {
            {
                var timline = await _historyHubContext.Timelines.FindAsync(timlineId);

                if (timline != null)
                {
                    timline.Status = status;
                    await _historyHubContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }
    }
}
