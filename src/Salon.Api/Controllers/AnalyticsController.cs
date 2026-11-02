using Microsoft.AspNetCore.Mvc;
using Salon.Contracts.Interfaces;

namespace Salon.Api.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    private readonly IAnalyticsService _analyticsService = analyticsService;
    private readonly ILogger<AnalyticsController> _logger = logger;

    /// <summary>
    /// ТЕСТ 1: Получить список мастеров со стажем работы не менее 5 лет.
    /// </summary>
    [HttpGet("masters-experience")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMasters()
    {
        try
        {
            _logger.LogInformation("Запрос аналитики: мастера со стажем >= 5 лет");
            var result = await _analyticsService.GetMastersWithExperienceAtLeast5YearsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении мастеров со стажем >= 5 лет");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// ТЕСТ 2: Найти свободные окошки выбранного мастера.
    /// </summary>
    [HttpGet("free-slots/{masterId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFreeSlots(Guid masterId)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Запрос аналитики: свободные окошки мастера {MasterId}", masterId);
            }
            var result = await _analyticsService.GetFreeSlotsForMasterAsync(masterId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении свободных окошек мастера {MasterId}", masterId);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// ТЕСТ 3: Получить топ-5 самых популярных услуг.
    /// </summary>
    [HttpGet("top-services")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTopServices()
    {
        try
        {
            _logger.LogInformation("Запрос аналитики: топ-5 популярных услуг");
            var result = await _analyticsService.GetTop5PopularServicesAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении топ-5 популярных услуг");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// ТЕСТ 4: Количество повторных записей клиентов за последний месяц.
    /// </summary>
    [HttpGet("repeat-appointments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRepeatAppointments()
    {
        try
        {
            _logger.LogInformation("Запрос аналитики: повторные записи за последний месяц");
            var result = await _analyticsService.GetRepeatAppointmentsLastMonthAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении повторных записей за последний месяц");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// ТЕСТ 5: Клиенты, записанные к нескольким мастерам.
    /// </summary>
    [HttpGet("loyal-clients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLoyalClients()
    {
        try
        {
            _logger.LogInformation("Запрос аналитики: клиенты, записанные к нескольким мастерам");
            var result = await _analyticsService.GetClientsWithSeveralMastersAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении клиентов, записанных к нескольким мастерам");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}