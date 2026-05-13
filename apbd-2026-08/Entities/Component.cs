using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace apbd_2026_08.Entities;

[Table("Components")]
[PrimaryKey(nameof(Code))]
public class Component
{
    [Key]
    [StringLength(10)]
    [Column(TypeName = "char(10)")]
    public string Code { get; set; } = string.Empty;
    [MaxLength(300)]
    [Required]
    public string Name { get; set; } = string.Empty;
    [MaxLength]
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public int ComponentManufacturersId { get; set; }

    [ForeignKey(nameof(ComponentManufacturersId))]
    public ComponentManufacturer ComponentManufacturer { get; set; } = null!;
    
    public int ComponentTypesId { get; set; }

    [ForeignKey(nameof(ComponentTypesId))]
    public ComponentType ComponentType { get; set; } = null!;
    
    public ICollection<PcComponent> PcComponents { get; set; } = [];
}