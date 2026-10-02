namespace _11._09_практика;

//

public class InterestEarningAccount:BankAccount
{
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance) 
    {


    }

    //override позволяет в дочернем классе определить новую реализацию

    public override void PerformMountAndTransaction()
    {
        if (Balance > 500m) 
        {
            decimal interest = Balance + 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "aply month interest");
        }
    }
}
