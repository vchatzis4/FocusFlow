using FocusFlow.Application.DTOs;
using MediatR;

namespace FocusFlow.Application.Features.Dashboard
{
    public record GetDashboardQuery(string OwnerId) : IRequest<DashboardDto>;
}
