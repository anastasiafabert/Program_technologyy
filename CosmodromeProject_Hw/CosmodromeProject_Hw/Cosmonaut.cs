using System;

namespace CosmodromeProject_Hw
{
    /// <summary>
    /// Космонавт.
    /// </summary>
    public class Cosmonaut
    {
        /// <summary>
        /// свойство класса, униквальный код, полное имя космонавта, опыт в годах, звание
        /// </summary>
        public int Id { get; set; }

        public string FullName { get; set; }

        public int Experience { get; set; }

        public string Rank { get; set; }

        /// <summary>
        /// true, если космонавт опытный (больше 5 лет).
        /// </summary>
        public bool IsExperienced
        {
            get { return Experience > 5; }
        }

        /// <summary>
        /// Возвращает информацию о космонавте.
        /// </summary>
        public string GetInfo()
        {
            return $"{FullName} ({Experience} лет, {Rank})";
        }

        /// <summary>
        /// Конструктор с проверками.
        /// </summary>
        public Cosmonaut(int id, string fullName, int experience, string rank)
        {
            if (id <= 0)
                throw new ArgumentException("Id должен быть больше 0");
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("FullName не может быть пустым");
            if (experience < 0)
                throw new ArgumentException("Experience не может быть отрицательным");
            if (string.IsNullOrWhiteSpace(rank))
                throw new ArgumentException("Rank не может быть пустым");

            Id = id;
            FullName = fullName;
            Experience = experience;
            Rank = rank;
        }
    }
}