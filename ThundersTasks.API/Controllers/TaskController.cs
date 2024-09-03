using Microsoft.AspNetCore.Mvc;
using ThundersTasks.Application.DTOs;
using ThundersTasks.Application.Services.Tasks;

namespace ThundersTasks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _TaskService;

        public TaskController(ITaskService TaskService)
        {
            _TaskService = TaskService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDTO>>> GetAllTasks()
        {
            try
            {
                var Tasks = await _TaskService.GetAllTasksAsync();
                return Ok(Tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTaskById(long id)
        {
            try
            {
                var Task = await _TaskService.GetTaskByIdAsync(id);
                if (Task == null)
                {
                    return NotFound();
                }
                return Ok(Task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> AddTask(TaskDTO TaskDTO)
        {
            try
            {
                var novaTask = await _TaskService.AddTaskAsync(TaskDTO);
                return CreatedAtAction(nameof(GetTaskById), new { id = novaTask.Id }, novaTask);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDTO>> UpdateTask(long id, TaskDTO TaskDTO)
        {
            try
            {
                var TaskAtualizada = await _TaskService.UpdateTaskAsync(id, TaskDTO);
                return Ok(TaskAtualizada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(long id)
        {
            try
            {
                await _TaskService.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
