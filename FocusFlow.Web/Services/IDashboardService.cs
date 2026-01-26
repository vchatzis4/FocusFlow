using FocusFlow.Web.Models;

namespace FocusFlow.Web.Services
{
    public interface IDashboardService
    {
        Task<DashboardDto?> GetDashboardAsync();
    }
}
