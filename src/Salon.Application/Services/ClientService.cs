using Salon.Contracts.Dtos;
using Salon.Contracts.Interfaces;
using Salon.Domain.Interfaces;
using Salon.Domain.Models;

namespace Salon.Application.Services;

/// <summary>
/// Сервис для управления клиентами салона.
/// </summary>
public class ClientService(IClientRepository clientRepo) : IClientService
{
    private readonly IClientRepository _clientRepo = clientRepo;

    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    /// <returns>Список DTO всех клиентов.</returns>
    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await _clientRepo.GetAllAsync();
        return clients.Select(c => new ClientDto
        {
            Id = c.Id,
            FullName = c.FullName,
            Phone = c.Phone,
            Gender = c.Gender,
            BirthDate = c.BirthDate
        });
    }

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>DTO клиента или null, если клиент не найден.</returns>
    public async Task<ClientDto?> GetByIdAsync(Guid id)
    {
        var c = await _clientRepo.GetByIdAsync(id);
        if (c == null) return null;
        return new ClientDto
        {
            Id = c.Id,
            FullName = c.FullName,
            Phone = c.Phone,
            Gender = c.Gender,
            BirthDate = c.BirthDate
        };
    }

    /// <summary>
    /// Создать нового клиента.
    /// </summary>
    /// <param name="dto">Данные для создания клиента.</param>
    /// <returns>DTO созданного клиента.</returns>
    public async Task<ClientDto> CreateAsync(CreateClientDto dto)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Phone = dto.Phone,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate
        };
        await _clientRepo.AddAsync(client);

        return new ClientDto
        {
            Id = client.Id,
            FullName = client.FullName,
            Phone = client.Phone,
            Gender = client.Gender,
            BirthDate = client.BirthDate
        };
    }

    /// <summary>
    /// Обновить данные существующего клиента.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    public async Task UpdateAsync(Guid id, CreateClientDto dto)
    {
        var client = await _clientRepo.GetByIdAsync(id);
        if (client == null) return;

        var updated = new Client
        {
            Id = id,
            FullName = dto.FullName,
            Phone = dto.Phone,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate
        };
        await _clientRepo.UpdateAsync(updated);
    }

    /// <summary>
    /// Удалить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого клиента.</param>
    public async Task DeleteAsync(Guid id) => await _clientRepo.DeleteAsync(id);
}