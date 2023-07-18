using BusinessObjects.Models;

namespace Repositories
{
    public class PeriodRepository : GenericRepository<Period>, IPeriodRepository
    {
        public async Task<bool> UpdatePeriodStatus(int? periodId, bool status)
        {
            {
                var period = await _historyHubContext.Periods.FindAsync(periodId);

                if (period != null)
                {
                    period.Status = status;
                    await _historyHubContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }
    }
}
