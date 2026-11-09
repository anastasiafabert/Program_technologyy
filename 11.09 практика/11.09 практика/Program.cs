using BankAccounts.Core.AccountNumbers;
using BankAccounts.Core.Factories;
using BankAccounts.Core.Statements;
using BankAccounts.Core.Time;

namespace BankAccounts.Console
{
    internal class Program
    {
        //плохо в прогрма создавать нов, каждый раз новыую генерац созд (надо номера уникальные)
        static void Main(string[] args)
        {
            var clock = new SystemClock();
            var factory = new AccountFactory(clock, new SequentialAccountNumberGenerator());
            var formatter = new TextStatementFormatter();

            var savings = factory.CreateInterestEarning("Nasy", 10000m);
            savings.Deposit(100m, clock.Now, "asda");
            System.Console.WriteLine(savings.Number);
            System.Console.WriteLine(formatter.Format(savings.Transactions));

            var deposit = factory.CreatDebitAccount("nasy", 1000m);
            deposit.Withdraw(500m, clock.Now, "asda");

            

            //foreach(var account in accounts)
            //{
            //    System.Console.WriteLine(f)
            //}

            
        }
    }
}