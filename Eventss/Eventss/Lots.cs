using System;
using System.Collections.Generic;
using System.Text;

namespace Eventss
{
    public class Lot
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// название
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// начальная цена
        /// </summary>
        public decimal StartPrice { get; }

        /// <summary>
        /// ставка
        /// </summary>
        public decimal BidStep { get; }

        /// <summary>
        /// псоледняя ставка
        /// </summary>
        public decimal? CurrentBid { get; private set; } = default;// default как и =null

        /// <summary>
        /// имя лидера
        /// </summary>
        public string? CurrentLeader { get; private set; } = default;//последняя ставка может хранить нуллб

        /// <summary>
        /// цена мгновенного выкупа
        /// </summary>
        public decimal? BuyNowPrice { get; }

        /// <summary>
        /// время окончания торгов
        /// </summary>
        public DateTime EndsAt { get; }

        /// <summary>
        /// оповещение, что ставка произошла
        /// </summary>
        //public Action<string, decimal>? BidPlaced;//актион делегаты - ссылки на метододы, которые ничего не возвращ

        /// <summary>
        /// оповещение, что произошел мгновенный выкуп
        /// </summary>
        //public Action<string, decimal>? Repurchase;//? - нулл может быть

        //оповещение, что ставка произошла
        public event EventHandler<BigPlacedEventsArgs>? BigPlaced;

        public event EventHandler<BuyNowTriggerEventArgs>? Repurchase;

        public Lot(string title, decimal startPrice, decimal bidStep,
            DateTime endsAt, decimal? buyNowPrice = default)
        {
            Id = Guid.NewGuid();
            Title = title;
            StartPrice = startPrice;
            BidStep = bidStep;
            EndsAt = endsAt;
            BuyNowPrice = buyNowPrice;
        }

        /// <summary>
        /// мгновенный выкуп
        /// </summary>
        /// <param name="buyer"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void BuyNow(string buyer)
        {
            //	фиксирует продажу(можно просто вывести в консоль «лот выкуплен» и запомнить победителя);
            //	оповещает подписчиков — но вот вопрос: через BidPlaced? Или нужен новый делегат? Реши сам и напиши, почему выбрал так.
            if (BuyNowPrice is null || EndsAt < DateTime.UtcNow)
                throw new InvalidOperationException("Вы не можете выкупить этот лот");

            if (CurrentBid is null || CurrentBid < BuyNowPrice)
            {
                CurrentBid = BuyNowPrice;
                CurrentLeader = buyer;

                //оповещаем о мгновенном выкупе лота
                //Repurchase(buyer, BuyNowPrice!.Value);пробл

                OnBuyNowTrigger(new BuyNowTriggerEventArgs(buyer, CurrentBid!.Value));//!-точно знаю, что в каррентбиг не будет нулЛ
            }

            throw new InvalidOperationException("Вы не можете выкупить этот лот");


        }

        protected virtual void OnBuyNowTrigger(BuyNowTriggerEventArgs e)
            => Repurchase?.Invoke(this, e);

        public void PlaceBid(string bidder, decimal amount)
        {
            if (DateTime.UtcNow >= EndsAt)
                throw new InvalidOperationException("Торги уже завершены");

            if (CurrentBid is null && amount < StartPrice)
                throw new InvalidOperationException("Ставка слишком мала");

            if (CurrentBid is not null && amount < CurrentBid + BidStep)
                throw new InvalidOperationException("Ставка слишком мала");

            var previosBig = CurrentBid;
            var previosLeader = CurrentLeader;

            CurrentBid = amount;
            CurrentLeader = bidder;

            ////уведомляем тех, кто подписался на это событие, что ставка произошла
            //BidPlaced?.(bidder, amount);//проблема! если никто не подписался, то исключение

            //BidPlaced?.Invoke(bidder, amount);

            OnBigPlaced(new BigPlacedEventsArgs(Title, bidder, amount, previosBig, previosLeader));
        }
        protected virtual void OnBigPlaced(BigPlacedEventsArgs e) 
            => BigPlaced?.Invoke(this, e);
    }

}
