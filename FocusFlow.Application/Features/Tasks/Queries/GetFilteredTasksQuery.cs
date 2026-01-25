using FocusFlow.Application.DTOs;
using FocusFlow.Domain.Enums;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Queries
{
    public record GetFilteredTasksQuery(
        string OwnerId,
        Guid? ProjectId = null,
        TaskItemStatus? Status = null,
        TaskPriority? Priority = null
    ) : IRequest<IEnumerable<TaskItemDto>>;
}
