using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_08.Entities;

[Table("PCComponents")]
[PrimaryKey(nameof(PcId), nameof(ComponentCode))]
public class PcComponent
{
    public int Amount { get; set; }
    
    public int PcId { get; set; }

    [ForeignKey(nameof(PcId))]
    public Pc Pc { get; set; } = null!;
    
    [Column(TypeName = "char(10)")]
    public string ComponentCode { get; set; } = string.Empty;

    [ForeignKey(nameof(ComponentCode))]
    public Component Component { get; set; } = null!;
}