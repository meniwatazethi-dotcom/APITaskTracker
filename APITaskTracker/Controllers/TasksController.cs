using APITaskTracker.Data.Model;
using APITaskTracker.Data.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APITaskTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(ITaskRepository repo) : ControllerBase
    {
        private readonly ITaskRepository _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll([FromQuery] string? description, [FromQuery] string? dueDate)
        {
            var tasks = await _repo.SearchTasksAsync(description, dueDate);
            return Ok(tasks);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskItem>> Get(int id)
        {
            var task = await _repo.GetByIdAsync(id);
            if (task == null) return NotFound(new { message = $"Task {id} not found" });

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create(TaskItem task)
        {
            task.CreatedAt = DateTime.UtcNow;
            await _repo.AddAsync(task);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, TaskItem updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.Status = updated.Status;
            existing.Priority = updated.Priority;
            existing.DueDate = updated.DueDate;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _repo.Delete(existing);
            await _repo.SaveChangesAsync();

            return NoContent();
        }
    }
}
