namespace Угадай_число_домашка
{
    /// <summary>
    /// Интерфейс уровня сложности.
    /// </summary>
    public interface IDifficultyLevel
    {
        string Name { get; }
        int MinNumber { get; }
        int MaxNumber { get; }
        int MaxAttempts { get; }
    }
}