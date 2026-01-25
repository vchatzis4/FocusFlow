using FocusFlow.Application.DTOs;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands
{
    public record CreateTaskCommand(
        string Title,
        string? Description,
        DateTime? DueDate,
        TaskPriority Priority,
        Guid ProjectId,
        string? AssignedUserId,
        string OwnerId
    ) : IRequest<TaskItemDto?>;
}
