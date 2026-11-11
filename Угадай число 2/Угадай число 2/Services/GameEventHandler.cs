using System;
using Угадай_число_2.Enums;

namespace Угадай_число_2.Services
{
    /// <summary>
    /// Обрабатывает события игрового процесса.
    /// </summary>
    public class GameEventHandler
    {
        /// <summary>
        /// Обрабатывает результат проверки предположения игрока.
        /// </summary>
        public void OnGuessChecked(GuessResult result)
        {
            switch (result)
            {
                case GuessResult.Correct:
                    Console.WriteLine("Вы угадали число!");
                    break;

                case GuessResult.TooLow:
                    Console.WriteLine("Загаданное число больше.");
                    break;

                case GuessResult.TooHigh:
                    Console.WriteLine("Загаданное число меньше.");
                    break;
            }
        }

        /// <summary>
        /// Обрабатывает победу игрока.
        /// </summary>
        public void OnGameWon()
        {
            Console.WriteLine("Поздравляем! Вы победили!");
        }

        /// <summary>
        /// Обрабатывает поражение игрока.
        /// </summary>
        public void OnGameLost()
        {
            Console.WriteLine("Вы проиграли! Попробуйте ещё раз.");
        }

        /// <summary>
        /// Подписывает обработчик на события игрового процесса.
        /// </summary>
        /// <param name="game">Текущий игровой раунд.</param>
        public void Subscribe(GuessingGame game)
        {
            game.GuessChecked += OnGuessChecked;
            game.GameWon += OnGameWon;
            game.GameLost += OnGameLost;
        }
    }
}