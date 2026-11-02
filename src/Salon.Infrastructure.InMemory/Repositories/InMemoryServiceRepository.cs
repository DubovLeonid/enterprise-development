using Salon.Domain.Interfaces;
using Salon.Domain.Models;

namespace Salon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий услуг, использующий хранение в памяти.
/// </summary>
public class InMemoryServiceRepository : IServiceRepository
{
    private readonly List<Service> _services = [];

    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    /// <returns>Список всех услуг.</returns>
    public Task<IReadOnlyList<Service>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Service>>([.. _services]);
    }

    /// <summary>
    /// Получить услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>Услуга или null, если услуга не найдена.</returns>
    public Task<Service?> GetByIdAsync(Guid id)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(service);
    }

    /// <summary>
    /// Добавить новую услугу.
    /// </summary>
    /// <param name="service">Объект услуги для добавления.</param>
    public Task AddAsync(Service service)
    {
        ArgumentNullException.ThrowIfNull(service);
        _services.Add(service);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить данные существующей услуги.
    /// </summary>
    /// <param name="service">Объект услуги с обновленными данными.</param>
    public Task UpdateAsync(Service service)
    {
        ArgumentNullException.ThrowIfNull(service);
        var index = _services.FindIndex(s => s.Id == service.Id);
        if (index >= 0)
        {
            _services[index] = service;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удалить услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой услуги.</param>
    public Task DeleteAsync(Guid id)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);
        if (service is not null)
        {
            _services.Remove(service);
        }
        return Task.CompletedTask;
    }
}