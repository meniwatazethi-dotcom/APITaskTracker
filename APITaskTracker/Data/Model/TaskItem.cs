using System.ComponentModel.DataAnnotations;
using TaskTracker.Data.Enums;

namespace APITaskTracker.Data.Model
{
    public class TaskItem
    {
        public int Id { get; set; }

        [MinLength(3)]
        public required string Title { get; set; }

        public string? Description { get; set; }

        public TaskItemStatus Status { get; set; } = TaskItemStatus.New;

        public TaskPriority Priority { get; set; } = TaskPriority.Low;

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
