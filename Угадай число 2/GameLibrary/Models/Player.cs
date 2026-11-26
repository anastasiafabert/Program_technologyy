
namespace Угадай_число_2.Models
{
    /// <summary>
    /// Хранит информацию об игроке.
    /// </summary>
    public class Player
    {
        public string PlayerName { get; }

        public Player(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
