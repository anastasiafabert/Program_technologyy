using GameLibrary.Interfaces;

namespace GameLibrary.Models
{
    /// <summary>
    /// Представляет средний уровень сложности игры.
    /// Игрок должен угадать число в диапазоне от 0 до 100
    /// за 7 попыток.
    /// </summary>
    public class MediumDifficulty : IDifficulty
    {
        /// <summary>
        /// Возвращает название уровня сложности.
        /// </summary>
        public string Name => "Средний";

        /// <summary>
        /// Возвращает минимальное число диапазона.
        /// </summary>
        public int MinNumber => 0;

        /// <summary>
        /// Возвращает максимальное число диапазона.
        /// </summary>
        public int MaxNumber => 100;

        /// <summary>
        /// Возвращает максимальное количество попыток.
        /// </summary>
        public int MaxAttempts => 7;
    }
}