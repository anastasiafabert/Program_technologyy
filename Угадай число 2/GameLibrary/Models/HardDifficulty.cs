using GameLibrary.Interfaces;

namespace GameLibrary.Models
{
    /// <summary>
    /// Представляет сложный уровень сложности игры.
    /// Игрок должен угадать число в диапазоне от 0 до 200
    /// за 5 попыток.
    /// </summary>
    public class HardDifficulty : IDifficulty
    {
        /// <summary>
        /// Возвращает название уровня сложности.
        /// </summary>
        public string Name => "Сложный";

        /// <summary>
        /// Возвращает минимальное число диапазона.
        /// </summary>
        public int MinNumber => 0;

        /// <summary>
        /// Возвращает максимальное число диапазона.
        /// </summary>
        public int MaxNumber => 200;

        /// <summary>
        /// Возвращает максимальное количество попыток.
        /// </summary>
        public int MaxAttempts => 5;
    }
}