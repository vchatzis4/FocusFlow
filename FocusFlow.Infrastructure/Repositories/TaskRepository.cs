using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Enums;
using FocusFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems
                .Include(t => t.Project)
                .Where(t => t.ProjectId == projectId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TaskItem>> GetFilteredAsync(
            Guid? projectId = null,
            TaskItemStatus? status = null,
            TaskPriority? priority = null,
            string? assignedUserId = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.TaskItems
                .Include(t => t.Project)
                .AsQueryable();

            if (projectId.HasValue)
            {
                query = query.Where(t => t.ProjectId == projectId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(t => t.Priority == priority.Value);
            }

            if (!string.IsNullOrEmpty(assignedUserId))
            {
                query = query.Where(t => t.AssignedUserId == assignedUserId);
            }

            return await query
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<TaskItem> CreateAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            await _context.TaskItems.AddAsync(task, cancellationToken);
            return task;
        }

        public Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            _context.TaskItems.Update(task);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var task = await _context.TaskItems.FindAsync(new object[] { id }, cancellationToken);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
            }
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.TaskItems.AnyAsync(t => t.Id == id, cancellationToken);
        }
    }
}
