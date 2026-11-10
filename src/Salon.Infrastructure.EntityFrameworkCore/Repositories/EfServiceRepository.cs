using Microsoft.EntityFrameworkCore;
using Salon.Domain.Interfaces;
using Salon.Domain.Models;
using Salon.Infrastructure.EntityFrameworkCore.Context;

namespace Salon.Infrastructure.EntityFrameworkCore.Repositories;

/// <summary>
/// Репозиторий услуг, использующий Entity Framework Core.
/// </summary>
public class EfServiceRepository(SalonDbContext context) : IServiceRepository
{
    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    public async Task<IReadOnlyList<Service>> GetAllAsync()
        => await context.Services.AsNoTracking().ToListAsync();

    /// <summary>
    /// Получить услугу по идентификатору.
    /// </summary>
    public async Task<Service?> GetByIdAsync(Guid id)
    => await context.Services
        .AsNoTracking()
        .FirstOrDefaultAsync(s => s.Id == id);

    /// <summary>
    /// Добавить новую услугу.
    /// </summary>
    public async Task AddAsync(Service service)
    {
        await context.Services.AddAsync(service);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить данные существующей услуги.
    /// </summary>
    public async Task UpdateAsync(Service service)
    {
        context.Services.Update(service);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить услугу по идентификатору.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var service = await context.Services.FindAsync(id);
        if (service != null)
        {
            context.Services.Remove(service);
            await context.SaveChangesAsync();
        }
    }
}