using APITaskTracker.Data.Model;

namespace TaskTracker.Data.Repository.Interface
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> SearchTasksAsync(string? query, string? sort);
    }
}