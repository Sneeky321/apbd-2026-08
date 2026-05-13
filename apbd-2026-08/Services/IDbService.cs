using apbd_2026_08.DTOs;

namespace apbd_2026_08.Services;

public interface IDbService
{
    Task<IEnumerable<GetPcDto>> GetAllAsync();
    
    Task<IEnumerable<GetPcComponentDto>?> GetComponentsAsync(int id);
    
    Task<GetPcDto> CreateAsync(CreatePcDto dto);

    Task<bool> UpdateAsync(int id, CreatePcDto dto);
    
    Task<bool> DeleteAsync(int id);
}