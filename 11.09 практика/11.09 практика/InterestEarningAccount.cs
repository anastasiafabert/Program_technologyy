namespace _11._09_практика;




/// <summary>
/// Сберегательный счёт с начислением процентов на остаток.
/// </summary>
public class InterestEarningAccount:BankAccount
{
    /// <summary>
    /// <summary>
    /// Создаёт сберегательный счёт с указанным владельцем и начальным балансом.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance) 
    {


    }

    //override позволяет в дочернем классе определить новую реализацию

    /// <summary>
    /// Начисляет проценты на остаток в конце месяца.если баланс превышает 500.
    /// </summary>
    public override void PerformMountAndTransaction()
    {
        if (Balance > 500m) 
        {
            decimal interest = Balance + 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "aply month interest");
        }
    }
}
