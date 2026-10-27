using Microsoft.AspNetCore.Mvc;
using Salon.Api.Dtos;
using Salon.Api.Repositories;

namespace Salon.Api.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AnalyticsController : ControllerBase
{
    /// <summary>
    ///Получить список мастеров со стажем работы не менее заданного.
    /// </summary>
    [HttpGet("experienced-masters")]
    [ProducesResponseType(typeof(ExperiencedMastersResponseDto), 200)]
    public IActionResult ExperiencedMasters([FromQuery] int minYears = 5)
    {
        var result = InMemoryStorage.Masters
            .Where(m => m.ExperienceYears >= minYears)
            .OrderByDescending(m => m.ExperienceYears)
            .ThenBy(m => m.FullName)
            .Select(m => new ExperiencedMasterDto
            {
                Id = m.Id,
                FullName = m.FullName,
                Specialization = m.Specialization,
                ExperienceYears = m.ExperienceYears
            })
            .ToList();

        return Ok(new ExperiencedMastersResponseDto
        {
            MinYears = minYears,
            Count = result.Count,
            Masters = result
        });
    }

    /// <summary>
    ///Найти свободные окошки.
    /// </summary>
    [HttpGet("free-slots/{masterId:guid}")]
    [ProducesResponseType(typeof(FreeSlotsResponseDto), 200)]
    [ProducesResponseType(404)]
    public IActionResult FreeSlots(Guid masterId)
    {
        var master = InMemoryStorage.Masters.FirstOrDefault(m => m.Id == masterId);
        if (master is null) return NotFound($"Мастер с Id = {masterId} не найден");

        var appointments = InMemoryStorage.Appointments
            .Where(a => a.MasterId == masterId)
            .OrderBy(a => a.StartTime)
            .ToList();

        var windows = new List<FreeSlotDto>();
        for (var i = 0; i < appointments.Count - 1; i++)
        {
            var current = appointments[i];
            var next = appointments[i + 1];

            
            var endCurrent = current.StartTime + current.Service.Duration;
            var free = next.StartTime - endCurrent;

            if (free > TimeSpan.Zero)
            {
                windows.Add(new FreeSlotDto
                {
                    Start = endCurrent,
                    End = next.StartTime,
                    DurationMinutes = (int)free.TotalMinutes
                });
            }
        }

        return Ok(new FreeSlotsResponseDto
        {
            Master = master.FullName,
            Count = windows.Count,
            Windows = windows
        });
    }

    /// <summary>
    ///Получить топ-N самых популярных услуг.
    /// </summary>
    [HttpGet("top-services")]
    [ProducesResponseType(typeof(TopServicesResponseDto), 200)]
    public IActionResult TopServices([FromQuery] int count = 5)
    {
        var result = InMemoryStorage.Appointments
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
            .Take(count)
            .ToList();

        return Ok(new TopServicesResponseDto
        {
            Total = result.Count,
            Top = result
        });
    }

    /// <summary>
    ///Количество повторных записей клиентов за последний месяц.
    /// </summary>
    [HttpGet("repeat-appointments")]
    [ProducesResponseType(typeof(RepeatAppointmentsResponseDto), 200)]
    public IActionResult RepeatAppointments([FromQuery] DateTime? endDate = null)
    {
        var end = endDate ?? DateTime.UtcNow;
        var start = end.AddMonths(-1);

        var perClient = InMemoryStorage.Appointments
            .Where(a => a.StartTime >= start && a.StartTime < end && a.IsRegular)
            .GroupBy(a => a.Client)
            .Select(g => new RepeatClientDto
            {
                ClientId = g.Key.Id,
                ClientName = g.Key.FullName,
                Total = g.Count(),
                Repeat = g.Count() - 1 
            })
            .Where(x => x.Repeat > 0) 
            .OrderBy(x => x.ClientName)
            .ToList();

        return Ok(new RepeatAppointmentsResponseDto
        {
            PeriodStart = start,
            PeriodEnd = end,
            TotalRepeats = perClient.Sum(x => x.Repeat),
            Clients = perClient
        });
    }

    /// <summary>
    ///Клиенты, записанные к нескольким мастерам.
    /// </summary>
    [HttpGet("loyal-clients")]
    [ProducesResponseType(typeof(LoyalClientsResponseDto), 200)]
    public IActionResult LoyalClients()
    {
        var result = InMemoryStorage.Appointments
            .GroupBy(a => a.Client)
            .Where(g => g.Select(a => a.MasterId).Distinct().Count() > 1)
            .Select(g => new LoyalClientDto
            {
                ClientId = g.Key.Id,
                ClientName = g.Key.FullName,
                BirthDate = g.Key.BirthDate,
                MastersCount = g.Select(a => a.MasterId).Distinct().Count()
            })
            .OrderBy(x => x.BirthDate) 
            .ToList();

        return Ok(new LoyalClientsResponseDto
        {
            Count = result.Count,
            Clients = result
        });
    }
}