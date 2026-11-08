using Salon.Contracts.Dtos;

namespace Salon.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления клиентами салона.
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    /// <returns>Список DTO всех клиентов.</returns>
    public Task<IEnumerable<ClientDto>> GetAllAsync();

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>DTO клиента или null, если клиент не найден.</returns>
    public Task<ClientDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Создать нового клиента.
    /// </summary>
    /// <param name="dto">Данные для создания клиента.</param>
    /// <returns>DTO созданного клиента.</returns>
    public Task<ClientDto> CreateAsync(CreateClientDto dto);

    /// <summary>
    /// Обновить данные существующего клиента.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    public Task<bool> UpdateAsync(Guid id, CreateClientDto dto);

    /// <summary>
    /// Удалить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого клиента.</param>
    public Task<bool> DeleteAsync(Guid id);
}