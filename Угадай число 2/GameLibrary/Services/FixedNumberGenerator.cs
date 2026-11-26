
using GameLibrary.Interfaces;

namespace GameLibrary.Services
{
    /// <summary>
    /// Генерирует заранее заданное число для проверки игровой логики.
    /// </summary>
    public class FixedNumberGenerator : INumberGenerator
    {
        /// <summary>
        /// Число, которое будет возвращать генератор.
        /// </summary>
        private readonly int _number;

        /// <summary>
        /// Создаёт генератор с заданным числом.
        /// </summary>
        /// <param name="number">Число для тестирования.</param>
        public FixedNumberGenerator(int number)
        {
            _number = number;
        }

        /// <summary>
        /// Возвращает заранее заданное число.
        /// </summary>
        /// <param name="min">Минимальное значение диапазона.</param>
        /// <param name="max">Максимальное значение диапазона.</param>
        /// <returns>Заданное число.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Выбрасывается, если диапазон некорректен
        /// или заданное число не входит в него.
        /// </exception>
        public int Generate(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(min),
                    "Минимальное значение не может быть больше максимального.");
            }

            if (_number < min || _number > max)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(_number),
                    "Заданное число не входит в допустимый диапазон.");
            }

            return _number;
        }
    }
}
