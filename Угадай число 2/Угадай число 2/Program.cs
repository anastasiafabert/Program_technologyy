
using System.Collections.Generic;

using Угадай_число_2.UI;
using Угадай_число_2.Services;

using GameLibrary.Enums;
using GameLibrary.Interfaces;
using GameLibrary.Models;
using GameLibrary.Services;

namespace Угадай_число_2
{
    /// <summary>
    /// Содержит точку входа в приложение
    /// и запускает игру «Угадай число».
    /// </summary>
    internal class Program
    {
        public GameMenu GameMenu
        {
            get => default;
            set
            {
            }
        }

        public GameEventHandler GameEventHandler
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Запускает приложение.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Создаём список доступных уровней сложности.
            List<IDifficulty> difficulties = new List<IDifficulty>
            {
                new EasyDifficulty(),
                new MediumDifficulty(),
                new HardDifficulty()
            };

            // Выбираем первый уровень сложности по умолчанию.
            IDifficulty selectedDifficulty = difficulties[0];

            // Создаём настройки игры на основе выбранной сложности.
            GameSettings settings =
                GameSettings.CreateForDifficulty(selectedDifficulty);

            // Создаём объект статистики.
            GameStatistics statistics = new GameStatistics();

            // Создаём сервис статистики.
            StatisticsService statisticsService =
                new StatisticsService(statistics);

            // Загружаем сохранённую статистику.
            statisticsService.Load();

            // Создаём генератор случайных чисел.
            NumberGenerator numberGenerator = new NumberGenerator();

            // Создаём обработчик игровых событий.
            GameEventHandler eventHandler = new GameEventHandler();

            // Создаём объект главного меню.
            GameMenu menu = new GameMenu();

            // Показывает, продолжает ли работать приложение.
            bool isRunning = true;

            // Повторяем меню, пока пользователь не выберет выход.
            while (isRunning)
            {
                menu.ShowMainMenu();

                string choice = menu.GetMenuChoice();

                if (!menu.IsValidChoice(choice, 4))
                {
                    menu.ShowInvalidChoice();
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        // Создаём новый игровой раунд с выбранной сложностью.
                        GuessingGame game =
                            new GuessingGame(numberGenerator, settings);

                        // Подписываем сервис статистики на события игры.
                        statisticsService.Subscribe(game, selectedDifficulty);

                        // Подписываем обработчик игровых событий.
                        eventHandler.Subscribe(game);

                        Console.WriteLine();
                        Console.WriteLine("----- ИГРА НАЧАЛАСЬ! -----");
                        Console.WriteLine(
                            $"Угадайте число от {settings.MinNumber} до {settings.MaxNumber}.");
                        Console.WriteLine(
                            $"У вас есть {settings.MaxAttempts} попыток.");

                        // Выполняем обычные попытки.
                        while (!game.IsWon && game.AttemptsRemaining > 0)
                        {
                            Console.WriteLine(
                                $"Осталось попыток: {game.AttemptsRemaining}");
                            Console.Write("Введите ваше число: ");

                            string input = Console.ReadLine() ?? string.Empty;

                            if (int.TryParse(input, out int guess))
                            {
                                if (guess < settings.MinNumber ||
                                    guess > settings.MaxNumber)
                                {
                                    Console.WriteLine(
                                        "Число выходит за пределы игрового диапазона.");
                                }
                                else
                                {
                                    GuessResult result = game.CheckGuess(guess);

                                    if (result != GuessResult.Correct)
                                    {
                                        TemperatureHint hint =
                                            game.GetTemperatureHint(guess);

                                        switch (hint)
                                        {
                                            case TemperatureHint.Cold:
                                                Console.WriteLine(
                                                    "Холодно! Вы далеко от загаданного числа.");
                                                break;

                                            case TemperatureHint.Warm:
                                                Console.WriteLine(
                                                    "Тепло! Вы приближаетесь к загаданному числу.");
                                                break;

                                            case TemperatureHint.Hot:
                                                Console.WriteLine(
                                                    "Горячо! Вы совсем близко!");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(
                                            $"🔥 Ваша серия побед: {statistics.CurrentWinStreak}");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(
                                    "Ошибка! Необходимо ввести целое число.");
                            }
                        }

                        // Предоставляем последний шанс после обычных попыток.
                        if (!game.IsWon && game.CanUseExtraChance)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Обычные попытки закончились!");
                            Console.WriteLine("У вас остался последний шанс!");

                            while (game.CanUseExtraChance)
                            {
                                Console.Write("Введите ваше число: ");

                                string extraInput =
                                    Console.ReadLine() ?? string.Empty;

                                if (int.TryParse(extraInput, out int extraGuess))
                                {
                                    if (extraGuess < settings.MinNumber ||
                                        extraGuess > settings.MaxNumber)
                                    {
                                        Console.WriteLine(
                                            "Число выходит за пределы игрового диапазона.");
                                    }
                                    else
                                    {
                                        game.UseExtraChance(extraGuess);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(
                                        "Ошибка! Необходимо ввести целое число.");
                                }
                            }
                        }

                        break;

                    case "2":
                        // Показываем статистику.
                        menu.ShowStatistics(statistics);
                        break;

                    case "3":
                        // Показываем все доступные уровни сложности.
                        menu.ShowDifficultyMenu(difficulties);

                        string difficultyChoice = menu.GetDifficultyChoice();

                        if (!menu.IsValidChoice(
                            difficultyChoice, difficulties.Count))
                        {
                            menu.ShowInvalidChoice();
                        }
                        else
                        {
                            // Получаем выбранный уровень из списка.
                            int difficultyIndex =
                                int.Parse(difficultyChoice) - 1;

                            selectedDifficulty = difficulties[difficultyIndex];

                            // Создаём настройки для выбранного уровня.
                            settings =
                                GameSettings.CreateForDifficulty(selectedDifficulty);

                            Console.WriteLine(
                                $"Выбрана сложность: {selectedDifficulty.Name}.");
                        }

                        break;

                    case "4":
                        Console.WriteLine("Выход из игры...");
                        isRunning = false;
                        break;
                }
            }
        }
    }
}
