using Microsoft.AspNetCore.Mvc;
using Salon.Domain.Models;
using Salon.Api.Dtos;
using Salon.Api.Repositories;

namespace Salon.Api.Controllers;

/// <summary>
/// Контроллер для управления услугами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    
    [HttpGet]
    public IEnumerable<ServiceDto> GetAll() =>
        InMemoryStorage.Services.Select(ToDto);

    /// <summary>
    /// Получить услугу по её уникальному идентификатору.
    /// </summary>
   
    [HttpGet("{id:guid}")]
    public ActionResult<ServiceDto> GetById(Guid id)
    {
        var s = InMemoryStorage.Services.FirstOrDefault(x => x.Id == id);
        if (s is null) return NotFound();
        return ToDto(s);
    }

    /// <summary>
    /// Создать новую услугу.
    /// </summary>
    
    [HttpPost]
    public ActionResult<ServiceDto> Create([FromBody] CreateServiceDto dto)
    {
        var s = new Service
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Category = dto.Category,
            Price = dto.Price,
            Duration = TimeSpan.FromMinutes(dto.DurationMinutes)
        };
        InMemoryStorage.Services.Add(s);
        return CreatedAtAction(nameof(GetById), new { id = s.Id }, ToDto(s));
    }

    /// <summary>
    /// Обновить данные существующей услуги.
    /// </summary>
   
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] CreateServiceDto dto)
    {
        var s = InMemoryStorage.Services.FirstOrDefault(x => x.Id == id);
        if (s is null) return NotFound();

        var index = InMemoryStorage.Services.IndexOf(s);
        InMemoryStorage.Services[index] = new Service
        {
            Id = s.Id,
            Name = dto.Name,
            Category = dto.Category,
            Price = dto.Price,
            Duration = TimeSpan.FromMinutes(dto.DurationMinutes)
        };
        return NoContent();
    }

    /// <summary>
    /// Удалить услугу по её уникальному идентификатору.
    /// </summary>
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var s = InMemoryStorage.Services.FirstOrDefault(x => x.Id == id);
        if (s is null) return NotFound();
        InMemoryStorage.Services.Remove(s);
        return NoContent();
    }

    /// <summary>
    /// Вспомогательный метод для преобразования в DTO.
    /// </summary>
    
    private static ServiceDto ToDto(Service s) => new()
    {
        Id = s.Id,
        Name = s.Name,
        Category = s.Category,
        Price = s.Price,
        DurationMinutes = (int)s.Duration.TotalMinutes
    };
}