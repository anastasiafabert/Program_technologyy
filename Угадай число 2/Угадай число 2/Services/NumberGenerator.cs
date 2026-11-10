using System;
using Угадай_число_2.Interfaces;

namespace Угадай_число_2.Services
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