using FluentAssertions;
using FocusFlow.Application.Features.Projects.Commands;
using FocusFlow.Application.Interfaces;
using FocusFlow.Domain.Entities;
using Moq;

namespace FocusFlow.Tests.Handlers;

public class CreateProjectCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IProjectRepository> _projectRepoMock;
    private readonly CreateProjectCommandHandler _handler;

    public CreateProjectCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _projectRepoMock = new Mock<IProjectRepository>();
        _unitOfWorkMock.Setup(u => u.Projects).Returns(_projectRepoMock.Object);
        _handler = new CreateProjectCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_CreatesProjectWithCorrectName()
    {
        var command = new CreateProjectCommand("New Project", "Description", "user-1");

        _projectRepoMock
            .Setup(r => r.CreateAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project p, CancellationToken _) => p);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Name.Should().Be("New Project");
        result.Description.Should().Be("Description");
        result.OwnerId.Should().Be("user-1");
    }

    [Fact]
    public async Task Handle_SavesChanges()
    {
        var command = new CreateProjectCommand("Test", null, "user-1");

        _projectRepoMock
            .Setup(r => r.CreateAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project p, CancellationToken _) => p);

        await _handler.Handle(command, CancellationToken.None);

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsZeroTaskCounts()
    {
        var command = new CreateProjectCommand("Test", null, "user-1");

        _projectRepoMock
            .Setup(r => r.CreateAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project p, CancellationToken _) => p);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.TotalTasks.Should().Be(0);
        result.CompletedTasks.Should().Be(0);
        result.OverdueTasks.Should().Be(0);
    }
}
