using GameLibrary.Interfaces;

namespace GameLibrary.Models
{
    /// <summary>
    /// Представляет лёгкий уровень сложности игры.
    /// Игрок должен угадать число в диапазоне от 0 до 50
    /// за 10 попыток.
    /// </summary>
    public class EasyDifficulty : IDifficulty
    {
        /// <summary>
        /// Возвращает название уровня сложности.
        /// </summary>
        public string Name => "Лёгкий";

        /// <summary>
        /// Возвращает минимальное число диапазона.
        /// </summary>
        public int MinNumber => 0;

        /// <summary>
        /// Возвращает максимальное число диапазона.
        /// </summary>
        public int MaxNumber => 50;

        /// <summary>
        /// Возвращает максимальное количество попыток.
        /// </summary>
        public int MaxAttempts => 10;
    }
}