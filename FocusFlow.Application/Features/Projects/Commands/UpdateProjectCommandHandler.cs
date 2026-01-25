using FocusFlow.Application.DTOs;
using FocusFlow.Application.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Projects.Commands
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectDto?> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(request.Id, cancellationToken);

            if (project == null || project.OwnerId != request.OwnerId)
                return null;

            project.Name = request.Name;
            project.Description = request.Description;
            project.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Projects.UpdateAsync(project, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var tasks = await _unitOfWork.Tasks.GetAllByProjectIdAsync(project.Id, cancellationToken);
            var taskList = tasks.ToList();

            return new ProjectDto(
                project.Id,
                project.Name,
                project.Description,
                project.OwnerId,
                project.CreatedAt,
                project.UpdatedAt,
                TotalTasks: taskList.Count,
                CompletedTasks: taskList.Count(t => t.Status == Domain.Enums.TaskItemStatus.Done),
                OverdueTasks: taskList.Count(t => t.DueDate < DateTime.UtcNow && t.Status != Domain.Enums.TaskItemStatus.Done)
            );
        }
    }
}
