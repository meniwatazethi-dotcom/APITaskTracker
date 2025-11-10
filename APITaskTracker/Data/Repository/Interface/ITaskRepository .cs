using APITaskTracker.Data.Model;

namespace APITaskTracker.Data.Repository.Interface
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> SearchTasksAsync(string? query, string? sort);
    }
}