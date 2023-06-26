using System.ComponentModel.DataAnnotations;

public class Sistemas{
    [Key]
    public int SistemaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
}