namespace FocusFlow.Application.DTOs
{
    public record DashboardDto(
        int TotalProjects,
        int TotalTasks,
        int CompletedTasks,
        int OverdueTasks,
        int InProgressTasks,
        IEnumerable<ProjectStatsDto> ProjectStats
    );

    public record ProjectStatsDto(
        Guid ProjectId,
        string ProjectName,
        int TotalTasks,
        int CompletedTasks,
        int OverdueTasks,
        int InProgressTasks
    );
}
