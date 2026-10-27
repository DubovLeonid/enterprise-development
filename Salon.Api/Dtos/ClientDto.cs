using Salon.Domain.Enums;

namespace Salon.Api.Dtos;

/// <summary>
/// Информация о клиенте салона .
/// </summary>
public class ClientDto
{
    /// <summary>Уникальный идентификатор клиента.</summary>
    public Guid Id { get; set; }

    /// <summary>ФИО клиента.</summary>
    public string FullName { get; set; } = "";

    /// <summary>Пол клиента.</summary>
    public Gender Gender { get; set; }

    /// <summary>Дата рождения клиента.</summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>Контактный номер телефона.</summary>
    public string Phone { get; set; } = "";
}

/// <summary>
/// Данные для создания или обновления информации о клиенте.
/// </summary>
public class CreateClientDto
{
    /// <summary>ФИО клиента.</summary>
    public string FullName { get; set; } = "";

    /// <summary>Пол клиента (Male / Female).</summary>
    public Gender Gender { get; set; }

    /// <summary>Дата рождения клиента.</summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>Контактный номер телефона.</summary>
    public string Phone { get; set; } = "";
}