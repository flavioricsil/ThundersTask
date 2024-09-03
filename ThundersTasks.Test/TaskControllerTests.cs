using Microsoft.AspNetCore.Mvc;
using Moq;
using ThundersTasks.Api.Controllers;
using ThundersTasks.Application.DTOs;
using ThundersTasks.Application.Services.Tasks;

namespace ThundersTasks.Test
{
    [TestClass]
    public class TaskControllerTests
    {
        private Mock<ITaskService> _mockTaskService;
        private TaskController _taskController;

        [TestInitialize]
        public void Setup()
        {
            _mockTaskService = new Mock<ITaskService>();
            _taskController = new TaskController(_mockTaskService.Object);
        }

        [TestMethod]
        public async Task GetAllTasks_ReturnsOkResult_WithListOfTasks()
        {
            var taskList = new List<TaskDTO>
            {
                new TaskDTO { Id = 1, Title = "Task 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = Core.Enums.EnumTaskStatus.Executando },
                new TaskDTO { Id = 2, Title = "Task 2", Description = "Description 2", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = Core.Enums.EnumTaskStatus.Executando }
            };
            _mockTaskService.Setup(service => service.GetAllTasksAsync()).ReturnsAsync(taskList);

            var result = await _taskController.GetAllTasks();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnTasks = okResult.Value as List<TaskDTO>;
            Assert.AreEqual(2, returnTasks.Count);
        }

        [TestMethod]
        public async Task GetAllTasks_ReturnsInternalServerError_OnException()
        {
            _mockTaskService.Setup(service => service.GetAllTasksAsync()).ThrowsAsync(new System.Exception("Test exception"));

            var result = await _taskController.GetAllTasks();

            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error: Test exception", statusCodeResult.Value);
        }

        [TestMethod]
        public async Task GetTaskById_ReturnsOkResult_WithTask()
        {
           
            var taskId = 1L;
            var task = new TaskDTO { Id = 1, Title = "Task 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = Core.Enums.EnumTaskStatus.Executando };
            _mockTaskService.Setup(service => service.GetTaskByIdAsync(taskId)).ReturnsAsync(task);

            var result = await _taskController.GetTaskById(taskId);
            
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnTask = okResult.Value as TaskDTO;
            Assert.IsNotNull(returnTask);
            Assert.AreEqual(taskId, returnTask.Id);
        }

        [TestMethod]
        public async Task GetTaskById_ReturnsNotFound_WhenTaskNotFound()
        {
           
            var taskId = 1L;
            _mockTaskService.Setup(service => service.GetTaskByIdAsync(taskId)).ReturnsAsync((TaskDTO)null);

            var result = await _taskController.GetTaskById(taskId);

            var notFoundResult = result.Result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
        }

        [TestMethod]
        public async Task GetTaskById_ReturnsInternalServerError_OnException()
        {
           
            var taskId = 1L;
            _mockTaskService.Setup(service => service.GetTaskByIdAsync(taskId)).ThrowsAsync(new System.Exception("Test exception"));

            var result = await _taskController.GetTaskById(taskId);
            
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error: Test exception", statusCodeResult.Value);
        }

        [TestMethod]
        public async Task AddTask_ReturnsCreatedAtActionResult_WithNewTask()
        {
           
            var newTask = new TaskDTO { Id = 1, Title = "Task 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = Core.Enums.EnumTaskStatus.Executando };
            _mockTaskService.Setup(service => service.AddTaskAsync(It.IsAny<TaskDTO>())).ReturnsAsync(newTask);
            
            var result = await _taskController.AddTask(new TaskDTO { Id = 1, Title = "Task 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = Core.Enums.EnumTaskStatus.Executando });
            
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(nameof(TaskController.GetTaskById), createdAtActionResult.ActionName);
            var returnTask = createdAtActionResult.Value as TaskDTO;
            Assert.IsNotNull(returnTask);
            Assert.AreEqual(newTask.Id, returnTask.Id);
        }

        [TestMethod]
        public async Task AddTask_ReturnsInternalServerError_OnException()
        {
           
            _mockTaskService.Setup(service => service.AddTaskAsync(It.IsAny<TaskDTO>())).ThrowsAsync(new System.Exception("Test exception"));
 
            var result = await _taskController.AddTask(new TaskDTO { Id = 1, Title = "Task 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = Core.Enums.EnumTaskStatus.Aberta });

            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error: Test exception", statusCodeResult.Value);
        }

        [TestMethod]
        public async Task UpdateTask_ReturnsOkResult_WithUpdatedTask()
        {
           
            var taskId = 1L;
            var updatedTask = new TaskDTO { Id = 1, Title = "Task 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = Core.Enums.EnumTaskStatus.Aberta };
            _mockTaskService.Setup(service => service.UpdateTaskAsync(taskId, It.IsAny<TaskDTO>())).ReturnsAsync(updatedTask);
            
            var result = await _taskController.UpdateTask(taskId, new TaskDTO { Id = 1, Title = "Task 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = Core.Enums.EnumTaskStatus.Aberta });
            
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnTask = okResult.Value as TaskDTO;
            Assert.IsNotNull(returnTask);
            Assert.AreEqual(updatedTask.Id, returnTask.Id);
        }

        [TestMethod]
        public async Task UpdateTask_ReturnsInternalServerError_OnException()
        {
           
            var taskId = 1L;
            _mockTaskService.Setup(service => service.UpdateTaskAsync(taskId, It.IsAny<TaskDTO>())).ThrowsAsync(new System.Exception("Test exception"));

            var result = await _taskController.UpdateTask(taskId, new TaskDTO { Id = 1, Title = "Task 1", Description = "Description 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = Core.Enums.EnumTaskStatus.Aberta });

            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error: Test exception", statusCodeResult.Value);
        }

        [TestMethod]
        public async Task DeleteTask_ReturnsNoContentResult()
        {
           
            var taskId = 1L;
            _mockTaskService.Setup(service => service.DeleteTaskAsync(taskId)).Returns(Task.CompletedTask);
            
            var result = await _taskController.DeleteTask(taskId);
            
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
        }

        [TestMethod]
        public async Task DeleteTask_ReturnsInternalServerError_OnException()
        {
           
            var taskId = 1L;
            _mockTaskService.Setup(service => service.DeleteTaskAsync(taskId)).ThrowsAsync(new System.Exception("Test exception"));

            var result = await _taskController.DeleteTask(taskId); 

            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error: Test exception", statusCodeResult.Value);
        }
    }
}
