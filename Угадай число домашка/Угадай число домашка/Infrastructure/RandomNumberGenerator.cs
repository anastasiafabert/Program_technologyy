using System;

namespace Угадай_число_домашка
{
    /// <summary>
    /// Стандартная реализация генератора.
    /// </summary>
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _random = new();
        public int Next(int min, int max) => _random.Next(min, max);
    }
}