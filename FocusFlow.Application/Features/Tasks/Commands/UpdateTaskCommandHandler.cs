using FocusFlow.Application.DTOs;
using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskItemDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskItemDto?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken);

            if (task == null)
                return null;

            var project = await _unitOfWork.Projects.GetByIdAsync(task.ProjectId, cancellationToken);

            if (project == null || project.OwnerId != request.OwnerId)
                return null;

            task.Title = request.Title;
            task.Description = request.Description;
            task.DueDate = request.DueDate;
            task.Status = request.Status;
            task.Priority = request.Priority;
            task.AssignedUserId = request.AssignedUserId;
            task.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Tasks.UpdateAsync(task, cancellationToken);
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
