
using GameLibrary.Enums;
using GameLibrary.Interfaces;
using GameLibrary.Models;

namespace GameLibrary.Services
{
    /// <summary>
    /// Управляет игровым процессом, проверяет предположения игрока
    /// и сообщает другим компонентам о результатах игры.
    /// </summary>
    public class GuessingGame 
    {
        /// <summary>
        /// Сообщает другим компонентам о результате проверки попытки.
        /// </summary>
        public event Action<GuessResult>? GuessChecked;

        /// <summary>
        /// Сообщает другим компонентам о победе игрока.
        /// </summary>
        public event Action? GameWon;

        /// <summary>
        /// Сообщает другим компонентам о поражении игрока.
        /// </summary>
        public event Action? GameLost;

        /// <summary>
        /// Хранит генератор случайных чисел.
        /// </summary>
        private readonly INumberGenerator _numberGenerator;

        /// <summary>
        /// Хранит настройки текущей игры.
        /// </summary>
        private readonly GameSettings _settings;

        /// <summary>
        /// Хранит загаданное компьютером число.
        /// </summary>
        private readonly int _secretNumber;

        /// <summary>
        /// Хранит количество использованных обычных попыток.
        /// </summary>
        private int _attemptsUsed;

        /// <summary>
        /// Показывает, использовал ли игрок дополнительную попытку.
        /// </summary>
        private bool _extraChanceUsed;

        /// <summary>
        /// Создаёт новый игровой раунд с заданным генератором
        /// случайных чисел и настройками.
        /// </summary>
        /// <param name="numberGenerator">Генератор случайных чисел.</param>
        /// <param name="settings">Настройки текущей игры.</param>
        public GuessingGame(
            INumberGenerator numberGenerator,
            GameSettings settings)
        {
            ArgumentNullException.ThrowIfNull(numberGenerator);
            ArgumentNullException.ThrowIfNull(settings);

            _numberGenerator = numberGenerator;
            _settings = settings;

            _secretNumber = _numberGenerator.Generate(
                _settings.MinNumber,
                _settings.MaxNumber);

            _attemptsUsed = 0;
            _extraChanceUsed = false;
        }

        /// <summary>
        /// Возвращает общее количество использованных попыток,
        /// включая дополнительную попытку.
        /// </summary>
        public int TotalAttemptsUsed
        {
            get
            {
                return _attemptsUsed + (_extraChanceUsed ? 1 : 0);
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
                return IsWon ||
                    (_attemptsUsed >= _settings.MaxAttempts && _extraChanceUsed);
            }
        }

        /// <summary>
        /// Показывает, угадал ли игрок загаданное число.
        /// </summary>
        public bool IsWon { get; private set; }

        /// <summary>
        /// Возвращает настройки текущей игры.
        /// </summary>
        public GameSettings Settings => _settings;

        public GameStatistics GameStatistics
        {
            get => default;
            set
            {
            }
        }

        public INumberGenerator INumberGenerator
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Проверяет предположение игрока.
        /// </summary>
        /// <param name="guess">Число, которое ввёл игрок.</param>
        /// <returns>Результат проверки предположения.</returns>
        public GuessResult CheckGuess(int guess)
        {
            ValidateGuess(guess);

            if (IsGameOver || CanUseExtraChance)
            {
                throw new InvalidOperationException(
                    "Обычные попытки закончились.");
            }

            _attemptsUsed++;

            return EvaluateGuess(guess);
        }

        /// <summary>
        /// Позволяет игроку использовать дополнительную попытку.
        /// </summary>
        /// <param name="guess">Число, которое ввёл игрок.</param>
        /// <returns>Результат проверки предположения.</returns>
        public GuessResult UseExtraChance(int guess)
        {
            ValidateGuess(guess);

            if (!CanUseExtraChance)
            {
                throw new InvalidOperationException(
                    "Дополнительный шанс недоступен.");
            }

            _extraChanceUsed = true;

            GuessResult result = EvaluateGuess(guess);

            if (!IsWon)
            {
                GameLost?.Invoke();
            }

            return result;
        }

        /// <summary>
        /// Проверяет число и уведомляет подписчиков о результате.
        /// </summary>
        /// <param name="guess">Предположение игрока.</param>
        /// <returns>Результат проверки числа.</returns>
        private GuessResult EvaluateGuess(int guess)
        {
            GuessResult result;

            if (guess == _secretNumber)
            {
                IsWon = true;
                result = GuessResult.Correct;

                GameWon?.Invoke();
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
        /// Проверяет, входит ли предположение игрока в игровой диапазон.
        /// </summary>
        /// <param name="guess">Число для проверки.</param>
        private void ValidateGuess(int guess)
        {
            if (guess < _settings.MinNumber || guess > _settings.MaxNumber)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(guess),
                    "Число выходит за пределы игрового диапазона.");
            }
        }

        /// <summary>
        /// Определяет степень близости предположения
        /// игрока к загаданному числу.
        /// </summary>
        /// <param name="guess">Число, которое ввёл игрок.</param>
        /// <returns>Температурная подсказка о близости к загаданному числу.</returns>
        public TemperatureHint GetTemperatureHint(int guess)
        {
            ValidateGuess(guess);

            long difference = Math.Abs((long)_secretNumber - guess);

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
