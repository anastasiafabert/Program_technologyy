using System;

namespace Угадай_число_домашка
{
    /// <summary>
    /// Реализация ввода/вывода через консоль.
    /// </summary>
    public class ConsoleGameIO : IGameIO
    {
        public void Write(string message) => Console.Write(message);

        public void WriteLine(string message) => Console.WriteLine(message);

        public string ReadLine() => Console.ReadLine() ?? "";
    }
}