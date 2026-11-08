using Salon.Contracts.Dtos;
using Salon.Contracts.Interfaces;
using Salon.Domain.Interfaces;

namespace Salon.Application.Services;

/// <summary>
/// Сервис для выполнения аналитических запросов по данным салона.
/// </summary>
public class AnalyticsService(
    IMasterRepository masterRepo,
    IAppointmentRepository appointmentRepo,
    IServiceRepository serviceRepo,
    IClientRepository clientRepo) : IAnalyticsService
{
    private readonly IMasterRepository _masterRepo = masterRepo;
    private readonly IAppointmentRepository _appointmentRepo = appointmentRepo;
    private readonly IServiceRepository _serviceRepo = serviceRepo;
    private readonly IClientRepository _clientRepo = clientRepo;

    /// <summary>
    /// Возвращает список мастеров, чей стаж работы составляет не менее 5 лет.
    /// </summary>
    /// <returns>Список DTO опытных мастеров, отсортированный по убыванию стажа.</returns>
    public async Task<IEnumerable<ExperiencedMasterDto>> GetMastersWithExperienceAtLeast5YearsAsync()
    {
        var masters = await _masterRepo.GetAllAsync();

        return [.. masters
            .Where(m => m.ExperienceYears >= 5)
            .OrderByDescending(m => m.ExperienceYears)
            .ThenBy(m => m.FullName)
            .Select(m => new ExperiencedMasterDto
            {
                Id = m.Id,
                FullName = m.FullName,
                Specialization = m.Specialization,
                ExperienceYears = m.ExperienceYears
            })];
    }

    /// <summary>
    /// Находит свободные окна (промежутки времени) в расписании выбранного мастера.
    /// </summary>
    /// <param name="masterId">Идентификатор мастера.</param>
    /// <returns>Объект с информацией о мастере и списком свободных окон.</returns>
    /// <exception cref="Exception">Выбрасывается, если мастер с указанным ID не найден.</exception>
    public async Task<FreeSlotsResponseDto?> GetFreeSlotsForMasterAsync(Guid masterId)
    {
        var master = await _masterRepo.GetByIdAsync(masterId);
        if (master == null) return null;

        var appointments = await _appointmentRepo.FindAsync(a => a.MasterId == masterId);
        var sorted = appointments.OrderBy(a => a.StartTime).ToList();

        var windows = new List<FreeSlotDto>();

        for (var i = 0; i < sorted.Count - 1; i++)
        {
            var current = sorted[i];
            var next = sorted[i + 1];
            var endCurrent = current.StartTime + current.Service.Duration;
            var freeTime = next.StartTime - endCurrent;

            if (freeTime > TimeSpan.Zero)
            {
                windows.Add(new FreeSlotDto
                {
                    Start = endCurrent,
                    End = next.StartTime,
                    DurationMinutes = (int)freeTime.TotalMinutes
                });
            }
        }

        return new FreeSlotsResponseDto
        {
            Master = master.FullName,
            Count = windows.Count,
            Windows = windows
        };
    }

    /// <summary>
    /// Возвращает топ-5 наиболее популярных услуг по количеству записей.
    /// </summary>
    /// <returns>Объект с общим количеством услуг в топе и списком популярных услуг.</returns>
    public async Task<TopServicesResponseDto> GetTop5PopularServicesAsync()
    {
        var appointments = await _appointmentRepo.GetAllAsync();

        var top = appointments
            .GroupBy(a => a.Service)
            .Select(g => new TopServiceDto
            {
                ServiceId = g.Key.Id,
                ServiceName = g.Key.Name,
                Category = g.Key.Category.ToString(),
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.ServiceName)
            .Take(5)
            .ToList();

        return new TopServicesResponseDto
        {
            Total = top.Count,
            Top = top
        };
    }

    /// <summary>
    /// Подсчитывает количество повторных записей клиентов за последний месяц.
    /// </summary>
    /// <returns>Объект со статистикой повторных записей по каждому клиенту и общим итогом.</returns>
    public async Task<RepeatAppointmentsResponseDto> GetRepeatAppointmentsLastMonthAsync()
    {
        var appointments = await _appointmentRepo.GetAllAsync();

        var endDate = new DateTime(2026, 9, 29);
        var startDate = endDate.AddMonths(-1);

        var clients = appointments
            .Where(a => a.StartTime >= startDate && a.StartTime < endDate && a.IsRegular)
            .GroupBy(a => a.Client)
            .Select(g => new RepeatClientDto
            {
                ClientId = g.Key.Id,
                ClientName = g.Key.FullName,
                Total = g.Count(),
                Repeat = g.Count() - 1
            })
            .Where(x => x.Repeat > 0)
            .OrderByDescending(x => x.Repeat)
            .ToList();

        return new RepeatAppointmentsResponseDto
        {
            PeriodStart = startDate,
            PeriodEnd = endDate,
            TotalRepeats = clients.Sum(x => x.Repeat),
            Clients = clients
        };
    }

    /// <summary>
    /// Возвращает список клиентов, которые были записаны более чем к одному мастеру.
    /// </summary>
    /// <returns>Объект с количеством таких клиентов и их списком, отсортированным по дате рождения.</returns>
    public async Task<LoyalClientsResponseDto> GetClientsWithSeveralMastersAsync()
    {
        var appointments = await _appointmentRepo.GetAllAsync();

        var clients = appointments
            .GroupBy(a => a.Client)
            .Where(g => g.Select(a => a.Master.Id).Distinct().Count() > 1)
            .Select(g => new LoyalClientDto
            {
                ClientId = g.Key.Id,
                ClientName = g.Key.FullName,
                BirthDate = g.Key.BirthDate,
                MastersCount = g.Select(a => a.Master.Id).Distinct().Count()
            })
            .OrderBy(x => x.BirthDate)
            .ToList();

        return new LoyalClientsResponseDto
        {
            Count = clients.Count,
            Clients = clients
        };
    }
}