namespace GameLibrary.Enums
{
    /// <summary>
    /// Определяет результат проверки предположения игрока.
    /// </summary>
    public enum GuessResult
    {
        /// <summary>
        /// Игрок угадал число.
        /// </summary>
        Correct,

        /// <summary>
        /// Предположение игрока меньше загаданного числа.
        /// </summary>
        TooLow,

        /// <summary>
        /// Предположение игрока больше загаданного числа.
        /// </summary>
        TooHigh
    }
}