using System;
using System.Collections.Generic;
using System.Linq;
using Salon.Models;
using Xunit;
using Xunit.Abstractions;
using Salon;
namespace Salon.Tests;

/// <summary>
/// Unit-тесты для предметной области «Салон красоты».
/// </summary>
public class SalonTests
{
    /// <summary>
    /// Помощник xUnit для вывода отладочных сообщений в лог теста.
    /// </summary>
    private readonly ITestOutputHelper _output;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public SalonTests(ITestOutputHelper output)
    {
        _output = output;
    }

    /// <summary>
    /// Тест 1. Выводит всех мастеров со стажем не менее 5 лет.
    /// </summary>
    [Fact]
    public void Test1_MastersWithExperienceAtLeast5Years()
    {
        var result = DataSeed.Masters
            .Where(m => m.ExperienceYears >= 5)
            .OrderByDescending(m => m.ExperienceYears)
            .ThenBy(m => m.FullName)
            .ToList();

        _output.WriteLine($"Мастера со стажем ≥ 5 лет (найдено: {result.Count}):");
        foreach (var m in result)
            _output.WriteLine("  - " + m);

      
        Assert.Equal(7, result.Count);
        Assert.All(result, m => Assert.True(m.ExperienceYears >= 5));
    }


   
    /// <summary>
    /// Тест 2. Выводит свободные окошки выбранного мастера
    /// </summary>
    [Fact]
    public void Test2_FreeSlotsForSelectedMaster_Strict()
    {
        var selectedMaster = DataSeed.Masters[0]; 

        var appointments = DataSeed.Appointments
            .Where(a => a.MasterId == selectedMaster.Id)
            .OrderBy(a => a.DateTime)
            .ToList();

        var windows = new List<(DateTime Start, DateTime End, TimeSpan Duration)>();

        for (int i = 0; i < appointments.Count - 1; i++)
        {
            var current = appointments[i];
            var next = appointments[i + 1];

            var endCurrent = current.DateTime + current.Service.Duration;
            var free = next.DateTime - endCurrent;

            if (free > TimeSpan.Zero)
                windows.Add((endCurrent, next.DateTime, free));
        }

        _output.WriteLine($"Свободные окошки у мастера {selectedMaster.FullName}:");
        foreach (var w in windows)
            _output.WriteLine(
                $"  {w.Start:dd.MM.yyyy HH:mm} — {w.End:HH:mm} " +
                $"({w.Duration.TotalMinutes:F0} мин.)");

        
        Assert.Equal(1, windows.Count);
        Assert.Equal(TimeSpan.FromMinutes(30), windows[0].Duration);

        
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
        var result = DataSeed.Appointments
            .GroupBy(a => a.Service)
            .Select(g => new
            {
                Service = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Service.Name)
            .Take(5)
            .ToList();

        _output.WriteLine("Топ-5 популярных услуг:");
        foreach (var t in result)
            _output.WriteLine($"  {t.Service.Name} — {t.Count} записей");

        Assert.Equal(5, result.Count);

        Assert.Equal("Стрижка", result[0].Service.Name);
        Assert.Equal(6, result[0].Count);

        Assert.Equal("Окрашивание", result[1].Service.Name);
        Assert.Equal(5, result[1].Count);

        Assert.Equal("Маникюр", result[2].Service.Name);
        Assert.Equal(4, result[2].Count);

        Assert.Equal("Укладка", result[3].Service.Name);
        Assert.Equal(3, result[3].Count);

        Assert.Equal("Педикюр", result[4].Service.Name);
        Assert.Equal(2, result[4].Count);
    }


    /// <summary>
    /// Тест 4. Считает количество повторных записей клиентов
    /// </summary>
    [Fact]
    public void Test4_RepeatAppointmentsLastMonth_Strict()
    {
        var endDate = new DateTime(2026, 9, 29);
        var startDate = endDate.AddMonths(-1);

        var perClient = DataSeed.Appointments
            .Where(a =>
                a.DateTime >= startDate &&
                a.DateTime < endDate &&
                a.IsRegular)
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

        _output.WriteLine($"Повторные записи за последний месяц " +
                          $"({startDate:dd.MM.yyyy} — {endDate:dd.MM.yyyy}):");
        foreach (var x in perClient)
            _output.WriteLine($"  {x.Client.FullName}: " +
                              $"всего {x.Total}, повторных {x.Repeat}");
        _output.WriteLine($"ИТОГО повторных записей: {totalRepeats}");

    
        Assert.All(perClient, x => Assert.True(x.Repeat > 0));

        
        for (int i = 0; i < perClient.Count - 1; i++)
        {
            Assert.True(perClient[i].Client.BirthDate
                        <= perClient[i + 1].Client.BirthDate);
        }

      
        var anna = perClient.First(x =>
            x.Client.FullName == "Иванова Анна Сергеевна");
        Assert.Equal(2, anna.Repeat);

       
        Assert.Equal(5, totalRepeats);
    }

  
    /// <summary>
    /// Тест 5. Выводит клиентов, записанных более чем к одному мастеру
    /// </summary>
    [Fact]
    public void Test5_ClientsWithSeveralMasters()
    {
        var result = DataSeed.Appointments
            .GroupBy(a => a.Client)
            .Where(g =>
                g.Select(a => a.Master.PassportNumber)
                 .Distinct()
                 .Count() > 1)
            .Select(g => new
            {
                Client = g.Key,
                MastersCount = g
                    .Select(a => a.Master.PassportNumber)
                    .Distinct()
                    .Count()
            })
            .OrderBy(x => x.Client.BirthDate)
            .ToList();

        _output.WriteLine("Клиенты, записанные к нескольким мастерам:");
        foreach (var x in result)
            _output.WriteLine($"  {x.Client.FullName} " +
                              $"(д.р. {x.Client.BirthDate:dd.MM.yyyy}) — " +
                              $"мастеров: {x.MastersCount}");

        Assert.NotEmpty(result);

        
        for (int i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].Client.BirthDate
                        <= result[i + 1].Client.BirthDate);
        }

       
        Assert.All(result, x => Assert.True(x.MastersCount > 1));
    }
}