using Salon.Domain.Interfaces;
using Salon.Domain.Models;

namespace Salon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий клиентов, использующий хранение в памяти.
/// </summary>
public class InMemoryClientRepository : IClientRepository
{
    private readonly List<Client> _clients = [];

    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    /// <returns>Список всех клиентов.</returns>
    public Task<IReadOnlyList<Client>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Client>>([.. _clients]);
    }

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Клиент или null, если клиент не найден.</returns>
    public Task<Client?> GetByIdAsync(Guid id)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(client);
    }

    /// <summary>
    /// Добавить нового клиента.
    /// </summary>
    /// <param name="client">Объект клиента для добавления.</param>
    public Task AddAsync(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        _clients.Add(client);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить данные существующего клиента.
    /// </summary>
    /// <param name="client">Объект клиента с обновленными данными.</param>
    public Task UpdateAsync(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        var index = _clients.FindIndex(c => c.Id == client.Id);
        if (index >= 0)
        {
            _clients[index] = client;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удалить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого клиента.</param>
    public Task DeleteAsync(Guid id)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        if (client is not null)
        {
            _clients.Remove(client);
        }
        return Task.CompletedTask;
    }
}