using Salon.Domain.Models;

namespace Salon.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с клиентами салона.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    /// <returns>Список всех клиентов.</returns>
    public Task<IReadOnlyList<Client>> GetAllAsync();

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Клиент или null, если клиент не найден.</returns>
    public Task<Client?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить нового клиента.
    /// </summary>
    /// <param name="client">Объект клиента для добавления.</param>
    public Task AddAsync(Client client);

    /// <summary>
    /// Обновить данные существующего клиента.
    /// </summary>
    /// <param name="client">Объект клиента с обновленными данными.</param>
    public Task UpdateAsync(Client client);

    /// <summary>
    /// Удалить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого клиента.</param>
    public Task DeleteAsync(Guid id);
}