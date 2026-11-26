
using System.IO;
using System.Text.Json;
using GameLibrary.Interfaces;
using GameLibrary.Models;

namespace GameLibrary.Services
{
    /// <summary>
    /// Управляет статистикой игрового процесса:
    /// загружает, обновляет и сохраняет результаты игр.
    /// </summary>
    public class StatisticsService
    {
        /// <summary>
        /// Хранит имя файла для сохранения статистики.
        /// </summary>
        private const string FilePath = "statistics.json";

        /// <summary>
        /// Хранит статистику сыгранных игр.
        /// </summary>
        private readonly GameStatistics _statistics;

        /// <summary>
        /// Хранит текущую игру.
        /// </summary>
        private GuessingGame? _game;

        /// <summary>
        /// Хранит название выбранного уровня сложности.
        /// </summary>
        private string _difficultyName = string.Empty;

        /// <summary>
        /// Создаёт сервис для работы со статистикой.
        /// </summary>
        /// <param name="statistics">Объект статистики.</param>
        public StatisticsService(GameStatistics statistics)
        {
            ArgumentNullException.ThrowIfNull(statistics);
            _statistics = statistics;
        }

        public GameStatistics GameStatistics
        {
            get => default;
            set
            {
            }
        }

        public GuessingGame GuessingGame
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Загружает статистику из файла.
        /// При необходимости переносит старые рекорды
        /// в новую структуру хранения.
        /// </summary>
        public void Load()
        {
            if (!File.Exists(FilePath))
            {
                return;
            }

            string json = File.ReadAllText(FilePath);

            GameStatistics? loadedStatistics =
                JsonSerializer.Deserialize<GameStatistics>(json);

            if (loadedStatistics == null)
            {
                return;
            }

            _statistics.TotalGames = loadedStatistics.TotalGames;
            _statistics.Wins = loadedStatistics.Wins;
            _statistics.Losses = loadedStatistics.Losses;
            _statistics.CurrentWinStreak = loadedStatistics.CurrentWinStreak;
            _statistics.BestWinStreak = loadedStatistics.BestWinStreak;

            _statistics.BestResultsByDifficulty =
                loadedStatistics.BestResultsByDifficulty
                ?? new Dictionary<string, int>();

            // Перенос рекордов из старого формата файла.
            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;

            AddLegacyResult(root, "BestEasyResult", "Лёгкий");
            AddLegacyResult(root, "BestMediumResult", "Средний");
            AddLegacyResult(root, "BestHardResult", "Сложный");
        }

        /// <summary>
        /// Переносит рекорд из старого формата статистики.
        /// </summary>
        /// <param name="root">Корневой элемент JSON-документа.</param>
        /// <param name="propertyName">Имя старого свойства.</param>
        /// <param name="difficultyName">Название уровня сложности.</param>
        private void AddLegacyResult(
            JsonElement root,
            string propertyName,
            string difficultyName)
        {
            if (root.TryGetProperty(propertyName, out JsonElement result))
            {
                int attempts = result.GetInt32();

                if (attempts > 0 &&
                    !_statistics.BestResultsByDifficulty.ContainsKey(difficultyName))
                {
                    _statistics.BestResultsByDifficulty[difficultyName] = attempts;
                }
            }
        }

        /// <summary>
        /// Сохраняет статистику в файл.
        /// </summary>
        public void Save()
        {
            string json = JsonSerializer.Serialize(
                _statistics,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Подписывает сервис статистики на события игры.
        /// </summary>
        /// <param name="game">Текущая игра.</param>
        /// <param name="difficulty">Выбранный уровень сложности.</param>
        public void Subscribe(GuessingGame game, IDifficulty difficulty)
        {
            ArgumentNullException.ThrowIfNull(game);
            ArgumentNullException.ThrowIfNull(difficulty);

            _game = game;
            _difficultyName = difficulty.Name;

            game.GameWon += OnGameWon;
            game.GameLost += OnGameLost;
        }

        /// <summary>
        /// Обновляет статистику после победы игрока.
        /// </summary>
        private void OnGameWon()
        {
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

            int attempts = _game.TotalAttemptsUsed;

            if (!_statistics.BestResultsByDifficulty.TryGetValue(
                _difficultyName, out int bestResult) ||
                attempts < bestResult)
            {
                _statistics.BestResultsByDifficulty[_difficultyName] = attempts;
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
