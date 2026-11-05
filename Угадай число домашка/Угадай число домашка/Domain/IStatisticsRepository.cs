namespace Угадай_число_домашка
{
    /// <summary>
    /// Интерфейс для сохранения/загрузки статистики.
    /// </summary>
    public interface IStatisticsRepository
    {
        GameStatistics Load();
        void Save(GameStatistics stats);
    }
}