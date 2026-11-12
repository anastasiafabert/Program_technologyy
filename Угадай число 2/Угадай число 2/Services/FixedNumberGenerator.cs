
using Угадай_число_2.Interfaces;

namespace Угадай_число_2.Services
{
    /// <summary>
    /// Генерирует заранее заданное число для проверки игровой логики.
    /// </summary>
    public class FixedNumberGenerator : INumberGenerator
    {
        /// <summary>
        /// Число, которое будет возвращать генератор.
        /// </summary>
        private readonly int _number;//хранит заранее заданное число

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
        public int Generate(int min, int max)
        {
            return _number;
        }
    }
}
