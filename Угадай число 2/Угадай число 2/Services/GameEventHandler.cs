
using GameLibrary.Enums;
using GameLibrary.Services;

namespace Угадай_число_2.Services
{
    /// <summary>
    /// Обрабатывает события игрового процесса
    /// и выводит сообщения для пользователя.
    /// </summary>
    public class GameEventHandler
    {
        public GuessingGame GuessingGame
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Обрабатывает результат проверки предположения игрока.
        /// </summary>
        /// <param name="result">Результат проверки числа.</param>
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
            Console.WriteLine("Поздравляем! Вы победили! 😸");
        }

        /// <summary>
        /// Обрабатывает поражение игрока.
        /// </summary>
        public void OnGameLost()
        {
            Console.WriteLine("Вы проиграли! Попробуйте ещё раз. 😿");
        }

        /// <summary>
        /// Подписывает обработчик на события игрового процесса.
        /// </summary>
        /// <param name="game">Текущий игровой раунд.</param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если игра равна null.
        /// </exception>
        public void Subscribe(GuessingGame game)
        {
            ArgumentNullException.ThrowIfNull(game);

            game.GuessChecked += OnGuessChecked;
            game.GameWon += OnGameWon;
            game.GameLost += OnGameLost;
        }
    }
}
