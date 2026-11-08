using Microsoft.EntityFrameworkCore;
using Salon.Domain.Interfaces;
using Salon.Domain.Models;
using Salon.Infrastructure.EntityFrameworkCore.Context;

namespace Salon.Infrastructure.EntityFrameworkCore.Repositories;

/// <summary>
/// Репозиторий мастеров, использующий Entity Framework Core.
/// </summary>
public class EfMasterRepository(SalonDbContext context) : IMasterRepository
{
    /// <summary>
    /// Получить список всех мастеров.
    /// </summary>
    public async Task<IReadOnlyList<Master>> GetAllAsync()
        => await context.Masters.AsNoTracking().ToListAsync();

    /// <summary>
    /// Получить мастера по идентификатору.
    /// </summary>
    public async Task<Master?> GetByIdAsync(Guid id)
        => await context.Masters.FirstOrDefaultAsync(m => m.Id == id);

    /// <summary>
    /// Добавить нового мастера.
    /// </summary>
    public async Task AddAsync(Master master)
    {
        await context.Masters.AddAsync(master);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить данные существующего мастера.
    /// </summary>
    public async Task UpdateAsync(Master master)
    {
        context.Masters.Update(master);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить мастера по идентификатору.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var master = await context.Masters.FindAsync(id);
        if (master != null)
        {
            context.Masters.Remove(master);
            await context.SaveChangesAsync();
        }
    }
}