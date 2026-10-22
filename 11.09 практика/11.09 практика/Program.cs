namespace _11._09_практика

    //Классы банковских считов

{

    /// <summary>
    /// Точка входа в программу. Демонстрирует работу с банковскими счетами.
    /// </summary>
    internal class Program
    {

        /// <summary>
        /// Точка входа в программу. Демонстрирует работу с банковскими счетами разных типов.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("nasy", 5000000000);

            BankAccount account2 = new BankAccount("nasy", 7000000000);
            Console.WriteLine($"account {account.Balance} {account.Number} {account.Owner}");
            Console.WriteLine($"account {account2.Balance} {account2.Number} {account2.Owner}");

            account.MakeDeposit(100, DateTime.UtcNow, ":)");
            Console.WriteLine(account.Balance);
            account.MakeDeposit(10, DateTime.UtcNow, ":)");
            Console.WriteLine(account.Balance);
            Console.WriteLine(account2.GetAccountHistory());
            try
            {
                account2.MakeWithdrawal(100000000, DateTime.UtcNow, ":(");
            }
            catch (InvalidOperationException e)
            { 
                Console.WriteLine(e.Message );
            }


            InterestEarningAccount interestEarning = new("nasy", 1000m);
            interestEarning.MakeDeposit(10000m, DateTime.UtcNow, " :) ");
            interestEarning.MakeWithdrawal(10m, DateTime.UtcNow, " :) ");
            interestEarning.PerformMountAndTransaction();
            


            Console.WriteLine(interestEarning);

            Console.WriteLine(interestEarning.GetAccountHistory());
            LineOfCreditAccount lineOfCredit = new LineOfCreditAccount("nasy", 0, 1000m);
            lineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, "credit");


            GiftCartAccount giftCart = new("nasy", 1000m, 5000m);

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(account2);
            accounts.Add(interestEarning);
            accounts.Add(lineOfCredit);
            accounts.Add(giftCart);

            foreach (BankAccount account1 in accounts)
            {
                Console.WriteLine(account1);
                account1.PerformMountAndTransaction();//рабоатте палиморфизм
                Console.WriteLine(account1.GetAccountHistory());
            }
            giftCart.MakeDeposit(100m, DateTime.UtcNow, " :) ");
            giftCart.MakeDeposit(10m, DateTime.UtcNow, " :) ;(");
            giftCart.PerformMountAndTransaction();

            Console.WriteLine(giftCart);
            Console.WriteLine(giftCart.GetAccountHistory());

            //Задача 1
            Console.WriteLine("\n Проверка статусов");

            // Regular — комиссия 50 руб/мес
            BankAccount regular = new("Regular-client", 20000m);
            regular.PerformMountAndTransaction();
            Console.WriteLine($"Regular: баланс = {regular.Balance} (ожидалось 19950)");

            // Premium — 1% на остаток свыше 10 000
            BankAccount premium = new("Premium-client", 20000m);
            premium.Status = AccountStatus.Premium;
            premium.PerformMountAndTransaction();
            Console.WriteLine($"Premium: баланс = {premium.Balance} (ожидалось 20100)");

            // VIP — 0,3% на остаток свыше 10 000 + кэшбэк 5% на списания
            BankAccount vip = new("VIP-client", 20000m);
            vip.Status = AccountStatus.VIP;
            vip.MakeWithdrawal(1000m, DateTime.UtcNow, "покупка");
            vip.PerformMountAndTransaction();
            Console.WriteLine($"VIP: баланс = {vip.Balance} (ожидалось ~19290)");

            AuditLog.Print();   
            Console.ReadKey();  
        }

    }
}
