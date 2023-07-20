using BusinessObjects.Models;

namespace Repositories
{
    public interface IPeriodRepository : IGenericRepository<Period>
    {
        Task<bool> UpdatePeriodStatus(int? periodId, bool status);
    }
}
