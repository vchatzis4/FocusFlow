using FocusFlow.Application.DTOs;
using MediatR;

namespace FocusFlow.Application.Features.Projects.Commands
{
    public record CreateProjectCommand(
        string Name,
        string? Description,
        string OwnerId
    ) : IRequest<ProjectDto>;
}
