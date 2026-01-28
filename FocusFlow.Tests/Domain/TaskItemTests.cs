using FluentAssertions;
using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Enums;

namespace FocusFlow.Tests.Domain;

public class TaskItemTests
{
    [Fact]
    public void NewTask_DefaultsToTodoStatus()
    {
        var task = new TaskItem();
        task.Status.Should().Be(TaskItemStatus.Todo);
    }

    [Fact]
    public void NewTask_DefaultsToMediumPriority()
    {
        var task = new TaskItem();
        task.Priority.Should().Be(TaskPriority.Medium);
    }

    [Fact]
    public void Task_CanChangeStatus()
    {
        var task = new TaskItem { Title = "Test Task" };

        task.Status = TaskItemStatus.InProgress;
        task.Status.Should().Be(TaskItemStatus.InProgress);

        task.Status = TaskItemStatus.Done;
        task.Status.Should().Be(TaskItemStatus.Done);
    }
}
