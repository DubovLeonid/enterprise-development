using Salon.Contracts.Dtos;

namespace Salon.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления услугами салона.
/// </summary>
public interface IServiceService
{
    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    /// <returns>Список DTO всех услуг.</returns>
    public Task<IEnumerable<ServiceDto>> GetAllAsync();

    /// <summary>
    /// Получить услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <returns>DTO услуги или null, если услуга не найдена.</returns>
    public Task<ServiceDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать новую услугу.
    /// </summary>
    /// <param name="dto">Данные для создания услуги.</param>
    /// <returns>DTO созданной услуги.</returns>
    public Task<ServiceDto> CreateAsync(CreateServiceDto dto);

    /// <summary>
    /// Обновить данные существующей услуги.
    /// </summary>
    /// <param name="id">Идентификатор обновляемой услуги.</param>
    /// <param name="dto">Новые данные услуги.</param>
    public Task UpdateAsync(Guid id, CreateServiceDto dto);

    /// <summary>
    /// Удалить услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой услуги.</param>
    public Task DeleteAsync(Guid id);
}