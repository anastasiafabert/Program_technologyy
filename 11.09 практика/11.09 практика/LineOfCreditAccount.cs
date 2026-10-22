using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace _11._09_практика;

/// <summary>
/// Кредитный счёт с возможностью ухода в минус до кредитного лимита.
/// </summary>
public class LineOfCreditAccount: BankAccount
{

    /// <summary>
    /// Создаёт кредитный счёт с указанным кредитным лимитом.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="creditLimit">Максимальный размер кредита.</param>
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit) { }

    /// <summary>
    /// Начисляет проценты по кредиту в конце месяца.
    /// </summary>
    public override void PerformMountAndTransaction()
    {
        if(Balance < 0) 
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.UtcNow, "Change montly interest");
        }
    }

    /// <summary>
    /// Возвращает комиссию за овердрафт, если счёт ушёл в минус.
    /// </summary>
    /// <param name="isOverdrawn">true, если счёт ушёл в минус.</param>
    /// <returns>Транзакция комиссии за овердрафт или null, если комиссия не нужна.</returns>
    protected override Transaction? CheckWithdrawlLimit(bool isOverdrawn)
    {
        return isOverdrawn ? new Transaction(-20, DateTime.UtcNow, " apply overdraft") : default;
    }

}
