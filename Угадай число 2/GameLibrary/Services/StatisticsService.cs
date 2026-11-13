using System.IO;
using System.Text.Json;
using GameLibrary.Models;
using GameLibrary.Enums;


namespace GameLibrary.Services
{
    /// <summary>
    /// Управляет статистикой игрового процесса.
    /// </summary>
    public class StatisticsService
    {
        /// <summary>
        /// Имя файла для хранения статистики.
        /// </summary>
        private const string FilePath = "statistics.json";

        /// <summary>
        /// Хранит статистику сыгранных игр.
        /// </summary>
        private readonly GameStatistics _statistics;

        private GuessingGame? _game;
        private DifficultyLevel _difficulty;

        /// <summary>
        /// Создаёт сервис для работы со статистикой.
        /// </summary>
        /// <param name="statistics">Объект статистики.</param>
        public StatisticsService(GameStatistics statistics)
        {
            _statistics = statistics;
        }

        /// <summary>
        /// Загружает статистику из файла.
        /// </summary>
        public void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);

                GameStatistics? loadedStatistics =
                    JsonSerializer.Deserialize<GameStatistics>(json);

                if (loadedStatistics != null)
                {
                    _statistics.TotalGames = loadedStatistics.TotalGames;
                    _statistics.Wins = loadedStatistics.Wins;
                    _statistics.Losses = loadedStatistics.Losses;
                    _statistics.CurrentWinStreak = loadedStatistics.CurrentWinStreak;
                    _statistics.BestWinStreak = loadedStatistics.BestWinStreak;

                    _statistics.BestEasyResult = loadedStatistics.BestEasyResult;
                    _statistics.BestMediumResult = loadedStatistics.BestMediumResult;
                    _statistics.BestHardResult = loadedStatistics.BestHardResult;
                }
            }
        }

        /// <summary>
        /// Сохраняет статистику в файл.
        /// </summary>
        public void Save()
        {
            string json = JsonSerializer.Serialize(_statistics,
                new JsonSerializerOptions { WriteIndented = true });//WriteIndented = true делает JSON красивым и удобным для чтения

            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Подписывает сервис статистики на события игры.
        /// </summary>
        public void Subscribe(GuessingGame game, DifficultyLevel difficulty)
        {
            _game = game;
            _difficulty = difficulty;

            game.GameWon += OnGameWon;
            game.GameLost += OnGameLost;
        }

        /// <summary>
        /// Обновляет статистику после победы игрока.
        /// </summary>
        private void OnGameWon()
        {
            // Проверяем, существует ли текущая игра.
            if (_game == null)
            {
                return;
            }

            _statistics.TotalGames++;
            _statistics.Wins++;

            _statistics.CurrentWinStreak++;

            if (_statistics.CurrentWinStreak > _statistics.BestWinStreak)
            {
                _statistics.BestWinStreak = _statistics.CurrentWinStreak;
            }


            // Получаем общее количество использованных попыток.
            int attempts = _game.TotalAttemptsUsed;

            // Обновляем рекорд в зависимости от уровня сложности.
            if (_difficulty == DifficultyLevel.Easy)
            {
                if (_statistics.BestEasyResult == 0 || attempts < _statistics.BestEasyResult)
                {
                    _statistics.BestEasyResult = attempts;
                }
            }
            // Обновляем рекорд среднего уровня.
            if (_difficulty == DifficultyLevel.Medium)
            {
                if (_statistics.BestMediumResult == 0 || attempts < _statistics.BestMediumResult)
                {
                    _statistics.BestMediumResult = attempts;
                }
            }

            // Обновляем рекорд сложного уровня.
            if (_difficulty == DifficultyLevel.Hard)
            {
                if (_statistics.BestHardResult == 0 || attempts < _statistics.BestHardResult)
                {
                    _statistics.BestHardResult = attempts;
                }
            }
            Save();
        }

        /// <summary>
        /// Обновляет статистику после поражения игрока.
        /// </summary>
        private void OnGameLost()
        {
            _statistics.TotalGames++;
            _statistics.Losses++;

            _statistics.CurrentWinStreak = 0;

            Save();
        }

    }
}