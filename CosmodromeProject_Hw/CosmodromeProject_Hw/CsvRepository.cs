using System;
using System.Collections.Generic;
using System.IO;

namespace CosmodromeProject_Hw
{
    /// <summary>
    /// Репозиторий, читающий данные из CSV-файлов.
    /// </summary>
    public class CsvRepository
    {
        /// <summary>
        /// Путь к папке с CSV-файлами.
        /// </summary>
        private string _basePath;

        /// <summary>
        /// Создаёт репозиторий с указанием папки с CSV-файлами.
        /// </summary>
        /// <param name="basePath">Путь к папке с CSV-файлами.</param>
        public CsvRepository(string basePath)
        {
            _basePath = basePath;
        }

        /// <summary>
        /// Читает миссии из файла missions.csv.
        /// </summary>
        /// <returns>Список миссий.</returns>
        public List<Mission> GetMissions()
        {
            List<Mission> result = new List<Mission>();
            string path = Path.Combine(_basePath, "missions.csv");
            string[] lines = File.ReadAllLines(path);

            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 4) continue;

                Mission m = new Mission(
                    int.Parse(parts[0]),
                    parts[1],
                    DateTime.ParseExact(parts[2], "dd.MM.yyyy", null),
                    parts[3]
                );

                result.Add(m);
            }

            return result;
        }

        /// <summary>
        /// Читает космонавтов из файла cosmonauts.csv.
        /// </summary>
        /// <returns>Список космонавтов.</returns>
        public List<Cosmonaut> GetCosmonauts()
        {
            List<Cosmonaut> result = new List<Cosmonaut>();
            string path = Path.Combine(_basePath, "cosmonauts.csv");
            string[] lines = File.ReadAllLines(path);

            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 4) continue;

                Cosmonaut c = new Cosmonaut(
                    int.Parse(parts[0]),
                    parts[1],
                    int.Parse(parts[2]),
                    parts[3]
                );

                result.Add(c);
            }

            return result;
        }

        /// <summary>
        /// Читает ракеты из файла rockets.csv.
        /// </summary>
        /// <returns>Список ракет.</returns>
        public List<Rocket> GetRockets()
        {
            List<Rocket> result = new List<Rocket>();
            string path = Path.Combine(_basePath, "rockets.csv");
            string[] lines = File.ReadAllLines(path);

            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 6) continue;

                Rocket r = new Rocket(
                    int.Parse(parts[0]),
                    parts[1],
                    int.Parse(parts[2]),
                    int.Parse(parts[3]),
                    int.Parse(parts[4]),
                    int.Parse(parts[5])
                );

                result.Add(r);
            }

            return result;
        }
    }
}