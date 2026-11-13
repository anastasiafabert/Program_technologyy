namespace GameLibrary.Enums
{
    /// <summary>
    /// Определяет степень близости предположения игрока к загаданному числу.
    /// </summary>
    public enum TemperatureHint
    {
        /// <summary>
        /// Игрок далеко от загаданного числа.
        /// </summary>
        Cold,

        /// <summary>
        /// Игрок находится на среднем расстоянии от загаданного числа.
        /// </summary>
        Warm,

        /// <summary>
        /// Игрок находится очень близко к загаданному числу.
        /// </summary>
        Hot
    }
}