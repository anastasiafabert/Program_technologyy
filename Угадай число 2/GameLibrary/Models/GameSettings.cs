using GameLibrary.Enums;

namespace GameLibrary.Models
{
    /// <summary>
    /// Хранит настройки текущей игры.
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// Минимальное число для угадывания.
        /// </summary>
        public int MinNumber { get; set; }

        /// <summary>
        /// Максимальное число для угадывания.
        /// </summary>
        public int MaxNumber { get; set; }

        /// <summary>
        /// Максимальное количество попыток игрока.
        /// </summary>
        public int MaxAttempts { get; set; }

        /// <summary>
        /// Создаёт настройки игры в зависимости от выбранного уровня сложности.
        /// </summary>
        /// <param name="difficulty">Выбранный уровень сложности.</param>
        /// <returns>Объект с настройками игры.</returns>
        public static GameSettings CreateForDifficulty(DifficultyLevel difficulty)
        {
            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    return new GameSettings
                    {
                        MinNumber = 0,
                        MaxNumber = 50,
                        MaxAttempts = 10,
                    };

                case DifficultyLevel.Medium:
                    return new GameSettings
                    {
                        MinNumber = 0,
                        MaxNumber = 100,
                        MaxAttempts = 7,                       
                    };

                case DifficultyLevel.Hard:
                    return new GameSettings
                    {
                        MinNumber = 0,
                        MaxNumber = 200,
                        MaxAttempts = 5,                     
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty));
            }
        }
    }
}
