using Salon.Contracts.Dtos;
using Salon.Contracts.Interfaces;
using Salon.Domain.Interfaces;
using Salon.Domain.Models;

namespace Salon.Application.Services;

/// <summary>
/// Сервис для управления мастерами салона.
/// </summary>
public class MasterService(IMasterRepository masterRepo) : IMasterService
{
    private readonly IMasterRepository _masterRepo = masterRepo;

    /// <summary>
    /// Получить список всех мастеров.
    /// </summary>
    /// <returns>Список DTO всех мастеров.</returns>
    public async Task<IEnumerable<MasterDto>> GetAllAsync()
    {
        var masters = await _masterRepo.GetAllAsync();
        return masters.Select(m => new MasterDto
        {
            Id = m.Id,
            FullName = m.FullName,
            Gender = m.Gender,
            BirthDate = m.BirthDate,
            PassportNumber = m.PassportNumber,
            Specialization = m.Specialization,
            ExperienceYears = m.ExperienceYears
        });
    }

    /// <summary>
    /// Получить мастера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор мастера.</param>
    /// <returns>DTO мастера или null, если мастер не найден.</returns>
    public async Task<MasterDto?> GetByIdAsync(Guid id)
    {
        var m = await _masterRepo.GetByIdAsync(id);
        if (m == null) return null;
        return new MasterDto
        {
            Id = m.Id,
            FullName = m.FullName,
            Gender = m.Gender,
            BirthDate = m.BirthDate,
            PassportNumber = m.PassportNumber,
            Specialization = m.Specialization,
            ExperienceYears = m.ExperienceYears
        };
    }

    /// <summary>
    /// Создать нового мастера.
    /// </summary>
    /// <param name="dto">Данные для создания мастера.</param>
    /// <returns>DTO созданного мастера.</returns>
    public async Task<MasterDto> CreateAsync(CreateMasterDto dto)
    {
        var master = new Master
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            PassportNumber = dto.PassportNumber,
            Specialization = dto.Specialization,
            ExperienceYears = dto.ExperienceYears
        };
        await _masterRepo.AddAsync(master);


        return new MasterDto
        {
            Id = master.Id,
            FullName = master.FullName,
            Gender = master.Gender,
            BirthDate = master.BirthDate,
            PassportNumber = master.PassportNumber,
            Specialization = master.Specialization,
            ExperienceYears = master.ExperienceYears
        };
    }

    /// <summary>
    /// Обновить данные существующего мастера.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого мастера.</param>
    /// <param name="dto">Новые данные мастера.</param>
    public async Task<bool> UpdateAsync(Guid id, CreateMasterDto dto)
    {
        var existing = await _masterRepo.GetByIdAsync(id);
        if (existing == null) return false; 

        var updated = new Master
        {
            Id = id,
            FullName = dto.FullName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            PassportNumber = dto.PassportNumber,
            Specialization = dto.Specialization,
            ExperienceYears = dto.ExperienceYears
        };
        await _masterRepo.UpdateAsync(updated);
        return true; 
    }

    /// <summary>
    /// Удалить мастера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого мастера.</param>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _masterRepo.GetByIdAsync(id);
        if (existing == null) return false; 

        await _masterRepo.DeleteAsync(id);
        return true; 
    }
}