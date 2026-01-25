using FocusFlow.Domain.Entities;

namespace FocusFlow.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetAllByOwnerIdAsync(string ownerId, CancellationToken cancellationToken = default);
        Task<Project> CreateAsync(Project project, CancellationToken cancellationToken = default);
        Task UpdateAsync(Project project, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
