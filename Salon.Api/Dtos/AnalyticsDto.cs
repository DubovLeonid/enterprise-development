namespace Salon.Api.Dtos;


/// <summary>
/// Информация о мастере, подходящем по стажу.
/// </summary>
public class ExperiencedMasterDto
{
    /// <summary>Уникальный идентификатор мастера.</summary>
    public Guid Id { get; set; }

    /// <summary>ФИО мастера.</summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>Специализация мастера.</summary>
    public string Specialization { get; set; } = string.Empty;

    /// <summary>Стаж работы в годах.</summary>
    public int ExperienceYears { get; set; }
}

/// <summary>
/// Ответ API со списком опытных мастеров.
/// </summary>
public class ExperiencedMastersResponseDto
{
    /// <summary>Запрошенный минимальный стаж.</summary>
    public int MinYears { get; set; }

    /// <summary>Количество найденных мастеров.</summary>
    public int Count { get; set; }

    /// <summary>Список мастеров.</summary>
    public List<ExperiencedMasterDto> Masters { get; set; } = [];
}


/// <summary>
/// Свободное окно в расписании мастера.
/// </summary>
public class FreeSlotDto
{
    /// <summary>Время начала свободного окна.</summary>
    public DateTime Start { get; set; }

    /// <summary>Время окончания свободного окна.</summary>
    public DateTime End { get; set; }

    /// <summary>Длительность окна в минутах.</summary>
    public int DurationMinutes { get; set; }
}

/// <summary>
/// Ответ API со списком свободных окон выбранного мастера.
/// </summary>
public class FreeSlotsResponseDto
{
    /// <summary>ФИО мастера.</summary>
    public string Master { get; set; } = string.Empty;

    /// <summary>Количество найденных окон.</summary>
    public int Count { get; set; }

    /// <summary>Список окон.</summary>
    public List<FreeSlotDto> Windows { get; set; } = [];
}



/// <summary>
/// Информация об услуге в топе популярности.
/// </summary>
public class TopServiceDto
{
    /// <summary>Уникальный идентификатор услуги.</summary>
    public Guid ServiceId { get; set; }

    /// <summary>Название услуги.</summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>Категория услуги.</summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>Количество записей на эту услугу.</summary>
    public int Count { get; set; }
}

/// <summary>
/// Ответ API с топом популярных услуг.
/// </summary>
public class TopServicesResponseDto
{
    /// <summary>Общее количество услуг, попавших в топ.</summary>
    public int Total { get; set; }

    /// <summary>Список популярных услуг.</summary>
    public List<TopServiceDto> Top { get; set; } = [];
}



/// <summary>
/// Статистика по повторным записям конкретного клиента.
/// </summary>
public class RepeatClientDto
{
    /// <summary>Уникальный идентификатор клиента.</summary>
    public Guid ClientId { get; set; }

    /// <summary>ФИО клиента.</summary>
    public string ClientName { get; set; } = string.Empty;

    /// <summary>Общее количество записей клиента за период.</summary>
    public int Total { get; set; }

    /// <summary>Количество повторных записей.</summary>
    public int Repeat { get; set; }
}

/// <summary>
/// Ответ API со статистикой повторных записей за месяц.
/// </summary>
public class RepeatAppointmentsResponseDto
{
    /// <summary>Дата начала периода.</summary>
    public DateTime PeriodStart { get; set; }

    /// <summary>Дата окончания периода.</summary>
    public DateTime PeriodEnd { get; set; }

    /// <summary>Общее количество повторных записей у всех клиентов.</summary>
    public int TotalRepeats { get; set; }

    /// <summary>Информация по клиентам.</summary>
    public List<RepeatClientDto> Clients { get; set; } = [];
}



/// <summary>
/// Информация о клиенте, который ходит к нескольким мастерам.
/// </summary>
public class LoyalClientDto
{
    /// <summary>Уникальный идентификатор клиента.</summary>
    public Guid ClientId { get; set; }

    /// <summary>ФИО клиента.</summary>
    public string ClientName { get; set; } = string.Empty;

    /// <summary>Дата рождения клиента.</summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>Количество уникальных мастеров, к которым записан клиент.</summary>
    public int MastersCount { get; set; }
}

/// <summary>
/// Ответ API со списком лояльных клиентов.
/// </summary>
public class LoyalClientsResponseDto
{
    /// <summary>Количество найденных клиентов.</summary>
    public int Count { get; set; }

    /// <summary>Список клиентов.</summary>
    public List<LoyalClientDto> Clients { get; set; } = [];
}