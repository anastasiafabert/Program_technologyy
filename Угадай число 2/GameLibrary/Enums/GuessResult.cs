
namespace GameLibrary.Enums
{
    /// <summary>
    /// Определяет результат проверки числа, введённого игроком.
    /// </summary>
    public enum GuessResult
    {
        /// <summary>
        /// Игрок угадал загаданное число.
        /// </summary>
        Correct,

        /// <summary>
        /// Введённое число меньше загаданного.
        /// </summary>
        TooLow,

        /// <summary>
        /// Введённое число больше загаданного.
        /// </summary>
        TooHigh
    }
}
