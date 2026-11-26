
using GameLibrary.Enums;
using GameLibrary.Interfaces;
using GameLibrary.Models;
using Угадай_число_2.Models;

namespace GameLibrary.Services
{
    /// <summary>
    /// Управляет многопользовательской игрой.
    /// </summary>
    public class MultiplayerGame
    {
        private readonly IUserInterface _userInterface;

        /// <summary>
        /// Создаёт многопользовательскую игру.
        /// </summary>
        /// <param name="userInterface">Интерфейс взаимодействия с пользователем.</param>
        public MultiplayerGame(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public GuessingGame GuessingGame
        {
            get => default;
            set
            {
            }
        }

        public Player Player
        {
            get => default;
            set
            {
            }
        }

        public IUserInterface IUserInterface
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Запускает игру для двух игроков.
        /// </summary>
        /// <param name="firstPlayer">Первый игрок.</param>
        /// <param name="secondPlayer">Второй игрок.</param>
        /// <param name="game">Игровой объект.</param>
        public void Play(
            Player firstPlayer,
            Player secondPlayer,
            GuessingGame game)
        {
            bool firstPlayersTurn = true;

            _userInterface.ShowMessage(
                $"Играют {firstPlayer.PlayerName} и {secondPlayer.PlayerName}!");

            _userInterface.ShowMessage(
                $"Угадайте число от {game.Settings.MinNumber} до {game.Settings.MaxNumber}.");

            while (!game.IsGameOver)
            {
                Player currentPlayer = firstPlayersTurn
                    ? firstPlayer
                    : secondPlayer;

                _userInterface.ShowMessage(
                    $"\nХод игрока: {currentPlayer.PlayerName}");

                string input = _userInterface.ReadInput("Введите число: ");

                if (!int.TryParse(input, out int guess))
                {
                    _userInterface.ShowMessage("Введите целое число!");
                    continue;
                }

                if (guess < game.Settings.MinNumber ||
                    guess > game.Settings.MaxNumber)
                {
                    _userInterface.ShowMessage(
                        $"Введите число от {game.Settings.MinNumber} до {game.Settings.MaxNumber}!");
                    continue;
                }

                GuessResult result;

                if (game.CanUseExtraChance)
                {
                    result = game.UseExtraChance(guess);
                }
                else
                {
                    result = game.CheckGuess(guess);
                }

                switch (result)
                {
                    case GuessResult.Correct:
                        _userInterface.ShowMessage(
                            $"Победил игрок {currentPlayer.PlayerName}!");
                        return;

                    case GuessResult.TooLow:
                        _userInterface.ShowMessage(
                            "Загаданное число больше!");
                        break;

                    case GuessResult.TooHigh:
                        _userInterface.ShowMessage(
                            "Загаданное число меньше!");
                        break;
                }

                firstPlayersTurn = !firstPlayersTurn;
            }

            _userInterface.ShowMessage(
                "Попытки закончились. Победителя нет!");
        }
    }
}
