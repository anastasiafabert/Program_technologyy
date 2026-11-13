namespace GameLibrary.Models
{
    /// <summary>
    /// Хранит общую статистику сыгранных игр.
    /// </summary>
    public class GameStatistics
    {
        /// <summary>
        /// Общее количество сыгранных игр.
        /// </summary>
        public int TotalGames { get; set; }

        /// <summary>
        /// Количество побед игрока.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Количество поражений игрока.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Текущая серия побед подряд.
        /// </summary>
        public int CurrentWinStreak { get; set; }

        /// <summary>
        /// Лучшая серия побед подряд.
        /// </summary>
        public int BestWinStreak { get; set; }


        /// <summary>
        /// Лучший результат на лёгком уровне.
        /// </summary>
        public int BestEasyResult { get; set; }

        /// <summary>
        /// Лучший результат на среднем уровне.
        /// </summary>
        public int BestMediumResult { get; set; }

        /// <summary>
        /// Лучший результат на сложном уровне.
        /// </summary>
        public int BestHardResult { get; set; }
    }


}