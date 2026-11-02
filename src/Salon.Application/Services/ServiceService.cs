using Salon.Contracts.Dtos;
using Salon.Contracts.Interfaces;
using Salon.Domain.Interfaces;
using Salon.Domain.Models;

namespace Salon.Application.Services;

/// <summary>
/// Сервис для управления услугами салона.
/// </summary>
public class ServiceService(IServiceRepository serviceRepo) : IServiceService
{
    private readonly IServiceRepository _serviceRepo = serviceRepo;

    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    /// <returns>Список DTO всех услуг.</returns>
    public async Task<IEnumerable<ServiceDto>> GetAllAsync()
    {
        var services = await _serviceRepo.GetAllAsync();
        return services.Select(s => new ServiceDto
        {
            Id = s.Id,
            Name = s.Name,
            Category = s.Category,
            Price = s.Price,
            DurationMinutes = (int)s.Duration.TotalMinutes
        });
    }

    /// <summary>
    /// Получить услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>DTO услуги или null, если услуга не найдена.</returns>
    public async Task<ServiceDto?> GetByIdAsync(Guid id)
    {
        var s = await _serviceRepo.GetByIdAsync(id);
        if (s == null) return null;
        return new ServiceDto
        {
            Id = s.Id,
            Name = s.Name,
            Category = s.Category,
            Price = s.Price,
            DurationMinutes = (int)s.Duration.TotalMinutes
        };
    }

    /// <summary>
    /// Создать новую услугу.
    /// </summary>
    /// <param name="dto">Данные для создания услуги.</param>
    /// <returns>DTO созданной услуги.</returns>
    public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
    {
        var service = new Service
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Category = dto.Category,
            Price = dto.Price,
            Duration = TimeSpan.FromMinutes(dto.DurationMinutes)
        };
        await _serviceRepo.AddAsync(service);

        
        return new ServiceDto
        {
            Id = service.Id,
            Name = service.Name,
            Category = service.Category,
            Price = service.Price,
            DurationMinutes = (int)service.Duration.TotalMinutes
        };
    }

    /// <summary>
    /// Обновить данные существующей услуги.
    /// </summary>
    /// <param name="id">Идентификатор обновляемой услуги.</param>
    /// <param name="dto">Новые данные услуги.</param>
    public async Task UpdateAsync(Guid id, CreateServiceDto dto)
    {
        var existing = await _serviceRepo.GetByIdAsync(id);
        if (existing == null) return;

        var updated = new Service
        {
            Id = id,
            Name = dto.Name,
            Category = dto.Category,
            Price = dto.Price,
            Duration = TimeSpan.FromMinutes(dto.DurationMinutes)
        };
        await _serviceRepo.UpdateAsync(updated);
    }

    /// <summary>
    /// Удалить услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой услуги.</param>
    public async Task DeleteAsync(Guid id) => await _serviceRepo.DeleteAsync(id);
}