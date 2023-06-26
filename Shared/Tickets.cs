using System.ComponentModel.DataAnnotations;
public class Tickets
{
    [Key]

    public int TicketId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Today; 

    [Required(ErrorMessage = "Es necesario especificar por quien fue solicitado")]
    public string? SolicitadoPor { get; set; }

    [Required(ErrorMessage = "Es necesario especificar el asunto")]
    public string? Asunto { get; set; }

    [Required(ErrorMessage = "Es necesario dar una descripcion")]
    public string? Descripcion { get; set; }

}