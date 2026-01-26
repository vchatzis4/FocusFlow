using FocusFlow.Web.Models;

namespace FocusFlow.Web.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(Guid id);
        Task<ProjectDto?> CreateAsync(CreateProjectRequest request);
        Task<ProjectDto?> UpdateAsync(Guid id, UpdateProjectRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
