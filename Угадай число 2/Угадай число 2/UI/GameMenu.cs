using GameLibrary.Enums;
using GameLibrary.Models;

namespace Угадай_число_2.UI

{
    /// <summary>
    /// Отвечает за отображение главного меню игры.
    /// </summary>
    public class GameMenu
    {
        /// <summary>
        /// Отображает главное меню игры.
        /// </summary>
        public void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine(" ИГРА УГАДАЙ ЧИСЛО ");
            Console.WriteLine("1. Начать игру");
            Console.WriteLine("2. Посмотреть статистику");
            Console.WriteLine("3. Изменить сложность");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите действие: ");
        }

        /// <summary>
        /// Получает выбранный пользователем пункт меню.
        /// </summary>
        /// <returns>Номер выбранного пункта.</returns>
        public string GetMenuChoice()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Проверяет правильность выбора пользователя.
        /// </summary>
        public bool IsValidChoice(string choice, int maxChoice)
        {
            if (!int.TryParse(choice, out int number))
            {
                return false;
            }

            return number >= 1 && number <= maxChoice;
        }

        /// <summary>
        /// Сообщает пользователю о неправильном выборе.
        /// </summary>
        public void ShowInvalidChoice()
        {
            Console.WriteLine("Некорректный выбор! Попробуйте ещё раз.");
        }

        /// <summary>
        /// Отображает меню выбора уровня сложности.
        /// </summary>
        public void ShowDifficultyMenu()
        {
            Console.WriteLine();
            Console.WriteLine(" ВЫБОР СЛОЖНОСТИ ");
            Console.WriteLine("1. Лёгкий (0–50, 10 попыток)");
            Console.WriteLine("2. Средний (0–100, 7 попыток)");
            Console.WriteLine("3. Сложный (0–200, 5 попыток)");
            Console.Write("Выберите уровень сложности: ");
        }

        /// <summary>
        /// Получает выбранный пользователем уровень сложности.
        /// </summary>
        /// <returns>Номер выбранного уровня.</returns>
        public string GetDifficultyChoice()
        {
            //если ввод вернул null, вместо него используем пустую строку.
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Отображает общую статистику сыгранных игр.
        /// </summary>
        public void ShowStatistics(GameStatistics statistics)
        {
            Console.WriteLine();
            Console.WriteLine("---- СТАТИСТИКА ИГРЫ ----");

            Console.WriteLine($"Всего игр: {statistics.TotalGames}");
            Console.WriteLine($"Побед: {statistics.Wins}");
            Console.WriteLine($"Поражений: {statistics.Losses}");

            Console.WriteLine($"Текущая серия побед: {statistics.CurrentWinStreak} 🔥");
            Console.WriteLine($"Лучшая серия побед: {statistics.BestWinStreak} 🔥");

            Console.WriteLine();
            Console.WriteLine("Лучшие результаты по сложности:");

            Console.WriteLine($"Лёгкий уровень: {statistics.BestEasyResult}");
            Console.WriteLine($"Средний уровень: {statistics.BestMediumResult}");
            Console.WriteLine($"Сложный уровень: {statistics.BestHardResult}");

            Console.WriteLine("--------------------------");
        }


    }
}