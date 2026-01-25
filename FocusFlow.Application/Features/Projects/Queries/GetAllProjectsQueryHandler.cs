using FocusFlow.Application.DTOs;
using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Projects.Queries
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _unitOfWork.Projects.GetAllByOwnerIdAsync(request.OwnerId, cancellationToken);
            var result = new List<ProjectDto>();

            foreach (var project in projects)
            {
                var tasks = await _unitOfWork.Tasks.GetAllByProjectIdAsync(project.Id, cancellationToken);
                var taskList = tasks.ToList();

                result.Add(new ProjectDto(
                    project.Id,
                    project.Name,
                    project.Description,
                    project.OwnerId,
                    project.CreatedAt,
                    project.UpdatedAt,
                    TotalTasks: taskList.Count,
                    CompletedTasks: taskList.Count(t => t.Status == TaskItemStatus.Done),
                    OverdueTasks: taskList.Count(t => t.DueDate < DateTime.UtcNow && t.Status != TaskItemStatus.Done)
                ));
            }

            return result;
        }
    }
}
