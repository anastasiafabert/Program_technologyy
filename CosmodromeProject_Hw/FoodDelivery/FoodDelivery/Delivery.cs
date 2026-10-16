namespace FoodDelivery
{
    /// <summary>
    /// Базовый класс доставки.
    /// </summary>
    public abstract class Delivery
    {
        /// <summary>
        /// Номер заказа.
        /// </summary>
        private string _orderNumber;

        /// <summary>
        /// Имя клиента.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Статус доставки.
        /// </summary>
        public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;

        /// <summary>
        /// Цена с надбавкой.
        /// </summary>
        public decimal PriceWithFee
        {
            get { return Price + 200; }
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="orderNumber"> номер заказа </param>
        /// <param name="customerName"> имя клиента </param>
        /// <param name="price"> цена </param>
        public Delivery(string orderNumber, string customerName, decimal price)
        {
            _orderNumber = orderNumber;
            CustomerName = customerName;
            Price = price;
        }

        /// <summary>
        /// Конструктор с одним параметром
        /// </summary>
        /// <param name="customerName"> имя клиента </param>
        public Delivery(string customerName)
            : this("N/A", customerName, 0)
        {
        }

        /// <summary>
        /// Абстрактный метод доставки.
        /// </summary>
        public abstract void Deliver();//нет реалзц 

        /// <summary>
        /// Номер заказа для наследников.
        /// </summary>
        protected string OrderNumber
        {
            get { return _orderNumber; }
        }
    }
}