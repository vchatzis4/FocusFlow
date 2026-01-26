using FocusFlow.Application.DTOs;
using FocusFlow.Application.Features.Tasks.Commands;
using FocusFlow.Application.Features.Tasks.Queries;
using FocusFlow.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.API.Controllers
{
    [Authorize]
    public class TasksController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetFiltered(
            [FromQuery] Guid? projectId = null,
            [FromQuery] TaskItemStatus? status = null,
            [FromQuery] TaskPriority? priority = null)
        {
            var query = new GetFilteredTasksQuery(
                GetUserId(),
                projectId,
                status,
                priority
            );

            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskItemDto>> GetById(Guid id)
        {
            var query = new GetTaskByIdQuery(id, GetUserId());
            var task = await _mediator.Send(query);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Create([FromBody] CreateTaskItemDto request)
        {
            var command = new CreateTaskCommand(
                request.Title,
                request.Description,
                request.DueDate,
                request.Priority,
                request.ProjectId,
                request.AssignedUserId,
                GetUserId()
            );

            var task = await _mediator.Send(command);

            if (task == null)
            {
                return BadRequest(new { message = "Failed to create task. Project not found or access denied." });
            }

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TaskItemDto>> Update(Guid id, [FromBody] UpdateTaskItemDto request)
        {
            var command = new UpdateTaskCommand(
                id,
                request.Title,
                request.Description,
                request.DueDate,
                request.Status,
                request.Priority,
                request.AssignedUserId,
                GetUserId()
            );

            var task = await _mediator.Send(command);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteTaskCommand(id, GetUserId());
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
