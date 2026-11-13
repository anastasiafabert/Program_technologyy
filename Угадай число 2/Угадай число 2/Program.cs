using Угадай_число_2.UI;
using Угадай_число_2.Services;

using GameLibrary.Enums;
using GameLibrary.Models;
using GameLibrary.Services;

namespace Угадай_число_2
{
    /// <summary>
    /// Содержит точку входа в приложение и запускает игру «Угадай число».
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Запускает приложение 
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Храним выбранный уровень сложности.
            DifficultyLevel selectedDifficulty = DifficultyLevel.Easy;

            // Создаём настройки игры для лёгкого уровня.
            GameSettings settings = GameSettings.CreateForDifficulty(DifficultyLevel.Easy);

            // Создаём объект статистики.
            GameStatistics statistics = new GameStatistics();

            // Создаём сервис статистики.
            StatisticsService statisticsService = new StatisticsService(statistics);

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
                        GuessingGame game = new GuessingGame(numberGenerator, settings);

                        // Подписываем сервис статистики на события игры.
                        statisticsService.Subscribe(game, selectedDifficulty);

                        // Подписываем обработчик игровых событий.
                        eventHandler.Subscribe(game);

                        Console.WriteLine();
                        Console.WriteLine("----- ИГРА НАЧАЛАСЬ! -----");
                        Console.WriteLine($"Угадайте число от {settings.MinNumber} до {settings.MaxNumber}.");
                        Console.WriteLine($"У вас есть {settings.MaxAttempts} попыток.");

                        // Обычные попытки.
                        while (!game.IsWon && game.AttemptsRemaining > 0)
                        {
                            Console.WriteLine($"Осталось попыток: {game.AttemptsRemaining}");
                            Console.Write("Введите ваше число: ");

                            string input = Console.ReadLine() ?? string.Empty;

                            if (int.TryParse(input, out int guess))
                            {
                                if (guess < settings.MinNumber || guess > settings.MaxNumber)
                                {
                                    Console.WriteLine("Число выходит за пределы игрового диапазона.");
                                }
                                else
                                {
                                    GuessResult result = game.CheckGuess(guess);

                                    if (result != GuessResult.Correct)
                                    {
                                        TemperatureHint hint = game.GetTemperatureHint(guess);

                                        switch (hint)
                                        {
                                            case TemperatureHint.Cold:
                                                Console.WriteLine("Холодно! Вы далеко от загаданного числа.");
                                                break;

                                            case TemperatureHint.Warm:
                                                Console.WriteLine("Тепло! Вы приближаетесь к загаданному числу.");
                                                break;

                                            case TemperatureHint.Hot:
                                                Console.WriteLine("Горячо! Вы совсем близко!");
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine($"🔥 Ваша серия побед: {statistics.CurrentWinStreak}");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка! Необходимо ввести целое число.");
                            }
                        }

                        // Последний шанс после окончания обычных попыток.
                        if (!game.IsWon && game.CanUseExtraChance)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Обычные попытки закончились!");
                            Console.WriteLine("У вас остался последний шанс!");

                            while (game.CanUseExtraChance)
                            {
                                Console.Write("Введите ваше число: ");

                                string extraInput = Console.ReadLine() ?? string.Empty;

                                if (int.TryParse(extraInput, out int extraGuess))
                                {
                                    if (extraGuess < settings.MinNumber || extraGuess > settings.MaxNumber)
                                    {
                                        Console.WriteLine("Число выходит за пределы игрового диапазона.");
                                    }
                                    else
                                    {
                                        game.UseExtraChance(extraGuess);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка! Необходимо ввести целое число.");
                                }
                            }
                        }

                        break;


                    case "2":
                        menu.ShowStatistics(statistics);
                        break;

                    case "3":
                        menu.ShowDifficultyMenu();

                        string difficultyChoice = menu.GetDifficultyChoice();

                        if (!menu.IsValidChoice(difficultyChoice, 3))
                        {
                            menu.ShowInvalidChoice();
                        }
                        else
                        {
                            switch (difficultyChoice)
                            {
                                case "1":

                                    selectedDifficulty = DifficultyLevel.Easy;
                                    settings = GameSettings.CreateForDifficulty(selectedDifficulty);
                                    //присваиваем новые настройки уже существующей переменной
                                   
                                    Console.WriteLine("Выбрана лёгкая сложность.");
                                    break;

                                case "2":
                                    selectedDifficulty = DifficultyLevel.Medium;
                                    settings = GameSettings.CreateForDifficulty(selectedDifficulty);
                                    
                                    Console.WriteLine("Выбрана средняя сложность.");
                                    break;

                                case "3":
                                    selectedDifficulty = DifficultyLevel.Hard;
                                    settings = GameSettings.CreateForDifficulty(selectedDifficulty);
                                    
                                    Console.WriteLine("Выбрана сложная сложность.");
                                    break;
                            }
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
