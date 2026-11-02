using Salon.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Salon.Contracts.Dtos;
/// <summary>
/// Данные для создания или обновления мастера.
/// </summary>
public class CreateMasterDto
{
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public DateOnly BirthDate { get; set; }

    [Required(ErrorMessage = "Паспорт обязателен")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Паспорт должен содержать 10 цифр")]
    public string PassportNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Специализация обязательна")]
    [MaxLength(100)]
    public string Specialization { get; set; } = string.Empty;

    [Range(0, 60)]
    public int ExperienceYears { get; set; }
}