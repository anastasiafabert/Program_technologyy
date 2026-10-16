namespace FoodDelivery
{
    /// <summary>
    /// Доставка продуктов
    /// </summary>
    public class GroceryDelivery : Delivery
    {
        /// <summary>
        /// Количество товаров
        /// </summary>
        public int ItemCount { get; set; }

        /// <summary>
        /// Большой ли заказ
        /// </summary>
        public bool IsBigOrder
        {
            get { return ItemCount > 10; }
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="oderNumber"> номер заказа </param>
        /// <param name="customName"> имя клиента </param>
        /// <param name="price"> цена </param>
        /// <param name="itemCount"> количество товаров </param>
        public GroceryDelivery(string oderNumber, string customName, decimal price, int itemCount)
            :base(oderNumber, customName, price)
        {
            ItemCount = itemCount;
        }
        

        /// <summary>
        /// Конструктор с 1 параметром
        /// </summary>
        /// <param name="customName"> имя клиента </param>
        public GroceryDelivery(string customName)
            :this("N/A", customName, 0, 0)
        {
        }

        /// <summary>
        /// Доставка
        /// </summary>
        public override void Deliver()
        {
            Status = DeliveryStatus.Delivered;
            Console.WriteLine("Order delivered");
        }

        //НЕ пишем Track()
        //Потому что GroceryDelivery не реализует ITrackable

        /// <summary>
        /// Строковое представление
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[GroceryDelivery] Order #{OrderNumber}, items: {ItemCount}, status: {Status}";

        }

    }
}
