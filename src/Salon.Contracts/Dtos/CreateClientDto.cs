using System.ComponentModel.DataAnnotations;
using Salon.Domain.Enums;

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
    [EnumDataType(typeof(Gender), ErrorMessage = "Недопустимое значение пола")] 
    public Gender Gender { get; set; }

    [Range(typeof(DateOnly), "1900-01-01", "2100-01-01", ErrorMessage = "Некорректная дата рождения")] 
    public DateOnly BirthDate { get; set; }
}