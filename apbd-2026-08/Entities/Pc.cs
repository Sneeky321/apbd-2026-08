using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apbd_2026_08.Entities;

[Table("PCs")]
public class Pc
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    [Required]
    public string Name { get; set; } = string.Empty;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }

    public ICollection<PcComponent> PcComponents { get; set; } = [];
}