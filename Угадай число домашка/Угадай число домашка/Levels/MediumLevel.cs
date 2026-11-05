namespace Угадай_число_домашка
{
    /// <summary>
    /// Средний уровень: числа 1-100, 7 попыток.
    /// </summary>
    public class MediumLevel : IDifficultyLevel
    {
        public string Name => "Средний";
        public int MinNumber => 1;
        public int MaxNumber => 100;
        public int MaxAttempts => 7;
    }
}