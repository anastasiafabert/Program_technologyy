using System;

namespace CosmodromeProject_Hw
{
    
    public class Rocket
    {
        /// <summary>
        /// Уникальный идентификатор ракеты.Модель ракеты.FK на Mission. ключ на Cosmonaut, топливо кг, нагрузка кг, 
        /// </summary>

        public int Id { get; set; }

        /// <summary>
        /// Модель ракеты
        /// </summary>
     

        public string Model { get; set; }
        /// <summary>
        /// FK на Mission
        /// </summary>
    

        public int MissionId { get; set; }
        /// <summary>
        /// ключ на Cosmonaut
        /// </summary>
 

        public int CosmonautId { get; set; }
        /// <summary>
        /// топливо кг
        /// </summary>
    

        public int Fuel { get; set; }

        /// <summary>
        /// нагрузка кг
        /// </summary>

        public int Payload { get; set; }

       
        public bool IsHeavy
        {
            get { return Payload > 5000; }
        }

        /// <summary>
        /// Информация о ракете.
        /// </summary>
        public string GetInfo()
        {
            return $"{Model} ({Payload} кг полезной нагрузки)";
        }

        /// <summary>
        /// Конструктор с проверками.
        /// </summary>
        public Rocket(int id, string model, int missionId, int cosmonautId, int fuel, int payload)
        {
            if (id <= 0)
                throw new ArgumentException("Id должен быть больше 0");
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model не может быть пустым");
            if (missionId <= 0)
                throw new ArgumentException("MissionId должен быть больше 0");
            if (cosmonautId <= 0)
                throw new ArgumentException("CosmonautId должен быть больше 0");
            if (fuel < 0)
                throw new ArgumentException("Fuel не может быть отрицательным");
            if (payload < 0)
                throw new ArgumentException("Payload не может быть отрицательным");

            Id = id;
            Model = model;
            MissionId = missionId;
            CosmonautId = cosmonautId;
            Fuel = fuel;
            Payload = payload;
        }
    }
}