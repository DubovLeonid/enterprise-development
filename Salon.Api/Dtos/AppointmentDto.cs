namespace Salon.Api.Dtos;

/// <summary>
/// Информация о записи клиента на услугу.
/// </summary>
public class AppointmentDto
{
    /// <summary>Уникальный идентификатор записи.</summary>
    public Guid Id { get; set; }

    /// <summary>Дата и время начала записи.</summary>
    public DateTime StartTime { get; set; }

    /// <summary>Уникальный идентификатор клиента.</summary>
    public Guid ClientId { get; set; }

    /// <summary>ФИО клиента.</summary>
    public string ClientName { get; set; } = "";

    /// <summary>Уникальный идентификатор мастера.</summary>
    public Guid MasterId { get; set; }

    /// <summary>ФИО мастера.</summary>
    public string MasterName { get; set; } = "";

    /// <summary>Уникальный идентификатор услуги.</summary>
    public Guid ServiceId { get; set; }

    /// <summary>Название услуги.</summary>
    public string ServiceName { get; set; } = "";

    /// <summary>Признак того, что клиент является постоянным.</summary>
    public bool IsRegular { get; set; }
}

/// <summary>
/// Данные для создания новой записи на услугу.
/// </summary>
public class CreateAppointmentDto
{
    /// <summary> дата и время начала записи.</summary>
    public DateTime StartTime { get; set; }

    /// <summary>ID клиента, который записывается .</summary>
    public Guid ClientId { get; set; }

    /// <summary>ID мастера, к которому идет запись.</summary>
    public Guid MasterId { get; set; }

    /// <summary>ID услуги, на которую записываются.</summary>
    public Guid ServiceId { get; set; }

    /// <summary>Является ли клиент постоянным.</summary>
    public bool IsRegular { get; set; }
}