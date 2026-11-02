using Salon.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Salon.Contracts.Dtos;
/// <summary>
/// Данные для создания или обновления услуги.
/// </summary>
public class CreateServiceDto
{
    [Required(ErrorMessage = "Название обязательно")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ServiceCategory Category { get; set; }

    [Range(0.01, 100000)]
    public decimal Price { get; set; }

    [Range(1, 480)]
    public int DurationMinutes { get; set; }
}