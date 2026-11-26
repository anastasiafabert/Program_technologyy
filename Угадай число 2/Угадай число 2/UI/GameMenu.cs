
using GameLibrary.Interfaces;
using GameLibrary.Models;

namespace Угадай_число_2.UI
{
    /// <summary>
    /// Отвечает за отображение меню игры и игровой статистики.
    /// </summary>
    public class GameMenu
    {
        public GameStatistics GameStatistics
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Отображает главное меню игры.
        /// </summary>
        public void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine(" ИГРА УГАДАЙ ЧИСЛО ");
            Console.WriteLine("1. Начать игру");
            Console.WriteLine("2. Мультиплеер");
            Console.WriteLine("3. Посмотреть статистику");
            Console.WriteLine("4. Изменить сложность");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите действие: ");
        }

        /// <summary>
        /// Получает выбранный пользователем пункт меню.
        /// </summary>
        /// <returns>Текст, введённый пользователем.</returns>
        public string GetMenuChoice()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Проверяет правильность выбора пользователя.
        /// </summary>
        /// <param name="choice">Введённый пользователем текст.</param>
        /// <param name="maxChoice">Максимальный допустимый номер пункта.</param>
        /// <returns>
        /// true, если введённое значение является допустимым номером;
        /// иначе false.
        /// </returns>
        public bool IsValidChoice(string choice, int maxChoice)
        {
            if (!int.TryParse(choice, out int number))
            {
                return false;
            }

            return number >= 1 && number <= maxChoice;
        }

        /// <summary>
        /// Выводит сообщение о неправильном выборе пользователя.
        /// </summary>
        public void ShowInvalidChoice()
        {
            Console.WriteLine("Некорректный выбор! Попробуйте ещё раз.");
        }

        /// <summary>
        /// Отображает доступные уровни сложности.
        /// </summary>
        /// <param name="difficulties">Список доступных уровней сложности.</param>
        public void ShowDifficultyMenu(IReadOnlyList<IDifficulty> difficulties)
        {
            Console.WriteLine();
            Console.WriteLine(" ВЫБОР СЛОЖНОСТИ ");

            for (int i = 0; i < difficulties.Count; i++)
            {
                IDifficulty difficulty = difficulties[i];

                Console.WriteLine(
                    $"{i + 1}. {difficulty.Name} " +
                    $"({difficulty.MinNumber}–{difficulty.MaxNumber}, " +
                    $"{difficulty.MaxAttempts} попыток)");
            }

            Console.Write("Выберите уровень сложности: ");
        }

        /// <summary>
        /// Получает выбранный пользователем уровень сложности.
        /// </summary>
        /// <returns>Введённый пользователем номер уровня.</returns>
        public string GetDifficultyChoice()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Отображает общую статистику сыгранных игр
        /// и лучшие результаты по уровням сложности.
        /// </summary>
        /// <param name="statistics">Объект игровой статистики.</param>
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

            if (statistics.BestResultsByDifficulty.Count == 0)
            {
                Console.WriteLine("Рекордов пока нет.");
            }
            else
            {
                foreach (var result in statistics.BestResultsByDifficulty)
                {
                    Console.WriteLine($"{result.Key}: {result.Value} попыток");
                }
            }

            Console.WriteLine("--------------------------");
        }
    }
}
