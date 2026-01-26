using FocusFlow.Web.Models;

namespace FocusFlow.Web.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetFilteredAsync(Guid? projectId = null, TaskItemStatus? status = null, TaskPriority? priority = null);
        Task<TaskItemDto?> GetByIdAsync(Guid id);
        Task<TaskItemDto?> CreateAsync(CreateTaskRequest request);
        Task<TaskItemDto?> UpdateAsync(Guid id, UpdateTaskRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
