using FocusFlow.Application.DTOs;
using MediatR;

namespace FocusFlow.Application.Features.Projects.Queries
{
    public record GetProjectByIdQuery(Guid Id, string OwnerId) : IRequest<ProjectDto?>;
}
