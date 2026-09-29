using System;
using System.Collections.Generic;

namespace BeautySalon.Models
{
    /// <summary>Пол человека.</summary>
    public enum Gender
    {
        
        Male,

     
        Female
    }

    /// <summary>
    /// Базовый класс для клиента и мастера салона красоты.
    /// </summary>
    public abstract class Person
    {
        /// <summary>Уникальный идентификатор персоны.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>Полное имя.</summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>Пол.</summary>
        public Gender Gender { get; set; }

        /// <summary>Дата рождения.</summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Возвращает строку персоны:
        /// </summary>
        public override string ToString() =>
            $"{FullName} ({Gender}, {BirthDate:dd.MM.yyyy})";
    }

    /// <summary>Клиент салона красоты.</summary>
    public class Client : Person
    {
        /// <summary>Контактный телефон.</summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Возвращает строку клиента,
        /// включая телефон.
        /// </summary>
        public override string ToString() =>
            $"{base.ToString()}, тел.: {Phone}";
    }

    /// <summary>Мастер салона красоты.</summary>
    public class Master : Person
    {
        /// <summary>Серия и номер паспорта.</summary>
        public string PassportNumber { get; set; } = string.Empty;

        /// <summary>Специализация.</summary>
        public string Specialization { get; set; } = string.Empty;

        /// <summary>Стаж работы.</summary>
        public int ExperienceYears { get; set; }

        /// <summary>
        /// Возвращает строку мастера, включая
        /// паспорт, специализацию и стаж работы.
        /// </summary>
        public override string ToString() =>
            $"{base.ToString()}, паспорт: {PassportNumber}, " +
            $"спец.: {Specialization}, стаж: {ExperienceYears} лет";
    }

    /// <summary>Категории услуг.</summary>
    public enum ServiceCategory
    {
        /// <summary>Парикмахерские услуги.</summary>
        Hairdressing,

        /// <summary>Маникюр и педикюр.</summary>
        Nails,

        /// <summary>Косметология.</summary>
        Cosmetology,

        /// <summary>Макияж.</summary>
        Makeup,

        /// <summary>Массаж.</summary>
        Massage
    }

    /// <summary>Услуга, оказываемая салоном.</summary>
    public class Service
    {
        /// <summary>Уникальный идентификатор услуги.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>Название услуги.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Категория услуги.</summary>
        public ServiceCategory Category { get; set; }

        /// <summary>Стоимость услуги.</summary>
        public decimal Price { get; set; }

        /// <summary>Длительность услуги.</summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Возвращает строку услуги:
        /// название, категория, цена и длительность.
        /// </summary>

        public override string ToString() =>
            $"{Name} ({Category}), {Price:C}, {Duration.TotalMinutes} мин.";
    }

    /// <summary>Запись клиента на услугу к мастеру.</summary>
    public class Appointment
    {
        /// <summary>Уникальный идентификатор записи.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>Дата и время начала услуги.</summary>
        public DateTime DateTime { get; set; }

        /// <summary>Идентификатор клиента.</summary>
        public Guid ClientId { get; set; }

        /// <summary>Идентификатор мастера.</summary>
        public Guid MasterId { get; set; }

        /// <summary>Идентификатор услуги.</summary>
        public Guid ServiceId { get; set; }

        /// <summary>Признак постоянного клиента.</summary>
        public bool IsRegular { get; set; }

        /// <summary>Клиент, записавшийся на услугу.</summary>
        public Client Client { get; set; } = null!;

        /// <summary>Мастер, оказывающий услугу.</summary>
        public Master Master { get; set; } = null!;

        /// <summary>Услуга, на которую записан клиент.</summary>
        public Service Service { get; set; } = null!;

        /// <summary>
        /// Возвращает строку записи:
        /// дата, клиент, мастер и услуга.
        /// </summary>
        public override string ToString() =>
            $"{DateTime:dd.MM.yyyy HH:mm} | {Client.FullName} - {Master.FullName} | {Service.Name}";
    }
}