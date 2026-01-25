namespace FocusFlow.Application.DTOs
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

    public record CreateProjectDto(
        string Name,
        string? Description
    );

    public record UpdateProjectDto(
        string Name,
        string? Description
    );
}
