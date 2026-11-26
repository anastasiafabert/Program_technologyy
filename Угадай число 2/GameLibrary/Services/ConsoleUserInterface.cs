
using GameLibrary.Interfaces;

namespace Угадай_число_2.Services
{
    /// <summary>
    /// Реализует взаимодействие с пользователем через консоль.
    /// </summary>
    public class ConsoleUserInterface : IUserInterface
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
