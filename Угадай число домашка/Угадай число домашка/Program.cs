using System;

namespace Угадай_число_домашка
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Создаем зависимости (Dependency Injection)
            IGameIO io = new ConsoleGameIO();
            IRandomNumberGenerator rng = new RandomNumberGenerator();
            IStatisticsRepository repo = new FileStatisticsRepository();

            // 2. Бесконечный цикл меню
            while (true)
            {
                io.WriteLine("\n Игра Угадай число ");
                io.WriteLine("1. Лёгкий уровень (1-10)");
                io.WriteLine("2. Средний уровень (1-100)");
                io.WriteLine("3. Сложный уровень (1-1000)");
                io.WriteLine("4. Посмотреть статистику");
                io.WriteLine("0. Выход");
                io.Write("Выбор: ");

                var choice = io.ReadLine();

                if (choice == "0") break;

                // 3. Просмотр статистики
                if (choice == "4")
                {
                    var stats = repo.Load();
                    io.WriteLine("\n Статистика ");
                    io.WriteLine($"Сыграно игр: {stats.GamesPlayed}");
                    io.WriteLine($"Побед: {stats.GamesWon}");
                    io.WriteLine($"Всего попыток: {stats.TotalAttempts}");
                    // Если BestScore равен максимальному числу, значит побед еще не было
                    io.WriteLine($"Лучший результат: {(stats.BestScore == int.MaxValue ? 0 : stats.BestScore)}");
                    continue;
                }

                // 4. Выбор уровня сложности
                IDifficultyLevel? level = choice switch
                {
                    "1" => new EasyLevel(),
                    "2" => new MediumLevel(),
                    "3" => new HardLevel(),
                    _ => null // Если ввели что-то другое
                };

                if (level == null)
                {
                    io.WriteLine("Неверный выбор. Попробуйте снова.");
                    continue;
                }

                // 5. Создаем игру и запускаем
                var game = new GuessGame(io, rng, level);

                // Play() возвращает количество попыток (или -1, если проиграл)
                int attempts = game.Play();

                // 6. Обновляем статистику (внедряем логику)
                var currentStats = repo.Load();
                currentStats.GamesPlayed++;

                if (attempts > 0)
                {
                    currentStats.GamesWon++;
                    currentStats.TotalAttempts += attempts;
                    if (attempts < currentStats.BestScore)
                        currentStats.BestScore = attempts;
                }

                // 7. Сохраняем в файл
                repo.Save(currentStats);

                io.WriteLine("\nНажмите любую клавишу для продолжения.");
                Console.ReadKey();
            }
        }
    }
}