namespace Угадай_число_домашка
{
    /// <summary>
    /// Модель статистики игры.
    /// </summary>
    public class GameStatistics
    {
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int TotalAttempts { get; set; }
        public int BestScore { get; set; } = int.MaxValue;
    }
}