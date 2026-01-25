using MediatR;

namespace FocusFlow.Application.Features.Projects.Commands
{
    public record DeleteProjectCommand(Guid Id, string OwnerId) : IRequest<bool>;
}
