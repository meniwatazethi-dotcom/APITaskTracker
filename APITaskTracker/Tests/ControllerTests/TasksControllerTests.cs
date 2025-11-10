using APITaskTracker.Controllers;
using APITaskTracker.Data.Model;
using APITaskTracker.Data.Repository.Interface;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace APITaskTracker.Tests.ControllerTests
{
    public class TasksControllerTests
    {
        private readonly ITaskRepository _fakeRepo;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _fakeRepo = A.Fake<ITaskRepository>();
            _controller = new TasksController(_fakeRepo);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithTasks()
        {
            var fakeTasks = new List<TaskItem>
        {
            new() { Id = 1, Description = "Test task 1", DueDate = DateTime.Today, Title = "zonke bonke" },
            new() { Id = 2, Description = "Test task 2", DueDate = DateTime.Today.AddDays(1), Title= "mahlabemzonda" }
        };
            A.CallTo(() => _fakeRepo.SearchTasksAsync(null, null)).Returns(Task.FromResult<IEnumerable<TaskItem>>(fakeTasks));

            var result = await _controller.GetAll(null, null);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTasks = Assert.IsAssignableFrom<IEnumerable<TaskItem>>(okResult.Value);
            Assert.Equal(2, ((List<TaskItem>)returnTasks).Count);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoTasksFound()
        {
          
            A.CallTo(() => _fakeRepo.SearchTasksAsync("Nonexistent", null))
             .Returns(Task.FromResult<IEnumerable<TaskItem>>(new List<TaskItem>()));

            var result = await _controller.GetAll("Nonexistent", null);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTasks = Assert.IsAssignableFrom<IEnumerable<TaskItem>>(okResult.Value);
            Assert.Empty(returnTasks);
        }

        [Fact]
        public async Task GetAll_ThrowsException_WhenRepoFails()
        {
            A.CallTo(() => _fakeRepo.SearchTasksAsync(null, null)).Throws(new Exception("Database error"));

            await Assert.ThrowsAsync<Exception>(() => _controller.GetAll(null, null));
        }
    }
}
