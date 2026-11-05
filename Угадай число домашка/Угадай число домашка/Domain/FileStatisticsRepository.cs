using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace Угадай_число_домашка
{
    /// <summary>
    /// Сохранение статистики в JSON-файл.
    /// </summary>
    public class FileStatisticsRepository : IStatisticsRepository
    {
        private const string FilePath = "stats.json";

        public GameStatistics Load()
        {
            if (!File.Exists(FilePath)) return new GameStatistics();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<GameStatistics>(json) ?? new GameStatistics();
        }

        public void Save(GameStatistics stats)
        {
            var json = JsonSerializer.Serialize(stats);
            File.WriteAllText(FilePath, json);
        }
    }
}