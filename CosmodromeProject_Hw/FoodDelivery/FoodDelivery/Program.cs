using System;
using System.Collections.Generic;
namespace FoodDelivery
{


    /// <summary>
    /// Главныц класс
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"> Аргументы командной строки </param>
        static void Main(string[] args)
        {
            //Delivery delivery = new Delivery(); нельз


            List<Delivery> deliveries = new List<Delivery>//созд список баз типа, кладем обьекты наследников 
            {
                new PizzaDelivery("001", "Anna", 800, 40),
                new GroceryDelivery("002", "Lena", 2300, 5)
            };

            foreach (Delivery d in deliveries)
            {
                Console.WriteLine(d.ToString());
                Console.WriteLine($"price with fee: {d.PriceWithFee}");
                d.Deliver();

                if (d is ITrackable trackable)
                {
                    trackable.Track();
                }
                else 
                {
                    Console.WriteLine($"Order #{GetOrderNumber(d)} cannot be tracked.");
                }
                Console.WriteLine("----------------------------------------");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Получить номер заказа из строки
        /// </summary>
        /// <param name="d"> обьект доставки </param>
        /// <returns> номер заказа ("001") </returns>
        static string GetOrderNumber(Delivery d)
        {
            return d.ToString().Split('#')[1].Split(',')[0];
        }
    }
}
