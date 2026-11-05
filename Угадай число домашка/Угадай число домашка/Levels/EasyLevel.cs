namespace Угадай_число_домашка
{
    /// <summary>
    /// Лёгкий уровень: числа 1-10, 5 попыток.
    /// </summary>
    public class EasyLevel : IDifficultyLevel
    {
        public string Name => "Лёгкий";
        public int MinNumber => 1;
        public int MaxNumber => 10;
        public int MaxAttempts => 5;
    }
}