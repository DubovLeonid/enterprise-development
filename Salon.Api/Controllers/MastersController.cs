using Microsoft.AspNetCore.Mvc;
using Salon.Domain.Models;
using Salon.Api.Dtos;
using Salon.Api.Repositories;

namespace Salon.Api.Controllers;

/// <summary>
/// Контроллер для управления мастерами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MastersController : ControllerBase
{
    /// <summary>
    /// Получить список всех мастеров.
    /// </summary>
    
    [HttpGet]
    public IEnumerable<MasterDto> GetAll() =>
        InMemoryStorage.Masters.Select(ToDto);

    /// <summary>
    /// Получить мастера по его уникальному идентификатору.
    /// </summary>
  
    [HttpGet("{id:guid}")]
    public ActionResult<MasterDto> GetById(Guid id)
    {
        var m = InMemoryStorage.Masters.FirstOrDefault(x => x.Id == id);
        if (m is null) return NotFound($"Мастер с Id = {id} не найден");
        return ToDto(m);
    }

    /// <summary>
    /// Создать нового мастера.
    /// </summary>
    
    [HttpPost]
    public ActionResult<MasterDto> Create([FromBody] CreateMasterDto dto)
    {
        var m = new Master
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            PassportNumber = dto.PassportNumber,
            Specialization = dto.Specialization,
            ExperienceYears = dto.ExperienceYears
        };
        InMemoryStorage.Masters.Add(m);
        return CreatedAtAction(nameof(GetById), new { id = m.Id }, ToDto(m));
    }

    /// <summary>
    /// Обновить данные существующего мастера.
    /// </summary>
    
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] CreateMasterDto dto)
    {
        var m = InMemoryStorage.Masters.FirstOrDefault(x => x.Id == id);
        if (m is null) return NotFound();

        var index = InMemoryStorage.Masters.IndexOf(m);
        InMemoryStorage.Masters[index] = new Master
        {
            Id = m.Id,
            FullName = dto.FullName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            PassportNumber = dto.PassportNumber,
            Specialization = dto.Specialization,
            ExperienceYears = dto.ExperienceYears
        };
        return NoContent();
    }

    /// <summary>
    /// Удалить мастера по его уникальному идентификатору.
    /// </summary>
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var m = InMemoryStorage.Masters.FirstOrDefault(x => x.Id == id);
        if (m is null) return NotFound();
        InMemoryStorage.Masters.Remove(m);
        return NoContent();
    }

    /// <summary>
    /// Вспомогательный метод для преобразования в DTO.
    /// </summary>
   
    private static MasterDto ToDto(Master m) => new()
    {
        Id = m.Id,
        FullName = m.FullName,
        Gender = m.Gender,
        BirthDate = m.BirthDate,
        PassportNumber = m.PassportNumber,
        Specialization = m.Specialization,
        ExperienceYears = m.ExperienceYears
    };
}