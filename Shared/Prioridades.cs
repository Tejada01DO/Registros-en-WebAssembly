using System.ComponentModel.DataAnnotations;

public class Prioridades
{
    [Key]
    public int PrioridadId { get; set; }

    [Required(ErrorMessage = "La descripcion no puede ser nula")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Es necesario agregar los dias de compromiso")]
    [Range(0, 31)]
    public int DiasCompromiso { get; set; }
}