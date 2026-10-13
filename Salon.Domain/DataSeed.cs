using Salon.Domain.Enums;
using Salon.Domain.Models;

namespace Salon.Domain;

/// <summary>
/// Содержит по 10 экземпляров клиентов, мастеров, услуг
/// </summary>
public class DataSeed
{
    /// <summary>Список клиентов салона.</summary>
    public IReadOnlyList<Client> Clients { get; }

    /// <summary>Список мастеров салона.</summary>
    public IReadOnlyList<Master> Masters { get; }

    /// <summary>Список услуг салона.</summary>
    public IReadOnlyList<Service> Services { get; }

    /// <summary>Список записей клиентов на услуги.</summary>
    public IReadOnlyList<Appointment> Appointments { get; }

    /// <summary>
    /// Конструктор. Наполняет списки тестовыми данными.
    /// </summary>
    public DataSeed()
    {
        Clients = CreateClients();
        Masters = CreateMasters();
        Services = CreateServices();
        Appointments = CreateAppointments(Clients, Masters, Services);
    }

    private static List<Client> CreateClients() =>
    [
        new Client { Id = Guid.NewGuid(), FullName = "Иванова Анна Сергеевна",       Gender = Gender.Female, BirthDate = new DateOnly(1998, 4, 12),  Phone = "+79000000001" },
        new Client { Id = Guid.NewGuid(), FullName = "Петров Алексей Иванович",      Gender = Gender.Male,   BirthDate = new DateOnly(1995, 7, 20),  Phone = "+79000000002" },
        new Client { Id = Guid.NewGuid(), FullName = "Смирнова Мария Олеговна",      Gender = Gender.Female, BirthDate = new DateOnly(2000, 1, 15),  Phone = "+79000000003" },
        new Client { Id = Guid.NewGuid(), FullName = "Кузнецова Елена Андреевна",    Gender = Gender.Female, BirthDate = new DateOnly(1992, 5, 8),   Phone = "+79000000004" },
        new Client { Id = Guid.NewGuid(), FullName = "Волкова Ольга Петровна",       Gender = Gender.Female, BirthDate = new DateOnly(1988, 11, 30), Phone = "+79000000005" },
        new Client { Id = Guid.NewGuid(), FullName = "Соколов Дмитрий Сергеевич",    Gender = Gender.Male,   BirthDate = new DateOnly(2002, 2, 10),  Phone = "+79000000006" },
        new Client { Id = Guid.NewGuid(), FullName = "Попова Наталья Викторовна",    Gender = Gender.Female, BirthDate = new DateOnly(1997, 9, 22),  Phone = "+79000000007" },
        new Client { Id = Guid.NewGuid(), FullName = "Морозова Ирина Павловна",      Gender = Gender.Female, BirthDate = new DateOnly(1990, 6, 18),  Phone = "+79000000008" },
        new Client { Id = Guid.NewGuid(), FullName = "Лебедев Максим Александрович", Gender = Gender.Male,   BirthDate = new DateOnly(2001, 12, 5),  Phone = "+79000000009" },
        new Client { Id = Guid.NewGuid(), FullName = "Новикова Светлана Игоревна",   Gender = Gender.Female, BirthDate = new DateOnly(1996, 3, 25),  Phone = "+79000000010" }
    ];

    private static List<Master> CreateMasters() =>
    [
        new Master { Id = Guid.NewGuid(), FullName = "Александрова Елена Владимировна", Gender = Gender.Female, BirthDate = new DateOnly(1985, 3, 12),  PassportNumber = "7001000001", Specialization = "Парикмахер",      ExperienceYears = 8  },
        new Master { Id = Guid.NewGuid(), FullName = "Иванов Сергей Петрович",          Gender = Gender.Male,   BirthDate = new DateOnly(1990, 7, 21),  PassportNumber = "7001000002", Specialization = "Барбер",          ExperienceYears = 3  },
        new Master { Id = Guid.NewGuid(), FullName = "Павлова Ольга Николаевна",        Gender = Gender.Female, BirthDate = new DateOnly(1982, 2, 17),  PassportNumber = "7001000003", Specialization = "Колорист",        ExperienceYears = 12 },
        new Master { Id = Guid.NewGuid(), FullName = "Сидорова Марина Андреевна",       Gender = Gender.Female, BirthDate = new DateOnly(1988, 5, 14),  PassportNumber = "7001000004", Specialization = "Мастер маникюра", ExperienceYears = 6  },
        new Master { Id = Guid.NewGuid(), FullName = "Крылов Дмитрий Сергеевич",        Gender = Gender.Male,   BirthDate = new DateOnly(1995, 11, 5),  PassportNumber = "7001000005", Specialization = "Мастер педикюра", ExperienceYears = 2  },
        new Master { Id = Guid.NewGuid(), FullName = "Васильева Ирина Олеговна",        Gender = Gender.Female, BirthDate = new DateOnly(1984, 6, 25),  PassportNumber = "7001000006", Specialization = "Визажист",        ExperienceYears = 10 },
        new Master { Id = Guid.NewGuid(), FullName = "Федорова Екатерина Ильинична",    Gender = Gender.Female, BirthDate = new DateOnly(1993, 9, 30),  PassportNumber = "7001000007", Specialization = "Стилист",         ExperienceYears = 7  },
        new Master { Id = Guid.NewGuid(), FullName = "Николаев Артем Викторович",       Gender = Gender.Male,   BirthDate = new DateOnly(1992, 4, 19),  PassportNumber = "7001000008", Specialization = "Парикмахер",      ExperienceYears = 5  },
        new Master { Id = Guid.NewGuid(), FullName = "Орлова Татьяна Сергеевна",        Gender = Gender.Female, BirthDate = new DateOnly(1987, 8, 3),   PassportNumber = "7001000009", Specialization = "Косметолог",      ExperienceYears = 9  },
        new Master { Id = Guid.NewGuid(), FullName = "Максимов Павел Андреевич",        Gender = Gender.Male,   BirthDate = new DateOnly(1999, 12, 12), PassportNumber = "7001000010", Specialization = "Барбер",          ExperienceYears = 1  }
    ];

