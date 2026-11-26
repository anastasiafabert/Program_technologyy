
using GameLibrary.Interfaces;

namespace GameLibrary.Services
{
    /// <summary>
    /// Реализует генерацию случайных чисел.
    /// </summary>
    public class NumberGenerator : INumberGenerator
    {
        /// <summary>
        /// Объект для генерации случайных чисел.
        /// </summary>
        private readonly Random _random = new Random();

        /// <summary>
        /// Генерирует случайное число в заданном диапазоне.
        /// </summary>
        /// <param name="min">Минимальное значение диапазона.</param>
        /// <param name="max">Максимальное значение диапазона.</param>
        /// <returns>Случайное число от min до max включительно.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если минимальное значение больше максимального.
        /// </exception>
        public int Generate(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(min),
                    "Минимальное значение не может быть больше максимального.");
            }

            return (int)_random.NextInt64(min, (long)max + 1);
        }
    }
}
