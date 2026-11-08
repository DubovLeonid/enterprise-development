using Microsoft.AspNetCore.Mvc;
using Salon.Contracts.Dtos;
using Salon.Contracts.Interfaces;

namespace Salon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(
    IAppointmentService appointmentService,
    ILogger<AppointmentsController> logger) : ControllerBase
{
    private readonly IAppointmentService _appointmentService = appointmentService;
    private readonly ILogger<AppointmentsController> _logger = logger;

    /// <summary>
    /// Получить список всех записей.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAll()
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Запрос на получение всех записей");

            return Ok(await _appointmentService.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка записей");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentDto>> GetById(Guid id)
    {
        try
        {
            var result = await _appointmentService.GetByIdAsync(id);

            if (result == null)
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                    _logger.LogWarning("Запись с Id {Id} не найдена", id);

                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении записи с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать новую запись.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentDto>> Create(CreateAppointmentDto dto)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Создание новой записи для клиента {ClientId}", dto.ClientId);

            var result = await _appointmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании записи");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить существующую запись.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, CreateAppointmentDto dto)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Обновление записи с Id {Id}", id);

            var success = await _appointmentService.UpdateAsync(id, dto);

            if (!success)
            {
                _logger.LogWarning("Запись с Id {Id} не найдена для обновления", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении записи с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить запись.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Удаление записи с Id {Id}", id);

            var success = await _appointmentService.DeleteAsync(id);

            if (!success)
            {
                _logger.LogWarning("Запись с Id {Id} не найдена для удаления", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении записи с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}