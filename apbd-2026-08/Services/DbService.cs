using apbd_2026_08.Data;
using apbd_2026_08.DTOs;
using apbd_2026_08.Entities;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_08.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _context;
    
    public DbService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetPcDto>> GetAllAsync()
    {
        return await _context.PCs
            .Select(pc => new GetPcDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<GetPcComponentDto>?> GetComponentsAsync(int id)
    {
        var exists = await _context.PCs
            .AnyAsync(pc => pc.Id == id);

        if (!exists)
            return null;
        
        return await _context.PcComponents
            .Where(pc => pc.PcId == id)
            .Select(pc => new GetPcComponentDto
            {
                Code = pc.Component.Code,
                Name = pc.Component.Name,
                Description = pc.Component.Description,
                Manufacturer = pc.Component.ComponentManufacturer.FullName,
                Type = pc.Component.ComponentType.Name,
                Amount = pc.Amount
            })
            .ToListAsync();
    }

    public async Task<GetPcDto> CreateAsync(CreatePcDto dto)
    {
        var pc = new Pc
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };
        
        _context.PCs.Add(pc);
        
        await _context.SaveChangesAsync();

        return new GetPcDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> UpdateAsync(int id, CreatePcDto dto)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc == null)
            return false;
        
        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;
        
        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc == null)
            return false;

        _context.PCs.Remove(pc);
        
        await _context.SaveChangesAsync();

        return true;
    }
}