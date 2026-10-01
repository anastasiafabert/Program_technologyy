using System;

namespace CosmodromeProject_Hw
{
    /// <summary>
    /// Космическая миссия.
    /// </summary>
    public class Mission
    {
        /// <summary>
        /// Уникальный идентификатор миссии.,Название миссии. Дата запуска.Цель миссии.Информация о миссии в формате "Название (дата, цель)".
        /// </summary>
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Target { get; set; }

        public string Info
        {
            get { return $"{Name} ({Date:dd.MM.yyyy}, {Target})"; }
        }

        /// <summary>
        /// Конструктор с проверками правил предметной области.
        /// </summary>
        public Mission(int id, string name, DateTime date, string target)
        {
            if (id <= 0)
                throw new ArgumentException("Id должен быть больше 0");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name не может быть пустым");
            if (date.Year < 1900 || date.Year > 2100)
                throw new ArgumentException("Год должен быть от 1900 до 2100");
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentException("Target не может быть пустым");

            Id = id;
            Name = name;
            Date = date;
            Target = target;
        }
    }
}