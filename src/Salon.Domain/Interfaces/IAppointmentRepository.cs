using System.Linq.Expressions;
using Salon.Domain.Models;

namespace Salon.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с записями клиентов на услуги.
/// </summary>
public interface IAppointmentRepository
{
    /// <summary>
    /// Получить список всех записей.
    /// </summary>
    /// <returns>Список всех записей.</returns>
    public Task<IReadOnlyList<Appointment>> GetAllAsync();

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <returns>Запись или null, если запись не найдена.</returns>
    public Task<Appointment?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новую запись.
    /// </summary>
    /// <param name="appointment">Объект записи для добавления.</param>
    public Task AddAsync(Appointment appointment);

    /// <summary>
    /// Обновить существующую запись.
    /// </summary>
    /// <param name="appointment">Объект записи с обновленными данными.</param>
    public Task UpdateAsync(Appointment appointment);

    /// <summary>
    /// Удалить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой записи.</param>
    public Task DeleteAsync(Guid id);

    /// <summary>
    /// Найти записи, соответствующие заданному условию.
    /// </summary>
    /// <param name="predicate">Условие фильтрации записей.</param>
    /// <returns>Список записей, удовлетворяющих условию.</returns>
    public Task<IReadOnlyList<Appointment>> FindAsync(Expression<Func<Appointment, bool>> predicate);
}