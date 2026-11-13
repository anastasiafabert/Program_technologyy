
using GameLibrary.Enums;
using GameLibrary.Interfaces;
using GameLibrary.Models;

namespace GameLibrary.Services
{
    /// <summary>
    /// Управляет игровым процессом и проверяет предположения игрока.
    /// </summary>
    public class GuessingGame
    {

        /// <summary>
        /// Сообщает! другим компонентам о результате проверки попытки.
        /// </summary>
        public event Action<GuessResult>? GuessChecked;

        /// <summary>
        /// Сообщает! другим компонентам о победе игрока.
        /// </summary>
        public event Action? GameWon;

        /// <summary>
        /// Сообщает! другим компонентам о поражении игрока.
        /// </summary>
        public event Action? GameLost;

        /// <summary>
        /// Хранит генератор случайных чисел, который используется для создания загаданного числа.
        /// </summary>
        private readonly INumberGenerator _numberGenerator;

        /// <summary>
        /// Хранит настройки текущей игры: диапазон чисел и количество попыток.
        /// </summary>
        private readonly GameSettings _settings;

        /// <summary>
        /// Хранит число, которое компьютер загадал для текущего раунда.
        /// </summary>
        private int _secretNumber;

        /// <summary>
        /// Хранит количество использованных обычных попыток
        /// </summary>
        private int _attemptsUsed;

        /// <summary>
        /// Показывает, использовал ли игрок дополнительную попытку.
        /// </summary>
        private bool _extraChanceUsed;

        /// <summary>
        /// Создаёт новый игровой раунд с заданным генератором чисел и настройками.
        /// </summary>
        /// <param name="numberGenerator">Генератор случайных чисел.</param>
        /// <param name="settings">Настройки текущей игры.</param>
        public GuessingGame(INumberGenerator numberGenerator, GameSettings settings)
        {
            _numberGenerator = numberGenerator;
            _settings = settings;

            _secretNumber = _numberGenerator.Generate(_settings.MinNumber, _settings.MaxNumber);

            _attemptsUsed = 0;
            _extraChanceUsed = false;
        }

        /// <summary>
        /// Возвращает общее количество использованных попыток, включая последний шанс.
        /// </summary>
        public int TotalAttemptsUsed
        {
            get
            {
                int total = _attemptsUsed;

                if (_extraChanceUsed)
                {
                    total++;
                }

                return total;
            }
        }

        /// <summary>
        /// Возвращает количество оставшихся обычных попыток.
        /// </summary>
        public int AttemptsRemaining
        {
            get
            {
                return _settings.MaxAttempts - _attemptsUsed;
            }
        }

        /// <summary>
        /// Показывает, доступен ли игроку дополнительный шанс.
        /// </summary>
        public bool CanUseExtraChance
        {
            get
            {
                return _attemptsUsed >= _settings.MaxAttempts
                    && !_extraChanceUsed
                    && !IsWon;
            }
        }

        /// <summary>
        /// Показывает, завершена ли игра.
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                return IsWon || (_attemptsUsed >= _settings.MaxAttempts && _extraChanceUsed);
            }
        }

        /// <summary>
        /// Показывает, угадал ли игрок загаданное число.
        /// </summary>
        public bool IsWon { get; private set; }

        /// <summary>
        /// Проверяет предположение игрока и определяет результат попытки.
        /// </summary>
        /// <param name="guess">Число, которое ввёл игрок.</param>
        /// <returns>Результат проверки предположения.</returns>
        public GuessResult CheckGuess(int guess)
        {
            if (guess < _settings.MinNumber || guess > _settings.MaxNumber)
            {
                throw new ArgumentOutOfRangeException(//исключение
                    nameof(guess),
                    "Число выходит за пределы игрового диапазона.");
            }

            if (CanUseExtraChance || IsGameOver)
            {
                throw new InvalidOperationException("Обычные попытки закончились.");
            }
            _attemptsUsed++;

            GuessResult result;

            if (guess == _secretNumber)
            {
                IsWon = true;
                GameWon?.Invoke();//событие
                result = GuessResult.Correct;
            }
            else if (guess < _secretNumber)
            {
                result = GuessResult.TooLow;
            }
            else
            {
                result = GuessResult.TooHigh;
            }
            GuessChecked?.Invoke(result);
            return result;
        }

        /// <summary>
        /// Позволяет игроку использовать дополнительную попытку.
        /// </summary>
        /// <param name="guess">Число, которое ввёл игрок.</param>
        /// <returns>Результат проверки предположения.</returns>
        public GuessResult UseExtraChance(int guess)
        {
            if (guess < _settings.MinNumber || guess > _settings.MaxNumber)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(guess),
                    "Число выходит за пределы игрового диапазона.");
            }

            if (!CanUseExtraChance)
            {
                throw new InvalidOperationException("Дополнительный шанс недоступен.");
            }

            _extraChanceUsed = true;

            GuessResult result;

            if (guess == _secretNumber)
            {
                IsWon = true;
                GameWon?.Invoke();//вызов события
                result = GuessResult.Correct;
            }
            else if (guess < _secretNumber)
            {
                result = GuessResult.TooLow;
            }
            else
            {
                result = GuessResult.TooHigh;
            }

            GuessChecked?.Invoke(result);
            if (!IsWon)
            {
                GameLost?.Invoke();
            }
            return result;
        }

        /// <summary>
        /// Определяет степень близости предположения игрока к загаданному числу.
        /// </summary>
        /// <param name="guess">Число, которое ввёл игрок.</param>
        /// <returns>Температурная подсказка о близости к загаданному числу.</returns>
        public TemperatureHint GetTemperatureHint(int guess)
        {
            if (guess < _settings.MinNumber || guess > _settings.MaxNumber)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(guess),
                    "Число выходит за пределы игрового диапазона.");
            }

            int difference = Math.Abs(_secretNumber - guess);

            if (difference <= 5)
            {
                return TemperatureHint.Hot;
            }

            if (difference <= 15)
            {
                return TemperatureHint.Warm;
            }

            return TemperatureHint.Cold;
        }
    }
}