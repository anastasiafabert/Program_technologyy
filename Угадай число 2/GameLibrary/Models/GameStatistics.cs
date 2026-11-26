
using System.Collections.Generic;

namespace GameLibrary.Models
{
    /// <summary>
    /// Хранит общую статистику сыгранных игр,
    /// включая количество побед, поражений и лучшие результаты.
    /// </summary>
    public class GameStatistics 
    {
        /// <summary>
        /// Возвращает и задаёт общее количество сыгранных игр.
        /// </summary>
        public int TotalGames { get; set; }

        /// <summary>
        /// Возвращает и задаёт количество побед игрока.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Возвращает и задаёт количество поражений игрока.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Возвращает и задаёт текущую серию побед подряд.
        /// </summary>
        public int CurrentWinStreak { get; set; }

        /// <summary>
        /// Возвращает и задаёт лучшую серию побед подряд за всё время.
        /// </summary>
        public int BestWinStreak { get; set; }

        /// <summary>
        /// Хранит лучшие результаты по каждому уровню сложности.
        /// Ключом является название уровня сложности,
        /// а значением — минимальное количество попыток для победы.
        /// </summary>
        public Dictionary<string, int> BestResultsByDifficulty { get; set; }
            = new Dictionary<string, int>();
    }
}
