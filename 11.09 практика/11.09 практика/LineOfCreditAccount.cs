using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace _11._09_практика;

public class LineOfCreditAccount: BankAccount
{
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit) { }

    public override void PerformMountAndTransaction()
    {
        if(Balance < 0) 
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.UtcNow, "Change montly interest");
        }
    }

    protected override Transaction? CheckWithdrawlLimit(bool isOverdrawn) => isOverdrawn ? new Transaction(-20, DateTime.UtcNow, " apply overdraft") : default;

}
