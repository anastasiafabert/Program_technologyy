namespace Угадай_число_домашка
{
    /// <summary>
    /// Генератор случайных чисел.
    /// </summary>
    public interface IRandomNumberGenerator
    {
        int Next(int min, int max);
    }
}