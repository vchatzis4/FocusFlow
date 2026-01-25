using FocusFlow.Application.DTOs;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands
{
    public record UpdateTaskCommand(
        Guid Id,
        string Title,
        string? Description,
        DateTime? DueDate,
        TaskItemStatus Status,
        TaskPriority Priority,
        string? AssignedUserId,
        string OwnerId
    ) : IRequest<TaskItemDto?>;
}
