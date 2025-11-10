namespace TaskTracker.Data.Repository
{
    using APITaskTracker.Data;
    using APITaskTracker.Data.Model;
    using APITaskTracker.Data.Repository.Interface;
    using Microsoft.EntityFrameworkCore;

    public class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> SearchTasksAsync(string? q, string? sort)
        {
            IQueryable<TaskItem> taskItems = _context.Tasks;

            if (!string.IsNullOrWhiteSpace(q))
            {
                var lower = q.ToLower();
                taskItems = taskItems.Where(t =>
                    t.Title.ToLower().Contains(lower) ||
                    (t.Description != null && t.Description.ToLower().Contains(lower)));
            }

            taskItems = sort?.ToLower() switch
            {
                "duedate:desc" => taskItems.OrderByDescending(t => t.DueDate),
                _ => taskItems.OrderBy(t => t.DueDate)
            };

            return await taskItems.ToListAsync();
        }
    }


}
