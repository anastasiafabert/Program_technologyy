using System;
using System.Collections.Generic;

namespace CosmodromeProject_Hw
{
    /// <summary>
    /// Репозиторий с данными в памяти.
    /// </summary>
    public class InMemoryRepository
    {
        private List<Mission> _missions;
        private List<Cosmonaut> _cosmonauts;
        private List<Rocket> _rockets;

        /// <summary>
        /// Создаёт репозиторий и заполняет его тестовыми данными.
        /// </summary>
        public InMemoryRepository()
        {
            _missions = new List<Mission>
            {
                new Mission(1, "Луна-25", new DateTime(2025, 9, 1), "Луна"),
                new Mission(2, "Марс-1", new DateTime(2026, 3, 15), "Марс"),
                new Mission(3, "Венера-Д", new DateTime(2027, 6, 20), "Венера"),
                new Mission(4, "Луна-26", new DateTime(2028, 1, 10), "Луна"),
                new Mission(5, "Юпитер-1", new DateTime(2030, 5, 5), "Юпитер")
            };

            _cosmonauts = new List<Cosmonaut>
            {
                new Cosmonaut(1, "Иванов И.И.", 10, "Капитан"),
                new Cosmonaut(2, "Петров П.П.", 8, "Майор"),
                new Cosmonaut(3, "Сидоров С.С.", 4, "Лейтенант"),
                new Cosmonaut(4, "Орлов А.А.", 6, "Капитан"),
                new Cosmonaut(5, "Соколов П.П.", 3, "Лейтенант")
            };

            _rockets = new List<Rocket>
            {
                new Rocket(1, "Союз-2", 1, 1, 5000, 12000),
                new Rocket(2, "Протон-М", 2, 2, 8000, 15000),
                new Rocket(3, "Ангара-А5", 3, 3, 7000, 9000),
                new Rocket(4, "Союз-2", 4, 4, 5000, 6000),
                new Rocket(5, "Восток", 1, 5, 3000, 4000)
            };
        }

        
        public List<Mission> GetMissions() { return _missions; }

       
        public List<Cosmonaut> GetCosmonauts() { return _cosmonauts; }

        
        public List<Rocket> GetRockets() { return _rockets; }
    }
}