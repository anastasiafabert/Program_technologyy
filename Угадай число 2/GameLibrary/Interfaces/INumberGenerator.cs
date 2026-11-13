namespace GameLibrary.Interfaces
{
    /// <summary>
    /// Определяет способ генерации случайного числа.
    /// </summary>
    public interface INumberGenerator
    {
        /// <summary>
        /// Генерирует случайное число в заданном диапазоне.
        /// </summary>
        /// <param name="min">Минимальное значение.</param>
        /// <param name="max">Максимальное значение.</param>
        /// <returns>Сгенерированное случайное число.</returns>
        int Generate(int min, int max);
    }
}
