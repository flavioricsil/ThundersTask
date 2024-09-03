using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ThundersTasks.Core.Enums;

namespace ThundersTasks.Application.DTOs
{
    public class TaskDTO
    {
        [Key]
        [DisplayName("Id")]
        public long Id { get; set; }

        [DisplayName("Título")]
        public required string Title { get; set; }

        [DisplayName("Descrição")]
        public string? Description { get; set; }

        [DisplayName("Data/Hora para Início")]
       
        public required DateTime StartDate { get; set; }

        [DisplayName("Data/Hora Limite para Conclusão")]
       
        public required DateTime EndDate { get; set; }

        [DisplayName("Status")]
        public EnumTaskStatus Status { get; set; }
    }
}
