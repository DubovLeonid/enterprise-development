using Microsoft.EntityFrameworkCore;
using Salon.Domain.Interfaces;
using Salon.Domain.Models;
using Salon.Infrastructure.EntityFrameworkCore.Context;
using System.Linq.Expressions;

namespace Salon.Infrastructure.EntityFrameworkCore.Repositories;

/// <summary>
/// Репозиторий записей на услуги, использующий Entity Framework Core.
/// </summary>
public class EfAppointmentRepository(SalonDbContext context) : IAppointmentRepository
{
    /// <summary>
    /// Получить список всех записей.
    /// </summary>
    public async Task<IReadOnlyList<Appointment>> GetAllAsync()
    => await context.Appointments
        .AsNoTracking()
        .Include(a => a.Client)   
        .Include(a => a.Master)   
        .Include(a => a.Service)  
        .ToListAsync();

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    public async Task<Appointment?> GetByIdAsync(Guid id)
    => await context.Appointments
        .AsNoTracking()
        .Include(a => a.Client)   
        .Include(a => a.Master)   
        .Include(a => a.Service) 
        .FirstOrDefaultAsync(a => a.Id == id);

    /// <summary>
    /// Добавить новую запись.
    /// </summary>
    public async Task AddAsync(Appointment appointment)
    {
        await context.Appointments.AddAsync(appointment);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить существующую запись.
    /// </summary>
    public async Task UpdateAsync(Appointment appointment)
    {
        context.Appointments.Update(appointment);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить запись по идентификатору.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var appointment = await context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            context.Appointments.Remove(appointment);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Найти записи, соответствующие заданному условию.
    /// </summary>
    public async Task<IReadOnlyList<Appointment>> FindAsync(Expression<Func<Appointment, bool>> predicate)
    => await context.Appointments
        .AsNoTracking()
        .Include(a => a.Client)
        .Include(a => a.Service) 
        .Include(a => a.Master)  
        .Where(predicate)
        .ToListAsync();
}