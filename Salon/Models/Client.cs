namespace BeautySalon.Models
{
    /// <summary>Клиент салона красоты.</summary>
    public class Client : Person
    {
        /// <summary>Контактный телефон.</summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>Строковое представление клиента, включая телефон.</summary>
        public override string ToString() =>
            $"{base.ToString()}, тел.: {Phone}";
    }
}