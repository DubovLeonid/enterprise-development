using Salon.Domain.Interfaces;
using Salon.Domain.Models;
using System.Linq.Expressions;

namespace Salon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий записей на услуги, использующий хранение в памяти.
/// </summary>
public class InMemoryAppointmentRepository : IAppointmentRepository
{
    private readonly List<Appointment> _appointments = [];

    /// <summary>
    /// Получить список всех записей.
    /// </summary>
    /// <returns>Список всех записей.</returns>
    public Task<IReadOnlyList<Appointment>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Appointment>>([.. _appointments]);
    }

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <returns>Запись или null, если запись не найдена.</returns>
    public Task<Appointment?> GetByIdAsync(Guid id)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(appointment);
    }

    /// <summary>
    /// Добавить новую запись.
    /// </summary>
    /// <param name="appointment">Объект записи для добавления.</param>
    public Task AddAsync(Appointment appointment)
    {
        ArgumentNullException.ThrowIfNull(appointment);
        _appointments.Add(appointment);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить существующую запись.
    /// </summary>
    /// <param name="appointment">Объект записи с обновленными данными.</param>
    public Task UpdateAsync(Appointment appointment)
    {
        ArgumentNullException.ThrowIfNull(appointment);
        var index = _appointments.FindIndex(a => a.Id == appointment.Id);
        if (index >= 0)
        {
            _appointments[index] = appointment;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удалить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой записи.</param>
    public Task DeleteAsync(Guid id)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == id);
        if (appointment is not null)
        {
            _appointments.Remove(appointment);
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Найти записи, соответствующие заданному условию.
    /// </summary>
    /// <param name="predicate">Условие фильтрации записей.</param>
    /// <returns>Список записей, удовлетворяющих условию.</returns>
    public Task<IReadOnlyList<Appointment>> FindAsync(Expression<Func<Appointment, bool>> predicate)
    {
        var result = _appointments.AsQueryable().Where(predicate).ToList();
        return Task.FromResult<IReadOnlyList<Appointment>>(result);
    }
}