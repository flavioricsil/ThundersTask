using ThundersTasks.Application.DTOs;
using ThundersTasks.Core.Models;


namespace ThundersTasks.Application.Mapping
{
    public static class TaskMapper
    {
        public static TaskDTO MapTaskToDTO(TaskModel Task)
        {
            return new TaskDTO
            {
                Id          = Task.Id,
                Title       = Task.Title,
                Description = Task.Description,
                StartDate   = DateTime.Now,
                EndDate     = DateTime.Now,
                Status      = Task.Status
            };
        }

        public static List<TaskDTO> MapTasksToDTOs(List<TaskModel> Tasks)
        {
            List<TaskDTO> TasksDTO = new List<TaskDTO>();
            foreach (TaskModel Task in Tasks)
            {
                TasksDTO.Add(MapTaskToDTO(Task));
            }
            return TasksDTO;
        }

        public static TaskModel MapDTOToTask(TaskDTO TaskDTO)
        {
            return new TaskModel
            {
                Id          = TaskDTO.Id,
                Title       = TaskDTO.Title,
                Description = TaskDTO.Description,
                StartDate   = TaskDTO.StartDate,
                EndDate     = TaskDTO.EndDate,
                Status      = TaskDTO.Status
            };
        }
    }
}
