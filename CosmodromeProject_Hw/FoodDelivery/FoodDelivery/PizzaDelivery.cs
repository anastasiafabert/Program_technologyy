namespace FoodDelivery
{
    /// <summary>
    /// Доставка пиццы.
    /// </summary>
    public class PizzaDelivery : Delivery, ITrackable
    { 
        /// <summary>
        /// РАзмер пиццы (см)
        /// </summary>
        public int PizzaSize { get; set; }


        /// <summary>
        /// Большая ли пицца (>35)
        /// </summary>
        public bool IsLargePizza 
        {
            get { return PizzaSize > 35; }
        }

        /// <summary>
        /// Конструктор с параментрами
        /// </summary>
        /// <param name="orderNumber"> номер заказа </param>
        /// <param name="customerName"> имя клиена </param>
        /// <param name="price"> цена </param>
        /// <param name="pizzaSize"> размер пиццы</param>
        public PizzaDelivery(string orderNumber, string customerName, decimal price, int pizzaSize)
            :base(orderNumber, customerName, price)//вызывает коснтруктор базового класса
        {
            PizzaSize = pizzaSize;  
        }

        /// <summary>
        /// Конструктор с 1 параметром
        /// </summary>
        /// <param name="customerName"> иям клиента </param>
        public PizzaDelivery(string customerName) 
            :this ("N/A", customerName, 0, 30)
        {

        }

        /// <summary>
        /// Доставка
        /// </summary>
        public override void Deliver() 
        {
            Status = DeliveryStatus.Delivered;
            Console.WriteLine("Order delivered.");
        }

        /// <summary>
        /// Отслеживание 
        /// </summary>
        public void Track()
        {
            Status = DeliveryStatus.OnTheWay;
            Console.WriteLine($"Order # {OrderNumber} tracked: on the way.");
        }

        /// <summary>
        /// Строковое представление
        /// </summary>
        /// <returns>Строка с информацией о доставке пиццы.</returns>
        public override string ToString()
        {
            return $"[PizzaDelivery] Order #{OrderNumber}, pizza size: {PizzaSize}, status: {Status}";
        }
    }
}
