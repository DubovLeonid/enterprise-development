using Salon.Domain.Interfaces;
using Salon.Domain.Models;

namespace Salon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий мастеров, использующий хранение в памяти.
/// </summary>
public class InMemoryMasterRepository : IMasterRepository
{
    private readonly List<Master> _masters = [];

    /// <summary>
    /// Получить список всех мастеров.
    /// </summary>
    /// <returns>Список всех мастеров.</returns>
    public Task<IReadOnlyList<Master>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Master>>([.. _masters]);
    }

    /// <summary>
    /// Получить мастера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор мастера.</param>
    /// <returns>Мастер или null, если мастер не найден.</returns>
    public Task<Master?> GetByIdAsync(Guid id)
    {
        var master = _masters.FirstOrDefault(m => m.Id == id);
        return Task.FromResult(master);
    }

    /// <summary>
    /// Добавить нового мастера.
    /// </summary>
    /// <param name="master">Объект мастера для добавления.</param>
    public Task AddAsync(Master master)
    {
        ArgumentNullException.ThrowIfNull(master);
        _masters.Add(master);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить данные существующего мастера.
    /// </summary>
    /// <param name="master">Объект мастера с обновленными данными.</param>
    public Task UpdateAsync(Master master)
    {
        ArgumentNullException.ThrowIfNull(master);
        var index = _masters.FindIndex(m => m.Id == master.Id);
        if (index >= 0)
        {
            _masters[index] = master;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удалить мастера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого мастера.</param>
    public Task DeleteAsync(Guid id)
    {
        var master = _masters.FirstOrDefault(m => m.Id == id);
        if (master is not null)
        {
            _masters.Remove(master);
        }
        return Task.CompletedTask;
    }
}