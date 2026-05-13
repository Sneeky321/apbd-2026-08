using Microsoft.EntityFrameworkCore;
using apbd_2026_08.Entities;

namespace apbd_2026_08.Data;

public class AppDbContext : DbContext
{
    public DbSet<Pc> PCs { get; set; }
    
    public DbSet<Component> Components { get; set; }
    
    public DbSet<ComponentType> ComponentTypes { get; set; }
    
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    
    public DbSet<PcComponent> PcComponents { get; set; }

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");
            
            entity.Property(e => e.Weight)
                .HasColumnType("float");

            entity.Property(e => e.Warranty)
                .IsRequired();
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasColumnType("char(10)");
            
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnType("nvarchar(max)")
                .IsRequired();
            
            entity.HasOne(e => e.ComponentManufacturer)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentManufacturersId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.ComponentType)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();
            
            entity.Property(e => e.FullName)
                .HasMaxLength(300)
                .IsRequired();
            
            entity.Property(e => e.FoundationDate)
                .HasColumnType("date");
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();
        });
        
        modelBuilder.Entity<PcComponent>(entity =>
        {
            entity.HasKey(e => new
            {
                e.PcId,
                e.ComponentCode
            });

            entity.Property(e => e.ComponentCode)
                .HasColumnType("char(10)");

            entity.Property(e => e.Amount)
                .IsRequired();

            entity.HasOne(e => e.Pc)
                .WithMany(e => e.PcComponents)
                .HasForeignKey(e => e.PcId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Component)
                .WithMany(e => e.PcComponents)
                .HasForeignKey(e => e.ComponentCode)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1,
                Abbreviation = "AMD",
                FullName = "Advanced Micro Devices",
                FoundationDate = new DateTime(1969, 5, 1)
            },
            new ComponentManufacturer
            {
                Id = 2,
                Abbreviation = "INTEL",
                FullName = "Intel Corporation",
                FoundationDate = new DateTime(1968, 7, 18)
            },
            new ComponentManufacturer
            {
                Id = 3,
                Abbreviation = "NVIDIA",
                FullName = "NVIDIA Corporation",
                FoundationDate = new DateTime(1993, 4, 5)
            }
        );
        
        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType
            {
                Id = 1,
                Abbreviation = "CPU",
                Name = "Processor"
            },
            new ComponentType
            {
                Id = 2,
                Abbreviation = "GPU",
                Name = "Graphics Card"
            },
            new ComponentType
            {
                Id = 3,
                Abbreviation = "RAM",
                Name = "Memory"
            }
        );
        
        modelBuilder.Entity<Component>().HasData(
            new
            {
                Code = "CPU0000001",
                Name = "Ryzen 9800X3D",
                Description = "Processor",
                ComponentManufacturersId = 1,
                ComponentTypesId = 1
            },
            new
            {
                Code = "GPU0000001",
                Name = "RTX 5090",
                Description = "High-end graphics card",
                ComponentManufacturersId = 3,
                ComponentTypesId = 2
            },
            new
            {
                Code = "RAM0000001",
                Name = "32GB DDR5",
                Description = "DDR5 RAM Kit",
                ComponentManufacturersId = 2,
                ComponentTypesId = 3
            }
        );
        
        modelBuilder.Entity<Pc>().HasData(
            new Pc
            {
                Id = 1,
                Name = "Gaming X",
                Weight = 12.5,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8),
                Stock = 5
            },
            new Pc
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15),
                Stock = 12
            },
            new Pc
            {
                Id = 3,
                Name = "UltraBook Z",
                Weight = 2.1,
                Warranty = 12,
                CreatedAt = new DateTime(2026, 3, 1),
                Stock = 8
            }
        );
        
        modelBuilder.Entity<PcComponent>().HasData(
            new
            {
                PcId = 1,
                ComponentCode = "CPU0000001",
                Amount = 1
            },
            new
            {
                PcId = 1,
                ComponentCode = "GPU0000001",
                Amount = 1
            },
            new
            {
                PcId = 1,
                ComponentCode = "RAM0000001",
                Amount = 2
            }
        );
    }
}