using System.Net.Http.Json;
using FocusFlow.Web.Models;

namespace FocusFlow.Web.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DashboardDto?> GetDashboardAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DashboardDto>("api/dashboard");
            }
            catch
            {
                return null;
            }
        }
    }
}
