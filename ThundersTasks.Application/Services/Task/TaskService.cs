using ThundersTasks.Application.DTOs;
using ThundersTasks.Application.Mapping;
using ThundersTasks.Core.Models;
using ThundersTasks.Infrastructure.Repository;

namespace ThundersTasks.Application.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskModel> _TaskRepository;

        public TaskService(IRepository<TaskModel> TaskRepository)
        {
            _TaskRepository = TaskRepository;
        }

        public async Task<List<TaskDTO>> GetAllTasksAsync()
        {
            var Tasks = await _TaskRepository.GetAllAsync();
            return TaskMapper.MapTasksToDTOs(Tasks);
        }

        public async Task<TaskDTO> GetTaskByIdAsync(long id)
        {
            var Task = await _TaskRepository.GetByIdAsync(id);
            return TaskMapper.MapTaskToDTO(Task);
        }

        public async Task<TaskDTO> AddTaskAsync(TaskDTO taskDTO)
        {
            var Task = TaskMapper.MapDTOToTask(taskDTO);
            await _TaskRepository.AddAsync(Task);
            return taskDTO;
        }

        public async Task<TaskDTO> UpdateTaskAsync(long id, TaskDTO taskDTO)
        {
            var TaskModel = await _TaskRepository.GetByIdAsync(id);

            if (TaskModel == null)
            {
                throw new KeyNotFoundException($"Tarefa com ID {id} não encontrada.");
            }

            TaskModel.Title = taskDTO.Title;
            TaskModel.Description = taskDTO.Description;
            TaskModel.Status = taskDTO.Status;
            TaskModel.StartDate = taskDTO.StartDate;
            TaskModel.EndDate = taskDTO.EndDate;

            await _TaskRepository.UpdateAsync(TaskModel);
            return taskDTO;
        }

        public async Task DeleteTaskAsync(long id)
        {
            await _TaskRepository.DeleteAsync(id);
        }
    }
}
