namespace Eventss
{
    public  sealed class BigPlacedEventsArgs:EventArgs
    {
        public string Title { get; }
        public string Bidder { get; }

        public decimal Amount { get; }
        public decimal? PreviosAmount { get; }
        public string? PreviosLeader { get; }

        public BigPlacedEventsArgs(string title, string bidder, decimal amount, decimal? previosAmount, string? previosLeader)
        {
            Title = title;
            Bidder = bidder;
            Amount = amount;
            PreviosAmount = previosAmount;
            PreviosLeader = previosLeader;
        }
    }
}