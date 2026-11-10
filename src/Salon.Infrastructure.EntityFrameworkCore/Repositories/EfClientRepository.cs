using Microsoft.EntityFrameworkCore;
using Salon.Domain.Interfaces;
using Salon.Domain.Models;
using Salon.Infrastructure.EntityFrameworkCore.Context;

namespace Salon.Infrastructure.EntityFrameworkCore.Repositories;

/// <summary>
/// Репозиторий клиентов, использующий Entity Framework Core.
/// </summary>
public class EfClientRepository(SalonDbContext context) : IClientRepository
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    public async Task<IReadOnlyList<Client>> GetAllAsync()
        => await context.Clients.AsNoTracking().ToListAsync();

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    public async Task<Client?> GetByIdAsync(Guid id)
    => await context.Clients
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == id);

    /// <summary>
    /// Добавить нового клиента.
    /// </summary>
    public async Task AddAsync(Client client)
    {
        await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить данные существующего клиента.
    /// </summary>
    public async Task UpdateAsync(Client client)
    {
        context.Clients.Update(client);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить клиента по идентификатору.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var client = await context.Clients.FindAsync(id);
        if (client != null)
        {
            context.Clients.Remove(client);
            await context.SaveChangesAsync();
        }
    }
}