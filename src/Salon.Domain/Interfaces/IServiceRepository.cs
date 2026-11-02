using Salon.Domain.Models;

namespace Salon.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с услугами салона.
/// </summary>
public interface IServiceRepository
{
    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    /// <returns>Список всех услуг.</returns>
    public Task<IReadOnlyList<Service>> GetAllAsync();

    /// <summary>
    /// Получить услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>Услуга или null, если услуга не найдена.</returns>
    public Task<Service?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новую услугу.
    /// </summary>
    /// <param name="service">Объект услуги для добавления.</param>
    public Task AddAsync(Service service);

    /// <summary>
    /// Обновить данные существующей услуги.
    /// </summary>
    /// <param name="service">Объект услуги с обновленными данными.</param>
    public Task UpdateAsync(Service service);

    /// <summary>
    /// Удалить услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой услуги.</param>
    public Task DeleteAsync(Guid id);
}