using Salon.Domain;

using Xunit.Abstractions;

namespace Salon.Tests;

/// <summary>
/// Unit-тесты для предметной области «Салон красоты».
/// </summary>
public class SalonTests(DataSeed data, ITestOutputHelper output) : IClassFixture<DataSeed>
{
    private readonly DataSeed _data = data;
    private readonly ITestOutputHelper _output = output;

    /// <summary>
    /// Тест 1. Выводит всех мастеров со стажем не менее 5 лет.
    /// </summary>
    [Fact]
    public void Test1_MastersWithExperienceAtLeast5Years()
    {

        var expectedCount = 7;


        var result = _data.Masters
            .Where(m => m.ExperienceYears >= 5)
            .OrderByDescending(m => m.ExperienceYears)
            .ThenBy(m => m.FullName)
            .ToList();


        _output.WriteLine($"Мастера со стажем ≥ 5 лет (найдено: {result.Count}):");
        foreach (var m in result)
            _output.WriteLine("  - " + m);

        Assert.Equal(expectedCount, result.Count);
    }

    /// <summary>
    /// Тест 2. Выводит свободные окошки выбранного мастера (строгая трактовка).
    /// </summary>
    [Fact]
    public void Test2_FreeSlotsForSelectedMaster_Strict()
    {

        var selectedMaster = _data.Masters[0];
        var expectedDuration = TimeSpan.FromMinutes(30);


        var appointments = _data.Appointments
            .Where(a => a.MasterId == selectedMaster.Id)
            .OrderBy(a => a.DateTime)
            .ToList();

        var windows = new List<(DateTime Start, DateTime End, TimeSpan Duration)>();

        for (var i = 0; i < appointments.Count - 1; i++)
        {
            var current = appointments[i];
            var next = appointments[i + 1];
            var endCurrent = current.DateTime + current.Service.Duration;
            var free = next.DateTime - endCurrent;

            if (free > TimeSpan.Zero)
                windows.Add((endCurrent, next.DateTime, free));
        }


        _output.WriteLine($"Свободные окошки у мастера {selectedMaster.FullName}:");
        foreach (var (start, end, duration) in windows)
            _output.WriteLine($"  {start:dd.MM.yyyy HH:mm} — {end:HH:mm} ({duration.TotalMinutes:F0} мин.)");

        Assert.Single(windows);
        Assert.Equal(expectedDuration, windows[0].Duration);

        var secondGap = appointments[2].DateTime
            - (appointments[1].DateTime + appointments[1].Service.Duration);
        Assert.True(secondGap <= TimeSpan.Zero);
    }

    /// <summary>
    /// Тест 3. Выводит топ-5 наиболее популярных услуг по числу записей.
    /// </summary>
    [Fact]
    public void Test3_Top5MostPopularServices()
    {
       
        var expectedCount = 5;
        var expectedNames = new[] { "Стрижка", "Окрашивание", "Маникюр", "Укладка", "Педикюр" };
        var expectedCounts = new[] { 6, 5, 4, 3, 2 };

        
        var result = _data.Appointments
            .GroupBy(a => a.Service)
            .Select(g => new { Service = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Service.Name)
            .Take(5)
            .ToList();

        
        _output.WriteLine("Топ-5 популярных услуг:");
        foreach (var t in result)
            _output.WriteLine($"  {t.Service.Name} — {t.Count} записей");

        Assert.Equal(expectedCount, result.Count);

        for (var i = 0; i < expectedCount; i++)
        {
            Assert.Equal(expectedNames[i], result[i].Service.Name);
            Assert.Equal(expectedCounts[i], result[i].Count);
        }
    }

    /// <summary>
    /// Тест 4. Считает количество повторных записей клиентов за последний месяц (строгая трактовка).
    /// </summary>
    [Fact]
    public void Test4_RepeatAppointmentsLastMonth_Strict()
    {

        var endDate = new DateTime(2026, 9, 29);
        var startDate = endDate.AddMonths(-1);
        var expectedTotalRepeats = 5;
        var expectedAnnaRepeats = 2;


        var perClient = _data.Appointments
            .Where(a => a.DateTime >= startDate && a.DateTime < endDate && a.IsRegular)
            .GroupBy(a => a.Client)
            .Select(g => new
            {
                Client = g.Key,
                Total = g.Count(),
                Repeat = g.Count() - 1
            })
            .Where(x => x.Repeat > 0)
            .OrderBy(x => x.Client.BirthDate)
            .ToList();

        var totalRepeats = perClient.Sum(x => x.Repeat);


        _output.WriteLine($"Повторные записи за последний месяц ({startDate:dd.MM.yyyy} — {endDate:dd.MM.yyyy}):");
        foreach (var x in perClient)
            _output.WriteLine($"  {x.Client.FullName}: всего {x.Total}, повторных {x.Repeat}");
        _output.WriteLine($"ИТОГО повторных записей: {totalRepeats}");

        for (var i = 0; i < perClient.Count - 1; i++)
            Assert.True(perClient[i].Client.BirthDate <= perClient[i + 1].Client.BirthDate);

        var anna = perClient.First(x => x.Client.FullName == "Иванова Анна Сергеевна");
        Assert.Equal(expectedAnnaRepeats, anna.Repeat);
        Assert.Equal(expectedTotalRepeats, totalRepeats);
    }

    /// <summary>
    /// Тест 5. Выводит клиентов, записанных более чем к одному мастеру.
    /// </summary>
    [Fact]
    public void Test5_ClientsWithSeveralMasters()
    {

        var result = _data.Appointments
            .GroupBy(a => a.Client)
            .Where(g => g.Select(a => a.Master.PassportNumber).Distinct().Count() > 1)
            .Select(g => new
            {
                Client = g.Key,
                MastersCount = g.Select(a => a.Master.PassportNumber).Distinct().Count()
            })
            .OrderBy(x => x.Client.BirthDate)
            .ToList();


        _output.WriteLine("Клиенты, записанные к нескольким мастерам:");
        foreach (var x in result)
            _output.WriteLine($"  {x.Client.FullName} (д.р. {x.Client.BirthDate:dd.MM.yyyy}) — мастеров: {x.MastersCount}");

        Assert.NotEmpty(result);

        for (var i = 0; i < result.Count - 1; i++)
            Assert.True(result[i].Client.BirthDate <= result[i + 1].Client.BirthDate);
    }
}