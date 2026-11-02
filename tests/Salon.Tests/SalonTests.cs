using Salon.Application.Services;
using Salon.Domain.Seeds;
using Salon.Infrastructure.InMemory.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace Salon.Tests;

/// <summary>
/// Unit-тесты для предметной области «Салон красоты».
/// </summary>
public class SalonTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _output;
    private readonly DataSeed _data;
    private AnalyticsService _analyticsService = null!;

    private readonly InMemoryClientRepository _clientRepo = new();
    private readonly InMemoryMasterRepository _masterRepo = new();
    private readonly InMemoryServiceRepository _serviceRepo = new();
    private readonly InMemoryAppointmentRepository _appointmentRepo = new();

    public SalonTests(ITestOutputHelper output)
    {
        _output = output;
        _data = new DataSeed();
    }

    /// <summary>
    /// Асинхронная инициализация репозиториев перед запуском тестов.
    /// </summary>
    public async Task InitializeAsync()
    {
        foreach (var c in _data.Clients) await _clientRepo.AddAsync(c);
        foreach (var m in _data.Masters) await _masterRepo.AddAsync(m);
        foreach (var s in _data.Services) await _serviceRepo.AddAsync(s);
        foreach (var a in _data.Appointments) await _appointmentRepo.AddAsync(a);

        _analyticsService = new AnalyticsService(_masterRepo, _appointmentRepo, _serviceRepo, _clientRepo);
    }

    /// <summary>
    /// Уборка после тестов (ничего делать не нужно).
    /// </summary>
    public Task DisposeAsync() => Task.CompletedTask;

    /// <summary>
    /// Тест 1. Выводит всех мастеров со стажем не менее 5 лет.
    /// </summary>
    [Fact]
    public async Task Test1_MastersWithExperienceAtLeast5Years()
    {
        var expectedCount = 7;

        var result = (await _analyticsService.GetMastersWithExperienceAtLeast5YearsAsync()).ToList();

        _output.WriteLine($"Мастера со стажем ≥ 5 лет (найдено: {result.Count}):");
        foreach (var m in result)
            _output.WriteLine($"  - {m.FullName}, стаж: {m.ExperienceYears}");

        Assert.Equal(expectedCount, result.Count);

        for (var i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].ExperienceYears >= result[i + 1].ExperienceYears);
        }
    }

    /// <summary>
    /// Тест 2. Выводит свободные окошки выбранного мастера.
    /// </summary>
    [Fact]
    public async Task Test2_FreeSlotsForSelectedMaster_Strict()
    {
        var selectedMaster = _data.Masters[0];
        var expectedDurationMinutes = 30;

        var response = await _analyticsService.GetFreeSlotsForMasterAsync(selectedMaster.Id);
        var windows = response.Windows;

        _output.WriteLine($"Свободные окошки у мастера {response.Master}:");
        foreach (var window in windows)
            _output.WriteLine($"  {window.Start:dd.MM.yyyy HH:mm} — {window.End:HH:mm} ({window.DurationMinutes} мин.)");

        Assert.Single(windows);
        Assert.Equal(expectedDurationMinutes, windows[0].DurationMinutes);
    }

    /// <summary>
    /// Тест 3. Выводит топ-5 наиболее популярных услуг по числу записей.
    /// </summary>
    [Fact]
    public async Task Test3_Top5MostPopularServices()
    {
        var expectedCount = 5;
        var expectedNames = new[] { "Стрижка", "Окрашивание", "Маникюр", "Укладка", "Педикюр" };
        var expectedCounts = new[] { 6, 5, 4, 3, 2 };

        var response = await _analyticsService.GetTop5PopularServicesAsync();
        var top = response.Top;

        _output.WriteLine("Топ-5 популярных услуг:");
        foreach (var t in top)
            _output.WriteLine($"  {t.ServiceName} — {t.Count} записей");

        Assert.Equal(expectedCount, response.Total);

        for (var i = 0; i < expectedCount; i++)
        {
            Assert.Equal(expectedNames[i], top[i].ServiceName);
            Assert.Equal(expectedCounts[i], top[i].Count);
        }
    }

    /// <summary>
    /// Тест 4. Считает количество повторных записей клиентов за последний месяц.
    /// </summary>
    [Fact]
    public async Task Test4_RepeatAppointmentsLastMonth_Strict()
    {
        var expectedTotalRepeats = 5;
        var expectedAnnaRepeats = 2;

        var response = await _analyticsService.GetRepeatAppointmentsLastMonthAsync();
        var clients = response.Clients;

        _output.WriteLine($"Повторные записи за последний месяц ({response.PeriodStart:dd.MM.yyyy} — {response.PeriodEnd:dd.MM.yyyy}):");
        foreach (var x in clients)
            _output.WriteLine($"  {x.ClientName}: всего {x.Total}, повторных {x.Repeat}");
        _output.WriteLine($"ИТОГО повторных записей: {response.TotalRepeats}");

        var anna = clients.First(x => x.ClientName == "Иванова Анна Сергеевна");
        Assert.Equal(expectedAnnaRepeats, anna.Repeat);
        Assert.Equal(expectedTotalRepeats, response.TotalRepeats);
    }

    /// <summary>
    /// Тест 5. Выводит клиентов, записанных более чем к одному мастеру.
    /// </summary>
    [Fact]
    public async Task Test5_ClientsWithSeveralMasters()
    {
        var response = await _analyticsService.GetClientsWithSeveralMastersAsync();
        var clients = response.Clients;

        _output.WriteLine("Клиенты, записанные к нескольким мастерам:");
        foreach (var x in clients)
            _output.WriteLine($"  {x.ClientName} (д.р. {x.BirthDate:dd.MM.yyyy}) — мастеров: {x.MastersCount}");

        Assert.NotEmpty(clients);

        for (var i = 0; i < clients.Count - 1; i++)
        {
            Assert.True(clients[i].BirthDate <= clients[i + 1].BirthDate);
        }
    }
}