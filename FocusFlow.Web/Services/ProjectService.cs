using System.Net.Http.Json;
using FocusFlow.Web.Models;

namespace FocusFlow.Web.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;

        public ProjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDto>>("api/projects");
                return result ?? Enumerable.Empty<ProjectDto>();
            }
            catch
            {
                return Enumerable.Empty<ProjectDto>();
            }
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ProjectDto>($"api/projects/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<ProjectDto?> CreateAsync(CreateProjectRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/projects", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProjectDto>();
        }

        public async Task<ProjectDto?> UpdateAsync(Guid id, UpdateProjectRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/projects/{id}", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProjectDto>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/projects/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
