using System.Net.Http.Json;
using FocusFlow.Web.Models;

namespace FocusFlow.Web.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TaskItemDto>> GetFilteredAsync(
            Guid? projectId = null,
            TaskItemStatus? status = null,
            TaskPriority? priority = null)
        {
            try
            {
                var queryParams = new List<string>();

                if (projectId.HasValue)
                    queryParams.Add($"projectId={projectId.Value}");
                if (status.HasValue)
                    queryParams.Add($"status={status.Value}");
                if (priority.HasValue)
                    queryParams.Add($"priority={priority.Value}");

                var query = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
                var result = await _httpClient.GetFromJsonAsync<IEnumerable<TaskItemDto>>($"api/tasks{query}");

                return result ?? Enumerable.Empty<TaskItemDto>();
            }
            catch
            {
                return Enumerable.Empty<TaskItemDto>();
            }
        }

        public async Task<TaskItemDto?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TaskItemDto>($"api/tasks/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<TaskItemDto?> CreateAsync(CreateTaskRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tasks", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<TaskItemDto>();
        }

        public async Task<TaskItemDto?> UpdateAsync(Guid id, UpdateTaskRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tasks/{id}", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<TaskItemDto>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
