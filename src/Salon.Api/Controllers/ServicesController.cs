using Microsoft.AspNetCore.Mvc;
using Salon.Contracts.Dtos;
using Salon.Contracts.Interfaces;

namespace Salon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController(
    IServiceService serviceService,
    ILogger<ServicesController> logger) : ControllerBase
{
    private readonly IServiceService _serviceService = serviceService;
    private readonly ILogger<ServicesController> _logger = logger;

    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ServiceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAll()
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Запрос на получение всех услуг");

            return Ok(await _serviceService.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка услуг");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить услугу по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceDto>> GetById(Guid id)
    {
        try
        {
            var result = await _serviceService.GetByIdAsync(id);

            if (result == null)
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                    _logger.LogWarning("Услуга с Id {Id} не найдена", id);

                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении услуги с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать новую услугу.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ServiceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceDto>> Create(CreateServiceDto dto)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Создание новой услуги: {Name}", dto.Name);

            var result = await _serviceService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании услуги");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные услуги.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, CreateServiceDto dto)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Обновление услуги с Id {Id}", id);

            var success = await _serviceService.UpdateAsync(id, dto);

            if (!success)
            {
                _logger.LogWarning("Услуга с Id {Id} не найдена для обновления", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении услуги {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить услугу.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Удаление услуги с Id {Id}", id);

            var success = await _serviceService.DeleteAsync(id);

            if (!success)
            {
                _logger.LogWarning("Услуга с Id {Id} не найдена для удаления", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex) when (ex.InnerException?.Message.Contains("foreign key") == true)
        {
            _logger.LogWarning(ex, "Попытка удалить услугу, на которую ссылаются записи");
            return Conflict("Невозможно удалить услугу, так как на неё ссылаются записи.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении услуги {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}