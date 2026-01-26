namespace FocusFlow.Web.Models
{
    public record ProjectDto(
        Guid Id,
        string Name,
        string? Description,
        string OwnerId,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        int TotalTasks,
        int CompletedTasks,
        int OverdueTasks
    );

    public record CreateProjectRequest(
        string Name,
        string? Description
    );

    public record UpdateProjectRequest(
        string Name,
        string? Description
    );
}
