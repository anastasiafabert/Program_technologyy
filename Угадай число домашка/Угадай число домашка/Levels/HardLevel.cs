namespace Угадай_число_домашка
{
    /// <summary>
    /// Сложный уровень: числа 1-1000, 10 попыток.
    /// </summary>
    public class HardLevel : IDifficultyLevel
    {
        public string Name => "Сложный";
        public int MinNumber => 1;
        public int MaxNumber => 1000;
        public int MaxAttempts => 10;
    }
}