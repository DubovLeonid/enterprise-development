using Salon.Contracts.Dtos;

namespace Salon.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления мастерами салона.
/// </summary>
public interface IMasterService
{
    /// <summary>
    /// Получить список всех мастеров.
    /// </summary>
    /// <returns>Список DTO всех мастеров.</returns>
    public Task<IEnumerable<MasterDto>> GetAllAsync();

    /// <summary>
    /// Получить мастера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор мастера.</param>
    /// <returns>DTO мастера или null, если мастер не найден.</returns>
    public Task<MasterDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать нового мастера.
    /// </summary>
    /// <param name="dto">Данные для создания мастера.</param>
    /// <returns>DTO созданного мастера.</returns>
    public Task<MasterDto> CreateAsync(CreateMasterDto dto);

    /// <summary>
    /// Обновить данные существующего мастера.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого мастера.</param>
    /// <param name="dto">Новые данные мастера.</param>
    public Task<bool> UpdateAsync(Guid id, CreateMasterDto dto);

    /// <summary>
    /// Удалить мастера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого мастера.</param>
    public Task<bool> DeleteAsync(Guid id);
}