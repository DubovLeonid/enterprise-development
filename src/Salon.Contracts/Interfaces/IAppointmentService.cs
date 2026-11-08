using Salon.Contracts.Dtos;

namespace Salon.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления записями клиентов на услуги.
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Получить список всех записей.
    /// </summary>
    /// <returns>Список DTO всех записей.</returns>
    public Task<IEnumerable<AppointmentDto>> GetAllAsync();

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <returns>DTO записи или null, если запись не найдена.</returns>
    public Task<AppointmentDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать новую запись.
    /// </summary>
    /// <param name="dto">Данные для создания записи.</param>
    /// <returns>DTO созданной записи.</returns>
    public Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);

    /// <summary>
    /// Обновить существующую запись.
    /// </summary>
    /// <param name="id">Идентификатор обновляемой записи.</param>
    /// <param name="dto">Новые данные записи.</param>
    public Task<bool> UpdateAsync(Guid id, CreateAppointmentDto dto);

    /// <summary>
    /// Удалить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой записи.</param>
    public Task<bool> DeleteAsync(Guid id);
}