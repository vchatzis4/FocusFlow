using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Enums;

namespace FocusFlow.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TaskItem>> GetFilteredAsync(
            Guid? projectId = null,
            TaskItemStatus? status = null,
            TaskPriority? priority = null,
            string? assignedUserId = null,
            CancellationToken cancellationToken = default);
        Task<TaskItem> CreateAsync(TaskItem task, CancellationToken cancellationToken = default);
        Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
