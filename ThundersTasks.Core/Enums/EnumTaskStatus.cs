using System.ComponentModel.DataAnnotations;

namespace ThundersTasks.Core.Enums
{
    public enum EnumTaskStatus
    {
        [Display(Name = "Aberta")]
        Aberta = 1,
        [Display(Name = "Executando")]
        Executando = 2,
        [Display(Name = "Cancelada")]
        Cancelada = 3,
        [Display(Name = "Finalizada")]
        Finalizada = 4
    }
}
