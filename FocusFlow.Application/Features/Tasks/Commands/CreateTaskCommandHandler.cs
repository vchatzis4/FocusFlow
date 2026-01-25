using FocusFlow.Application.DTOs;
using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskItemDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskItemDto?> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(request.ProjectId, cancellationToken);

            if (project == null || project.OwnerId != request.OwnerId)
                return null;

            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Status = TaskItemStatus.Todo,
                Priority = request.Priority,
                ProjectId = request.ProjectId,
                AssignedUserId = request.AssignedUserId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Tasks.CreateAsync(task, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
