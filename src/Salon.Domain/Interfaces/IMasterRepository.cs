using Salon.Domain.Models;

namespace Salon.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с мастерами салона.
/// </summary>
public interface IMasterRepository
{
    /// <summary>
    /// Получить список всех мастеров.
    /// </summary>
    /// <returns>Список всех мастеров.</returns>
    public Task<IReadOnlyList<Master>> GetAllAsync();

    /// <summary>
    /// Получить мастера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор мастера.</param>
    /// <returns>Мастер или null, если мастер не найден.</returns>
    public Task<Master?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить нового мастера.
    /// </summary>
    /// <param name="master">Объект мастера для добавления.</param>
    public Task AddAsync(Master master);

    /// <summary>
    /// Обновить данные существующего мастера.
    /// </summary>
    /// <param name="master">Объект мастера с обновленными данными.</param>
    public Task UpdateAsync(Master master);

    /// <summary>
    /// Удалить мастера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого мастера.</param>
    public Task DeleteAsync(Guid id);
}