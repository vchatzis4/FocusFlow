using FocusFlow.Application.DTOs;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Queries
{
    public record GetTaskByIdQuery(Guid Id, string OwnerId) : IRequest<TaskItemDto?>;
}
