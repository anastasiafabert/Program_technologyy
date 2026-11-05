namespace GuessGame
{
    /// <summary>
    /// Интерфейс для ввода/вывода (чтобы отделить логику от консоли).
    /// </summary>
    public interface IGameIO
    {
        void Write(string message);
        void WriteLine(string message);
        string ReadLine();
    }
}