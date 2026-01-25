using FocusFlow.Application.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken);

            if (task == null)
                return false;

            var project = await _unitOfWork.Projects.GetByIdAsync(task.ProjectId, cancellationToken);

            if (project == null || project.OwnerId != request.OwnerId)
                return false;

            await _unitOfWork.Tasks.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
