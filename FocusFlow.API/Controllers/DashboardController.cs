using FocusFlow.Application.DTOs;
using FocusFlow.Application.Features.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.API.Controllers
{
    [Authorize]
    public class DashboardController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            var query = new GetDashboardQuery(GetUserId());
            var dashboard = await _mediator.Send(query);
            return Ok(dashboard);
        }
    }
}
