using FocusFlow.Domain.Enums;

namespace FocusFlow.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; } = null!;
        public string? AssignedUserId { get; set; }
    }
}
