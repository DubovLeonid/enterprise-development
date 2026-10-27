using Salon.Domain;
using Salon.Domain.Models;

namespace Salon.Api.Repositories;

/// <summary>
/// Хранилище данных в памяти. 
/// </summary>
public static class InMemoryStorage
{
    /// <summary>Коллекция всех клиентов салона.</summary>
    public static List<Client> Clients { get; } = [];

    /// <summary>Коллекция всех мастеров салона.</summary>
    public static List<Master> Masters { get; } = [];

    /// <summary>Справочник всех услуг салона.</summary>
    public static List<Service> Services { get; } = [];

    /// <summary>Коллекция всех записей клиентов на услуги.</summary>
    public static List<Appointment> Appointments { get; } = [];

    /// <summary>
    /// Статический конструктор.
    /// </summary>
    static InMemoryStorage()
    {
        var seed = new DataSeed();
        Clients.AddRange(seed.Clients);
        Masters.AddRange(seed.Masters);
        Services.AddRange(seed.Services);
        Appointments.AddRange(seed.Appointments);
    }
}