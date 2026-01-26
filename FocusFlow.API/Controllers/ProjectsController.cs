using FocusFlow.Application.DTOs;
using FocusFlow.Application.Features.Projects.Commands;
using FocusFlow.Application.Features.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.API.Controllers
{
    [Authorize]
    public class ProjectsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
        {
            var query = new GetAllProjectsQuery(GetUserId());
            var projects = await _mediator.Send(query);
            return Ok(projects);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDto>> GetById(Guid id)
        {
            var query = new GetProjectByIdQuery(id, GetUserId());
            var project = await _mediator.Send(query);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] CreateProjectDto request)
        {
            var command = new CreateProjectCommand(
                request.Name,
                request.Description,
                GetUserId()
            );

            var project = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProjectDto>> Update(Guid id, [FromBody] UpdateProjectDto request)
        {
            var command = new UpdateProjectCommand(
                id,
                request.Name,
                request.Description,
                GetUserId()
            );

            var project = await _mediator.Send(command);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProjectCommand(id, GetUserId());
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
