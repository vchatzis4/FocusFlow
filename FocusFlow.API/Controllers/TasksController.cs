using FocusFlow.API.Hubs;
using FocusFlow.Application.DTOs;
using FocusFlow.Application.Features.Tasks.Commands;
using FocusFlow.Application.Features.Tasks.Queries;
using FocusFlow.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FocusFlow.API.Controllers
{
    [Authorize]
    public class TasksController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<TaskHub> _hubContext;

        public TasksController(IMediator mediator, IHubContext<TaskHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
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

            await _hubContext.Clients.Group(task.ProjectId.ToString()).SendAsync("TaskCreated", task);

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

            await _hubContext.Clients.Group(task.ProjectId.ToString()).SendAsync("TaskUpdated", task);

            return Ok(task);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await _mediator.Send(new GetTaskByIdQuery(id, GetUserId()));
            if (task == null)
            {
                return NotFound();
            }

            var command = new DeleteTaskCommand(id, GetUserId());
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            await _hubContext.Clients.Group(task.ProjectId.ToString()).SendAsync("TaskDeleted", id);

            return NoContent();
        }
    }
}
