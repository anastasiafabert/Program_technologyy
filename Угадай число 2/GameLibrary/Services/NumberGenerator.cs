
using GameLibrary.Interfaces;

namespace GameLibrary.Services

{
    /// <summary>
    /// Реализует генерацию случайных чисел.
    /// </summary>
    public class NumberGenerator : INumberGenerator
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Генерирует случайное число в заданном диапазоне.
        /// </summary>
        /// <param name="min">Минимальное значение.</param>
        /// <param name="max">Максимальное значение.</param>
        /// <returns>Сгенерированное случайное число.</returns>
        public int Generate(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
    }
} 