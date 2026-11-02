using Salon.Contracts.Dtos;

namespace Salon.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для выполнения аналитических запросов.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить список мастеров, чей стаж работы составляет не менее 5 лет.
    /// </summary>
    /// <returns>Список DTO опытных мастеров, отсортированный по убыванию стажа.</returns>
    public Task<IEnumerable<ExperiencedMasterDto>> GetMastersWithExperienceAtLeast5YearsAsync();

    /// <summary>
    /// Найти свободные окна (промежутки времени) в расписании выбранного мастера.
    /// </summary>
    /// <param name="masterId">Идентификатор мастера.</param>
    /// <returns>Объект с информацией о мастере и списком свободных окон.</returns>
    public Task<FreeSlotsResponseDto> GetFreeSlotsForMasterAsync(Guid masterId);

    /// <summary>
    /// Получить топ-5 наиболее популярных услуг по количеству записей.
    /// </summary>
    /// <returns>Объект с общим количеством услуг в топе и списком популярных услуг.</returns>
    public Task<TopServicesResponseDto> GetTop5PopularServicesAsync();

    /// <summary>
    /// Подсчитать количество повторных записей клиентов за последний месяц.
    /// </summary>
    /// <returns>Объект со статистикой повторных записей по каждому клиенту и общим итогом.</returns>
    public Task<RepeatAppointmentsResponseDto> GetRepeatAppointmentsLastMonthAsync();

    /// <summary>
    /// Получить список клиентов, которые были записаны более чем к одному мастеру.
    /// </summary>
    /// <returns>Объект с количеством таких клиентов и их списком, отсортированным по дате рождения.</returns>
    public Task<LoyalClientsResponseDto> GetClientsWithSeveralMastersAsync();
}