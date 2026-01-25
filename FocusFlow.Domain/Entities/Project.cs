namespace FocusFlow.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string OwnerId { get; set; } = string.Empty;
        public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
