using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Entities;
using FocusFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Project>> GetAllByOwnerIdAsync(string ownerId, CancellationToken cancellationToken = default)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.OwnerId == ownerId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Project> CreateAsync(Project project, CancellationToken cancellationToken = default)
        {
            await _context.Projects.AddAsync(project, cancellationToken);
            return project;
        }

        public Task UpdateAsync(Project project, CancellationToken cancellationToken = default)
        {
            _context.Projects.Update(project);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await _context.Projects.FindAsync(new object[] { id }, cancellationToken);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id, cancellationToken);
        }
    }
}
