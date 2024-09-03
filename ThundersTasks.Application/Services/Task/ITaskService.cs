using ThundersTasks.Application.DTOs;
using ThundersTasks.Core.Models;

namespace ThundersTasks.Application.Services.Tasks
{
    public interface ITaskService
    {
        Task<List<TaskDTO>> GetAllTasksAsync();
        Task<TaskDTO> GetTaskByIdAsync(long id);
        Task<TaskDTO> AddTaskAsync(TaskDTO taskDTO);
        Task<TaskDTO> UpdateTaskAsync(long id, TaskDTO taskDTO);
        Task DeleteTaskAsync(long id);
    }
}
