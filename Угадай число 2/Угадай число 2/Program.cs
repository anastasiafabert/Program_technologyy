
using Угадай_число_2.Enums;
using Угадай_число_2.Models;
using Угадай_число_2.Services;

namespace Угадай_число_2
{
    /// <summary>
    /// Содержит точку входа в приложение и запускает игру «Угадай число».
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Запускает приложение и создаёт основные игровые компоненты.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            // Создаём настройки игры для лёгкого уровня.
            GameSettings settings = GameSettings.CreateForDifficulty(DifficultyLevel.Easy);

            // Создаём генератор случайных чисел.
            NumberGenerator numberGenerator = new NumberGenerator();

            // Создаём новый игровой раунд.
            GuessingGame game = new GuessingGame(numberGenerator, settings);

            // Создаём обработчик игровых событий.
            GameEventHandler eventHandler = new GameEventHandler();

            // Подписываем обработчик на события игры.
            eventHandler.Subscribe(game);

            
        }
    }
}
