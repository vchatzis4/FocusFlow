using FocusFlow.Application.DTOs;
using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Dashboard
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDashboardQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var projects = await _unitOfWork.Projects.GetAllByOwnerIdAsync(request.OwnerId, cancellationToken);
            var projectList = projects.ToList();

            var projectStats = new List<ProjectStatsDto>();
            var totalTasks = 0;
            var totalCompleted = 0;
            var totalOverdue = 0;
            var totalInProgress = 0;

            foreach (var project in projectList)
            {
                var tasks = await _unitOfWork.Tasks.GetAllByProjectIdAsync(project.Id, cancellationToken);
                var taskList = tasks.ToList();

                var completed = taskList.Count(t => t.Status == TaskItemStatus.Done);
                var overdue = taskList.Count(t => t.DueDate < DateTime.UtcNow && t.Status != TaskItemStatus.Done);
                var inProgress = taskList.Count(t => t.Status == TaskItemStatus.InProgress);

                projectStats.Add(new ProjectStatsDto(
                    project.Id,
                    project.Name,
                    TotalTasks: taskList.Count,
                    CompletedTasks: completed,
                    OverdueTasks: overdue,
                    InProgressTasks: inProgress
                ));

                totalTasks += taskList.Count;
                totalCompleted += completed;
                totalOverdue += overdue;
                totalInProgress += inProgress;
            }

            return new DashboardDto(
                TotalProjects: projectList.Count,
                TotalTasks: totalTasks,
                CompletedTasks: totalCompleted,
                OverdueTasks: totalOverdue,
                InProgressTasks: totalInProgress,
                ProjectStats: projectStats
            );
        }
    }
}
