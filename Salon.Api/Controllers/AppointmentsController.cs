using Microsoft.AspNetCore.Mvc;
using Salon.Domain.Models;
using Salon.Api.Dtos;
using Salon.Api.Repositories;

namespace Salon.Api.Controllers;

/// <summary>
/// Контроллер для управления записями клиентов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    /// <summary>
    /// Получить список всех записей.
    /// </summary>
    [HttpGet]
    public IEnumerable<AppointmentDto> GetAll() =>
        InMemoryStorage.Appointments.Select(ToDto);

    /// <summary>
    /// Получить запись по её уникальному идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    public ActionResult<AppointmentDto> GetById(Guid id)
    {
        var a = InMemoryStorage.Appointments.FirstOrDefault(x => x.Id == id);
        if (a is null) return NotFound();
        return ToDto(a);
    }

    /// <summary>
    /// Создать новую запись клиента на услугу.
    /// </summary>
    
    [HttpPost]
    public ActionResult<AppointmentDto> Create([FromBody] CreateAppointmentDto dto)
    {
        var client = InMemoryStorage.Clients.FirstOrDefault(c => c.Id == dto.ClientId);
        if (client is null) return BadRequest($"Клиент с Id = {dto.ClientId} не найден");

        var master = InMemoryStorage.Masters.FirstOrDefault(m => m.Id == dto.MasterId);
        if (master is null) return BadRequest($"Мастер с Id = {dto.MasterId} не найден");

        var service = InMemoryStorage.Services.FirstOrDefault(s => s.Id == dto.ServiceId);
        if (service is null) return BadRequest($"Услуга с Id = {dto.ServiceId} не найдена");

        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            StartTime = dto.StartTime,
            ClientId = client.Id,
            MasterId = master.Id,
            ServiceId = service.Id,
            Client = client,
            Master = master,
            Service = service,
            IsRegular = dto.IsRegular
        };
        InMemoryStorage.Appointments.Add(appointment);
        return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, ToDto(appointment));
    }

    /// <summary>
    /// Удалить запись по её уникальному идентификатору.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var a = InMemoryStorage.Appointments.FirstOrDefault(x => x.Id == id);
        if (a is null) return NotFound();
        InMemoryStorage.Appointments.Remove(a);
        return NoContent();
    }

    /// <summary>
    /// Вспомогательный метод для преобразования в DTO.
    /// </summary>
    private static AppointmentDto ToDto(Appointment a) => new()
    {
        Id = a.Id,
        StartTime = a.StartTime,
        ClientId = a.ClientId,
        ClientName = a.Client.FullName,
        MasterId = a.MasterId,
        MasterName = a.Master.FullName,
        ServiceId = a.ServiceId,
        ServiceName = a.Service.Name,
        IsRegular = a.IsRegular
    };
}