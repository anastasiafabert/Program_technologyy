
using GameLibrary.Interfaces;

namespace GameLibrary.Models
{
    /// <summary>
    /// Хранит настройки игры:
    /// диапазон загадываемых чисел и максимальное количество попыток.
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// Возвращает минимальное число игрового диапазона.
        /// </summary>
        public int MinNumber { get; }

        /// <summary>
        /// Возвращает максимальное число игрового диапазона.
        /// </summary>
        public int MaxNumber { get; }

        /// <summary>
        /// Возвращает максимальное количество попыток игрока.
        /// </summary>
        public int MaxAttempts { get; }

        /// <summary>
        /// Создаёт настройки игры с заданными параметрами.
        /// </summary>
        /// <param name="minNumber">Минимальное число игрового диапазона.</param>
        /// <param name="maxNumber">Максимальное число игрового диапазона.</param>
        /// <param name="maxAttempts">Максимальное количество попыток.</param>
        private GameSettings(int minNumber, int maxNumber, int maxAttempts)
        {
            if (minNumber > maxNumber)
            {
                throw new ArgumentException(
                    "Минимальное число не может быть больше максимального.");
            }

            if (maxAttempts <= 0)
            {
                throw new ArgumentException(
                    "Количество попыток должно быть больше нуля.");
            }

            MinNumber = minNumber;
            MaxNumber = maxNumber;
            MaxAttempts = maxAttempts;
        }

        /// <summary>
        /// Создаёт настройки игры на основе выбранного уровня сложности.
        /// </summary>
        /// <param name="difficulty">Выбранный уровень сложности.</param>
        /// <returns>Настройки игры для выбранного уровня сложности.</returns>
        public static GameSettings CreateForDifficulty(IDifficulty difficulty)
        {
            ArgumentNullException.ThrowIfNull(difficulty);

            return new GameSettings(
                difficulty.MinNumber,
                difficulty.MaxNumber,
                difficulty.MaxAttempts);
        }
    }
}
