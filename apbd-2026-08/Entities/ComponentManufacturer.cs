using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace apbd_2026_08.Entities;

[Table("ComponentManufacturers")]
public class ComponentManufacturer
{
    [Key]
    public int Id { get; set; }
    [MaxLength(30)]
    [Required]
    public string Abbreviation { get; set; } = string.Empty;
    [MaxLength(300)]
    [Required]
    public string FullName { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; }
    
    public ICollection<Component> Components { get; set; } = [];
}