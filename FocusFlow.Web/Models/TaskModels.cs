namespace FocusFlow.Web.Models
{
    public enum TaskItemStatus
    {
        Todo = 0,
        InProgress = 1,
        Done = 2
    }

    public enum TaskPriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Urgent = 3
    }

    public record TaskItemDto(
        Guid Id,
        string Title,
        string? Description,
        DateTime? DueDate,
        TaskItemStatus Status,
        TaskPriority Priority,
        Guid ProjectId,
        string? AssignedUserId,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        bool IsOverdue
    );

    public record CreateTaskRequest(
        string Title,
        string? Description,
        DateTime? DueDate,
        TaskPriority Priority,
        Guid ProjectId,
        string? AssignedUserId
    );

    public record UpdateTaskRequest(
        string Title,
        string? Description,
        DateTime? DueDate,
        TaskItemStatus Status,
        TaskPriority Priority,
        string? AssignedUserId
    );
}
