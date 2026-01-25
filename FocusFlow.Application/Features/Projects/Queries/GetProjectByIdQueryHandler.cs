using FocusFlow.Application.DTOs;
using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Projects.Queries
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(request.Id, cancellationToken);

            if (project == null || project.OwnerId != request.OwnerId)
                return null;

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
                CompletedTasks: taskList.Count(t => t.Status == TaskItemStatus.Done),
                OverdueTasks: taskList.Count(t => t.DueDate < DateTime.UtcNow && t.Status != TaskItemStatus.Done)
            );
        }
    }
}
