
using GameLibrary.Enums;
using GameLibrary.Interfaces;
using GameLibrary.Models;
using GameLibrary.Services;
using System;
using System.Collections.Generic;
using Угадай_число_2.Models;
using Угадай_число_2.Services;
using Угадай_число_2.UI;

namespace Угадай_число_2
{
    /// <summary>
    /// Содержит точку входа в приложение.
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

        public ConsoleUserInterface ConsoleUserInterface
        {
            get => default;
            set
            {
            }
        }

        public MultiplayerGame MultiplayerGame
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

            List<IDifficulty> difficulties = new List<IDifficulty>
            {
                new EasyDifficulty(),
                new MediumDifficulty(),
                new HardDifficulty()
            };

            IDifficulty selectedDifficulty = difficulties[0];

            GameSettings settings =
                GameSettings.CreateForDifficulty(selectedDifficulty);

            GameStatistics statistics = new GameStatistics();

            StatisticsService statisticsService =
                new StatisticsService(statistics);

            statisticsService.Load();

            NumberGenerator numberGenerator = new NumberGenerator();

            GameEventHandler eventHandler = new GameEventHandler();

            GameMenu menu = new GameMenu();

            IUserInterface userInterface = new ConsoleUserInterface();

            MultiplayerGame multiplayerGame =
                new MultiplayerGame(userInterface);

            bool isRunning = true;

            while (isRunning)
            {
                menu.ShowMainMenu();

                string choice = menu.GetMenuChoice();

                if (!menu.IsValidChoice(choice, 5))
                {
                    menu.ShowInvalidChoice();
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        GuessingGame game =
                            new GuessingGame(numberGenerator, settings);

                        statisticsService.Subscribe(game, selectedDifficulty);
                        eventHandler.Subscribe(game);

                        Console.WriteLine();
                        Console.WriteLine("----- ИГРА НАЧАЛАСЬ! -----");
                        Console.WriteLine(
                            $"Угадайте число от {settings.MinNumber} до {settings.MaxNumber}.");
                        Console.WriteLine(
                            $"У вас есть {settings.MaxAttempts} попыток.");

                        while (!game.IsWon && game.AttemptsRemaining > 0)
                        {
                            Console.WriteLine(
                                $"Осталось попыток: {game.AttemptsRemaining}");
                            Console.Write("Введите ваше число: ");

                            string input = Console.ReadLine() ?? string.Empty;

                            if (!int.TryParse(input, out int guess))
                            {
                                Console.WriteLine(
                                    "Ошибка! Необходимо ввести целое число.");
                                continue;
                            }

                            if (guess < settings.MinNumber ||
                                guess > settings.MaxNumber)
                            {
                                Console.WriteLine(
                                    "Число выходит за пределы игрового диапазона.");
                                continue;
                            }

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
                        }

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

                                if (!int.TryParse(extraInput, out int extraGuess))
                                {
                                    Console.WriteLine(
                                        "Ошибка! Необходимо ввести целое число.");
                                    continue;
                                }

                                if (extraGuess < settings.MinNumber ||
                                    extraGuess > settings.MaxNumber)
                                {
                                    Console.WriteLine(
                                        "Число выходит за пределы игрового диапазона.");
                                    continue;
                                }

                                game.UseExtraChance(extraGuess);
                            }
                        }

                        break;

                    case "2":
                        string firstName = userInterface.ReadInput(
                            "Введите имя первого игрока: ");

                        string secondName = userInterface.ReadInput(
                            "Введите имя второго игрока: ");

                        if (string.IsNullOrWhiteSpace(firstName) ||
                            string.IsNullOrWhiteSpace(secondName))
                        {
                            userInterface.ShowMessage(
                                "Имена игроков не могут быть пустыми.");
                            break;
                        }

                        Player firstPlayer = new Player(firstName);
                        Player secondPlayer = new Player(secondName);

                        GuessingGame multiplayerRound =
                            new GuessingGame(numberGenerator, settings);

                        statisticsService.Subscribe(
                            multiplayerRound, selectedDifficulty);

                        eventHandler.Subscribe(multiplayerRound);

                        multiplayerGame.Play(
                            firstPlayer,
                            secondPlayer,
                            multiplayerRound);

                        break;

                    case "3":
                        menu.ShowStatistics(statistics);
                        break;

                    case "4":
                        menu.ShowDifficultyMenu(difficulties);

                        string difficultyChoice =
                            menu.GetDifficultyChoice();

                        if (!menu.IsValidChoice(
                            difficultyChoice, difficulties.Count))
                        {
                            menu.ShowInvalidChoice();
                        }
                        else
                        {
                            int difficultyIndex =
                                int.Parse(difficultyChoice) - 1;

                            selectedDifficulty =
                                difficulties[difficultyIndex];

                            settings =
                                GameSettings.CreateForDifficulty(
                                    selectedDifficulty);

                            Console.WriteLine(
                                $"Выбрана сложность: {selectedDifficulty.Name}.");
                        }

                        break;

                    case "5":
                        Console.WriteLine("Выход из игры...");
                        isRunning = false;
                        break;
                }
            }
        }
    }
}
