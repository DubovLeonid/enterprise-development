using Microsoft.AspNetCore.Mvc;
using Salon.Domain.Models;
using Salon.Api.Dtos;
using Salon.Api.Repositories;

namespace Salon.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    [HttpGet]
    public IEnumerable<ClientDto> GetAll() =>
        InMemoryStorage.Clients.Select(ToDto);

    /// <summary>
    /// Получить клиента по его уникальному идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    public ActionResult<ClientDto> GetById(Guid id)
    {
        var client = InMemoryStorage.Clients.FirstOrDefault(c => c.Id == id);
        if (client is null) return NotFound($"Клиент с Id = {id} не найден");
        return ToDto(client);
    }

    /// <summary>
    /// Создать нового клиента.
    /// </summary>
    [HttpPost]
    public ActionResult<ClientDto> Create([FromBody] CreateClientDto dto)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            Phone = dto.Phone
        };
        InMemoryStorage.Clients.Add(client);
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, ToDto(client));
    }

    /// <summary>
    /// Обновить данные существующего клиента.
    /// </summary>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] CreateClientDto dto)
    {
        var client = InMemoryStorage.Clients.FirstOrDefault(c => c.Id == id);
        if (client is null) return NotFound();

        var index = InMemoryStorage.Clients.IndexOf(client);
        InMemoryStorage.Clients[index] = new Client
        {
            Id = client.Id,
            FullName = dto.FullName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            Phone = dto.Phone
        };
        return NoContent();
    }

    /// <summary>
    /// Удалить клиента по его уникальному идентификатору.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var client = InMemoryStorage.Clients.FirstOrDefault(c => c.Id == id);
        if (client is null) return NotFound();
        InMemoryStorage.Clients.Remove(client);
        return NoContent();
    }

    /// <summary>
    /// Вспомогательный метод для преобразования  в DTO.
    /// </summary>
    private static ClientDto ToDto(Client c) => new()
    {
        Id = c.Id,
        FullName = c.FullName,
        Gender = c.Gender,
        BirthDate = c.BirthDate,
        Phone = c.Phone
    };
}