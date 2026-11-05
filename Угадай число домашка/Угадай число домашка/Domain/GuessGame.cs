using System;

namespace Угадай_число_домашка
{
    /// <summary>
    /// Главный класс игры.
    /// </summary>
    public class GuessGame
    {
        private readonly IGameIO _io;
        private readonly IRandomNumberGenerator _rng;
        private readonly IDifficultyLevel _level;

        public GuessGame(IGameIO io, IRandomNumberGenerator rng, IDifficultyLevel level)
        {
            _io = io;
            _rng = rng;
            _level = level;
        }

        /// <summary>
        /// Запускает игру. Возвращает количество попыток, или -1 если проиграл.
        /// </summary>
        public int Play()
        {
            var target = _rng.Next(_level.MinNumber, _level.MaxNumber);
            var attempts = 0;

            _io.WriteLine($"Я загадал число от {_level.MinNumber} до {_level.MaxNumber}. У тебя {_level.MaxAttempts} попыток.");

            while (attempts < _level.MaxAttempts)
            {
                _io.Write("Твой вариант: ");
                if (!int.TryParse(_io.ReadLine(), out var guess))
                {
                    _io.WriteLine("Ошибка! Введи число.");
                    continue;
                }

                attempts++;

                if (guess == target)
                {
                    _io.WriteLine($"Победа! Ты угадал за {attempts} попыток.");
                    return attempts; // Возвращаем попытки (успех)
                }

                _io.WriteLine(guess < target ? "Загаданное число БОЛЬШЕ." : "Загаданное число МЕНЬШЕ.");
            }

            _io.WriteLine($"Ты проиграл. Было загадано число {target}.");
            return -1; // Возвращаем -1 (проигрыш)
        }
    }
}