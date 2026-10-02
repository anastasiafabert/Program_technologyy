
using System.Text;

namespace _11._09_практика;
//BankAccount - потомок от обдект = можно переопределить.
//виртуальные методы, находящиеся в обджект

public class BankAccount
{
    private readonly decimal _minimumBalance;

    static private int s_accountNumberSeed = 1000000000;
    //данные конкретного обьекта
    public string Number { get; }//номер счета там
    public string Owner { get; private set; }//владелец
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }
            return balance;


        }
    }


    private List<Transaction> _allTransactions = new List<Transaction>();
    public BankAccount(string name, decimal initialBalance): this (name, initialBalance, 0) { }
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {

        Owner = name; //this.Owner = name  если одинаковые имена
        _minimumBalance = minimumBalance;
        if (initialBalance < 0)
        {
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
        }
        
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++; //
    }

    public void MakeDeposit(decimal amout, DateTime date, string note) //пополнение
    {
        if (amout <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amout), "Amount of deposit must be posisive");

        }
            var deposit = new Transaction(amout, date, note);
            _allTransactions.Add(deposit);


    }
    public void MakeWithdrawal(decimal amout, DateTime date, string note) //снятие
    {
        //if (amout <= 0)
        //{
        //    throw new ArgumentOutOfRangeException(nameof(amout), "Amount of deposit must be posisive");
        //}
        //if (Balance < amout)
        //{
        //    throw new InvalidOperationException("Not s...");
        //}
        //var withdrawal = new Transaction(-amout, date, note);
        //_allTransactions.Add(withdrawal);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amout);
        Transaction? overdraftTransaction = CheckWithdrawlLimit(Balance - amout < _minimumBalance);
        Transaction? withdrawwal = new(-amout, date, note);
        _allTransactions.Add(withdrawwal);

        if (overdraftTransaction is not null) _allTransactions.Add(overdraftTransaction);

    }

    protected virtual Transaction? CheckWithdrawlLimit(bool v) 
    {
        if (v) 
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawal");
        }
        return default;
    }


    public string GetAccountHistory ()
    {
        var repost = new StringBuilder();
        decimal balance = 0;
        repost.AppendLine("Data\t\tAmount\tBAlance\tNote");
        foreach (var item in _allTransactions) 
        {
            balance += item.Amount;
            repost.AppendLine($"" +
                $"{item.Date.ToShortDateString()}\t" +
                $"{item.Amount}\t {balance}\t {item.Note}");
        }
        return repost.ToString();



    }
    //Ключевое слово virtual позволяет в дочернем классе предоставить другую реализацию метода PerformMountAndTransaction
    public virtual void PerformMountAndTransaction() 
    {
       
    }
    //переопределяем метод, котооый унаследовали от обджект
    //Этот метод доожен возвращать строку с состоянием обьекта 
    public override string ToString()
    {
        return $"Type:{GetType().Name} Owner:{Owner} \t Number of account: {Number} ";
    }


}
