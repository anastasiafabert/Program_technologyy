namespace FoodDelivery
{
    /// <summary>
    /// Интерфейс отслеживания доставки.
    /// </summary>
    public interface ITrackable
    {
        /// <summary>
        /// Отследить доставку.
        /// </summary>
        void Track();
    }
}