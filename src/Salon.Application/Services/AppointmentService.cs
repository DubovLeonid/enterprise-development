using Salon.Contracts.Dtos;
using Salon.Contracts.Interfaces;
using Salon.Domain.Interfaces;
using Salon.Domain.Models;

namespace Salon.Application.Services;

/// <summary>
/// Сервис для управления записями клиентов на услуги.
/// </summary>
public class AppointmentService(IAppointmentRepository appointmentRepo) : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepo = appointmentRepo;

    /// <summary>
    /// Получить список всех записей.
    /// </summary>
    /// <returns>Список DTO всех записей.</returns>
    public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
    {
        var appointments = await _appointmentRepo.GetAllAsync();
        return appointments.Select(a => new AppointmentDto
        {
            Id = a.Id,
            StartTime = a.StartTime,
            ClientId = a.ClientId,
            MasterId = a.MasterId,
            ServiceId = a.ServiceId,
            IsRegular = a.IsRegular
        });
    }

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <returns>DTO записи или null, если запись не найдена.</returns>
    public async Task<AppointmentDto?> GetByIdAsync(Guid id)
    {
        var a = await _appointmentRepo.GetByIdAsync(id);
        if (a == null) return null;
        return new AppointmentDto
        {
            Id = a.Id,
            StartTime = a.StartTime,
            ClientId = a.ClientId,
            MasterId = a.MasterId,
            ServiceId = a.ServiceId,
            IsRegular = a.IsRegular
        };
    }

    /// <summary>
    /// Создать новую запись.
    /// </summary>
    /// <param name="dto">Данные для создания записи.</param>
    /// <returns>DTO созданной записи.</returns>
    public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto)
    {
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            StartTime = dto.StartTime,
            ClientId = dto.ClientId,
            MasterId = dto.MasterId,
            ServiceId = dto.ServiceId,
            IsRegular = dto.IsRegular,
            Client = null!,
            Master = null!,
            Service = null!
        };
        await _appointmentRepo.AddAsync(appointment);


        return new AppointmentDto
        {
            Id = appointment.Id,
            StartTime = appointment.StartTime,
            ClientId = appointment.ClientId,
            MasterId = appointment.MasterId,
            ServiceId = appointment.ServiceId,
            IsRegular = appointment.IsRegular
        };
    }

    /// <summary>
    /// Обновить существующую запись.
    /// </summary>
    /// <param name="id">Идентификатор обновляемой записи.</param>
    /// <param name="dto">Новые данные записи.</param>
    public async Task<bool> UpdateAsync(Guid id, CreateAppointmentDto dto)
    {
        var existing = await _appointmentRepo.GetByIdAsync(id);
        if (existing == null) return false; 

        var updated = new Appointment
        {
            Id = id,
            StartTime = dto.StartTime,
            ClientId = dto.ClientId,
            MasterId = dto.MasterId,
            ServiceId = dto.ServiceId,
            IsRegular = dto.IsRegular,
            Client = null!,
            Master = null!,
            Service = null!
        };
        await _appointmentRepo.UpdateAsync(updated);
        return true; 
    }

    /// <summary>
    /// Удалить запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой записи.</param>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _appointmentRepo.GetByIdAsync(id);
        if (existing == null) return false; 

        await _appointmentRepo.DeleteAsync(id);
        return true; 
    }
}