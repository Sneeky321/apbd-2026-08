using System.ComponentModel.DataAnnotations;

namespace apbd_2026_08.DTOs;

public class CreatePcDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}