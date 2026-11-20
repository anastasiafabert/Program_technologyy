namespace Eventss;

public class Program
{
    static void Main(string[] args)
    {
        Lot lot = new Lot("Pencel",100m,50m,DateTime.UtcNow.AddDays(1));

        lot.BidPlaced += (obj, e) => Console.WriteLine($"[Log] Ставка :{e.B} - {a}");

        //lot.BidPlaced("FAke", 1_000_000m);

        lot.PlaceBid("nasy", 10_000m);


    }
}