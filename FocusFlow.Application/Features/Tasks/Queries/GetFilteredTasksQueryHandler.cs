using FocusFlow.Application.DTOs;
using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Queries
{
    public class GetFilteredTasksQueryHandler : IRequestHandler<GetFilteredTasksQuery, IEnumerable<TaskItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFilteredTasksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaskItemDto>> Handle(GetFilteredTasksQuery request, CancellationToken cancellationToken)
        {
            var projects = await _unitOfWork.Projects.GetAllByOwnerIdAsync(request.OwnerId, cancellationToken);
            var projectIds = projects.Select(p => p.Id).ToHashSet();

            var tasks = await _unitOfWork.Tasks.GetFilteredAsync(
                request.ProjectId,
                request.Status,
                request.Priority,
                cancellationToken: cancellationToken
            );

            var filteredTasks = tasks.Where(t => projectIds.Contains(t.ProjectId));

            return filteredTasks.Select(task => new TaskItemDto(
                task.Id,
                task.Title,
                task.Description,
                task.DueDate,
                task.Status,
                task.Priority,
                task.ProjectId,
                task.AssignedUserId,
                task.CreatedAt,
                task.UpdatedAt,
                IsOverdue: task.DueDate < DateTime.UtcNow && task.Status != TaskItemStatus.Done
            ));
        }
    }
}
