using FocusFlow.Application.DTOs;
using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Queries
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskItemDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskItemDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken);

            if (task == null)
                return null;

            var project = await _unitOfWork.Projects.GetByIdAsync(task.ProjectId, cancellationToken);

            if (project == null || project.OwnerId != request.OwnerId)
                return null;

            return new TaskItemDto(
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
            );
        }
    }
}
