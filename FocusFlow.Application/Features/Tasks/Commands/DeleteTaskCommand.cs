using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands
{
    public record DeleteTaskCommand(Guid Id, string OwnerId) : IRequest<bool>;
}
