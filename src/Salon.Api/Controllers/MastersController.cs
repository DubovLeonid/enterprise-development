using Microsoft.AspNetCore.Mvc;
using Salon.Contracts.Dtos;
using Salon.Contracts.Interfaces;

namespace Salon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MastersController(
    IMasterService masterService,
    ILogger<MastersController> logger) : ControllerBase
{
    private readonly IMasterService _masterService = masterService;
    private readonly ILogger<MastersController> _logger = logger;

    /// <summary>
    /// Получить список всех мастеров.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MasterDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MasterDto>>> GetAll()
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Запрос на получение всех мастеров");

            return Ok(await _masterService.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка мастеров");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить мастера по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MasterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MasterDto>> GetById(Guid id)
    {
        try
        {
            var result = await _masterService.GetByIdAsync(id);

            if (result == null)
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                    _logger.LogWarning("Мастер с Id {Id} не найден", id);

                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении мастера с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать нового мастера.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MasterDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MasterDto>> Create(CreateMasterDto dto)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Создание нового мастера: {FullName}", dto.FullName);

            var result = await _masterService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании мастера");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные мастера.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, CreateMasterDto dto)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Обновление мастера с Id {Id}", id);

            await _masterService.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении мастера {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить мастера.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Удаление мастера с Id {Id}", id);

            await _masterService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении мастера {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}