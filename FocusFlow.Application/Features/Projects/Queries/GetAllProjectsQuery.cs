using FocusFlow.Application.DTOs;
using MediatR;

namespace FocusFlow.Application.Features.Projects.Queries
{
    public record GetAllProjectsQuery(string OwnerId) : IRequest<IEnumerable<ProjectDto>>;
}
