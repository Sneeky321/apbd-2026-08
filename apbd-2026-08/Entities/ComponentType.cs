using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apbd_2026_08.Entities;

[Table("ComponentTypes")]
public class ComponentType
{
    [Key]
    public int Id { get; set; }
    [MaxLength(30)]
    [Required]
    public string Abbreviation { get; set; } = string.Empty;
    [MaxLength(150)]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Component> Components { get; set; } = [];
}