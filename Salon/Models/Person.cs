using System;

namespace BeautySalon.Models
{
    /// <summary>
    /// Базовый класс для клиента и мастера салона красоты.
    /// </summary>
    public abstract class Person
    {
        /// <summary>Уникальный идентификатор персоны.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>Полное имя (ФИО).</summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>Пол.</summary>
        public Gender Gender { get; set; }

        /// <summary>Дата рождения.</summary>
        public DateTime BirthDate { get; set; }

        /// <summary>Строковое представление персоны.</summary>
        public override string ToString() =>
            $"{FullName} ({Gender}, {BirthDate:dd.MM.yyyy})";
    }
}