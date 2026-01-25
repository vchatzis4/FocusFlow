using FocusFlow.Application.DTOs;
using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Entities;
using MediatR;

namespace FocusFlow.Application.Features.Projects.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                OwnerId = request.OwnerId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Projects.CreateAsync(project, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ProjectDto(
                project.Id,
                project.Name,
                project.Description,
                project.OwnerId,
                project.CreatedAt,
                project.UpdatedAt,
                TotalTasks: 0,
                CompletedTasks: 0,
                OverdueTasks: 0
            );
        }
    }
}
