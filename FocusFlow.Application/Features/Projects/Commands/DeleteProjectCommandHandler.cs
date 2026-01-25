using FocusFlow.Application.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Projects.Commands
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(request.Id, cancellationToken);

            if (project == null || project.OwnerId != request.OwnerId)
                return false;

            await _unitOfWork.Projects.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
