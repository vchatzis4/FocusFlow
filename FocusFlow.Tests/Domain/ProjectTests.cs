using FluentAssertions;
using FocusFlow.Domain.Entities;

namespace FocusFlow.Tests.Domain;

public class ProjectTests
{
    [Fact]
    public void NewProject_HasEmptyTaskCollection()
    {
        var project = new Project();
        project.Tasks.Should().BeEmpty();
    }

    [Fact]
    public void CanAddTasksToProject()
    {
        var project = new Project { Id = Guid.NewGuid(), Name = "My Project" };
        var task = new TaskItem { Id = Guid.NewGuid(), Title = "Task 1", ProjectId = project.Id };

        project.Tasks.Add(task);

        project.Tasks.Should().HaveCount(1);
        project.Tasks.First().Title.Should().Be("Task 1");
    }
}
