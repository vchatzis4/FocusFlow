using FocusFlow.Domain.Enums;

namespace FocusFlow.Application.DTOs
{
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

    public record CreateTaskItemDto(
        string Title,
        string? Description,
        DateTime? DueDate,
        TaskPriority Priority,
        Guid ProjectId,
        string? AssignedUserId
    );

    public record UpdateTaskItemDto(
        string Title,
        string? Description,
        DateTime? DueDate,
        TaskItemStatus Status,
        TaskPriority Priority,
        string? AssignedUserId
    );
}
