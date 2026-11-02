using Salon.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Salon.Contracts.Dtos;
/// <summary>
/// Данные для создания или обновления клиента.
/// </summary>
public class CreateClientDto
{
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(150, ErrorMessage = "ФИО не должно превышать 150 символов")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Пол обязателен")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateOnly BirthDate { get; set; }
}