    private static List<Service> CreateServices() =>
    [
        new Service { Id = Guid.NewGuid(), Name = "Стрижка",          Category = ServiceCategory.Hairdressing, Price = 1500m, Duration = TimeSpan.FromMinutes(60)  },
        new Service { Id = Guid.NewGuid(), Name = "Окрашивание",      Category = ServiceCategory.Hairdressing, Price = 4500m, Duration = TimeSpan.FromMinutes(180) },
        new Service { Id = Guid.NewGuid(), Name = "Маникюр",          Category = ServiceCategory.Nails,        Price = 1800m, Duration = TimeSpan.FromMinutes(90)  },
        new Service { Id = Guid.NewGuid(), Name = "Укладка",          Category = ServiceCategory.Hairdressing, Price = 1200m, Duration = TimeSpan.FromMinutes(45)  },
        new Service { Id = Guid.NewGuid(), Name = "Педикюр",          Category = ServiceCategory.Nails,        Price = 2200m, Duration = TimeSpan.FromMinutes(120) },
        new Service { Id = Guid.NewGuid(), Name = "Макияж",           Category = ServiceCategory.Makeup,       Price = 2500m, Duration = TimeSpan.FromMinutes(90)  },
        new Service { Id = Guid.NewGuid(), Name = "Коррекция бровей", Category = ServiceCategory.Cosmetology,  Price = 800m,  Duration = TimeSpan.FromMinutes(30)  },
        new Service { Id = Guid.NewGuid(), Name = "Уход за лицом",    Category = ServiceCategory.Cosmetology,  Price = 3000m, Duration = TimeSpan.FromMinutes(60)  },
        new Service { Id = Guid.NewGuid(), Name = "Лечение волос",    Category = ServiceCategory.Hairdressing, Price = 3500m, Duration = TimeSpan.FromMinutes(120) },
        new Service { Id = Guid.NewGuid(), Name = "Массаж",           Category = ServiceCategory.Massage,      Price = 2800m, Duration = TimeSpan.FromMinutes(60)  }
    ];

    private static List<Appointment> CreateAppointments(
        IReadOnlyList<Client> c,
        IReadOnlyList<Master> m,
        IReadOnlyList<Service> s)
    {
        var appointments = new List<Appointment>();

        void Add(Client client, Master master, Service service, DateTime dt, bool isRegular)
        {
            appointments.Add(new Appointment
            {
                Id = Guid.NewGuid(),
                DateTime = dt,
                ClientId = client.Id,
                MasterId = master.Id,
                ServiceId = service.Id,
                Client = client,
                Master = master,
                Service = service,
                IsRegular = isRegular
            });
        }

        Add(c[0], m[0], s[0], new DateTime(2026, 9, 10, 9, 0, 0), true);
        Add(c[1], m[0], s[1], new DateTime(2026, 9, 10, 10, 30, 0), false);
        Add(c[2], m[0], s[2], new DateTime(2026, 9, 10, 13, 0, 0), true);

        Add(c[0], m[1], s[0], new DateTime(2026, 9, 2, 10, 0, 0), true);
        Add(c[3], m[2], s[1], new DateTime(2026, 9, 4, 10, 0, 0), false);
        Add(c[0], m[2], s[3], new DateTime(2026, 9, 5, 11, 0, 0), true);
        Add(c[1], m[1], s[0], new DateTime(2026, 9, 6, 10, 0, 0), true);
        Add(c[4], m[3], s[2], new DateTime(2026, 9, 7, 12, 0, 0), false);
        Add(c[5], m[4], s[4], new DateTime(2026, 9, 8, 13, 0, 0), true);
        Add(c[6], m[1], s[0], new DateTime(2026, 9, 9, 14, 0, 0), false);
        Add(c[7], m[5], s[1], new DateTime(2026, 9, 11, 10, 0, 0), true);
        Add(c[8], m[6], s[3], new DateTime(2026, 9, 12, 12, 0, 0), false);
        Add(c[9], m[7], s[2], new DateTime(2026, 9, 13, 15, 0, 0), true);
        Add(c[2], m[1], s[0], new DateTime(2026, 9, 14, 10, 0, 0), true);
        Add(c[3], m[2], s[1], new DateTime(2026, 9, 15, 11, 0, 0), true);
        Add(c[4], m[3], s[0], new DateTime(2026, 9, 15, 14, 0, 0), false);
        Add(c[5], m[4], s[2], new DateTime(2026, 9, 16, 13, 0, 0), true);
        Add(c[6], m[5], s[1], new DateTime(2026, 9, 17, 11, 0, 0), false);
        Add(c[7], m[6], s[4], new DateTime(2026, 9, 18, 12, 0, 0), true);
        Add(c[8], m[7], s[3], new DateTime(2026, 9, 19, 15, 0, 0), false);

        Add(c[9], m[9], s[5], new DateTime(2026, 7, 15, 10, 0, 0), true);
        Add(c[9], m[8], s[6], new DateTime(2026, 8, 15, 10, 0, 0), true);

        return appointments;
    }
}