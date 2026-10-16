using System.Net.NetworkInformation;

namespace FoodDelivery
{
    /// <summary>
    /// Доставка суши
    /// </summary>
    public class SushiDelivery : Delivery, ITrackable//наследование , реализация интерфейса
    {

        /// <summary>
        /// Размер набора (шт)
        /// </summary>
        public int SetSize { get; set; }//автосвойство


        /// <summary>
        /// Семейный ли набор (>20)
        /// </summary>
        public bool IsFamilySet
        {
            get { return SetSize > 20; }
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="orderNumber"> номер заказа </param>
        /// <param name="customerName"> имя клиента </param>
        /// <param name="price"> цена </param>
        /// <param name="setSize"> размер набора(шт) </param>
        public SushiDelivery(string orderNumber, string customerName, decimal price, int setSize)
            : base(orderNumber, customerName, price)//вызывает коснтруктор базового класса
        {
            SetSize = setSize;
        }

        /// <summary>
        /// Конструктор с 1 параметром
        /// </summary>
        /// <param name="customerName"> имя клиента </param>
        public SushiDelivery(string customerName) 
            : this("N/A", customerName, 0, 10)//Вызывает другой конструктор этого же класса.
        {

        }

        /// <summary>
        /// Доставка
        /// </summary>
        public override void Deliver()//переопределяем абстрактный метод.
        {
            Status = DeliveryStatus.Delivered;
            Console.WriteLine("Order delivered");
        }

        /// <summary>
        /// Отслеживание
        /// </summary>
        public void Track()//реализация интерфейса.
        {
            Status = DeliveryStatus.OnTheWay;
            Console.WriteLine($"Order #{OrderNumber} tracked: on the way.");
        }

        /// <summary>
        /// Строковое представление.
        /// </summary>
        /// <returns>Строка с информацией о доставке пиццы.</returns>
        public override string ToString()//переопределяем ToString() из Object.
        {
            return $"[SushiDelivery] Order #{OrderNumber}, set size: {SetSize}, status: {Status}";
           
        }
    }
}
