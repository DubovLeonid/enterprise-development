using System.ComponentModel.DataAnnotations;
namespace Salon.Contracts.Dtos;

/// <summary>
/// Данные для создания или обновления записи.
/// </summary>
public class CreateAppointmentDto
{
    [Required(ErrorMessage = "Дата и время обязательны")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "ID клиента обязателен")]
    public Guid ClientId { get; set; }

    [Required(ErrorMessage = "ID мастера обязателен")]
    public Guid MasterId { get; set; }

    [Required(ErrorMessage = "ID услуги обязателен")]
    public Guid ServiceId { get; set; }

    public bool IsRegular { get; set; }
}