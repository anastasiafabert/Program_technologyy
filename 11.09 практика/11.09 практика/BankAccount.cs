
using System.Text;

namespace _11._09_практика;
//BankAccount - потомок от обдект = можно переопределить.
//виртуальные методы, находящиеся в обджект

/// <summary>
/// Банковский счёт клиента.
/// </summary>
public class BankAccount
{
    private readonly decimal _minimumBalance;

    static private int s_accountNumberSeed = 1000000000;
    //данные конкретного обьекта

    /// <summary>
    /// Уникальный номер счёта из 10 цифр.
    /// </summary>
    public string Number { get; }//номер счета там

    /// <summary>
    /// Имя владельца счёта.
    /// </summary>
    public string Owner { get; private set; }//владелец

    /// <summary>
    /// Статус клиента банка. Влияет на условия обслуживания. начальное решуляр
    /// </summary>
    public AccountStatus Status { get; set; }  


    /// <summary>
    /// Текущий баланс счёта.
    /// </summary>
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

    /// <summary>
    /// Список весх транзакций по счету.
    /// </summary>
    private List<Transaction> _allTransactions = new List<Transaction>();

    /// <summary>
    /// Создает банковский счет
    /// </summary>
    /// <param name="name"></param>Имя владельца счёта.</param>
    /// <param name="initialBalance"></param>Начальный баланс счёта.</param>
    public BankAccount(string name, decimal initialBalance): this (name, initialBalance, 0, AccountStatus.Regular) 
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>Имя владельца счёта.</param>
    /// <param name="initialBalance"></param>Начальный баланс счёта.</param>
    /// <param name="minimumBalance"></param>Минимально допустимый баланс счёта.</param>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance, AccountStatus status = AccountStatus.Regular)
    {

        Owner = name; //this.Owner = name  если одинаковые имена
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
        {
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
        }
        
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++; //
    }

    /// <summary>
    /// Пополняет счёт на указанную сумму.
    /// </summary>
    /// <param name="amout">Сумма пополнения. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amout"/> меньше или равен нулю.
    /// </exception>
    public void MakeDeposit(decimal amout, DateTime date, string note) //пополнение
    {
        if (amout <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amout), "Amount of deposit must be posisive");

        }
            var deposit = new Transaction(amout, date, note);
            _allTransactions.Add(deposit);


    }

    /// <summary>
    /// Снимает указанную сумму со счёта и создаёт транзакцию списания.
    /// </summary>
    /// <param name="amout">Сумма снятия. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amout"/> меньше или равен нулю.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если на счёте недостаточно средств.
    /// </exception>
    public void MakeWithdrawal(decimal amout, DateTime date, string note) //снятие
    {
        

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amout);
        Transaction? overdraftTransaction = CheckWithdrawlLimit(Balance - amout < _minimumBalance);
        Transaction? withdrawwal = new(-amout, date, note);
        _allTransactions.Add(withdrawwal);

        if (overdraftTransaction is not null) _allTransactions.Add(overdraftTransaction);

    }

    /// <summary>
    /// Проверяет, не превышен ли лимит снятия, и при необходимости создаёт транзакцию комиссии за овердрафт.
    /// </summary>
    /// <param name="v">true, если баланс после операции уходит ниже минимального.</param>
    /// <returns>Транзакция комиссии или null, если комиссия не требуется.</returns>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если снятие превышает допустимый лимит.
    /// </exception>
    protected virtual Transaction? CheckWithdrawlLimit(bool v) 
    {
        if (v) 
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawal");
        }
        return default;
    }


    /// <summary>
    ///  Возвращает историю всех транзакций по счёту в виде текста.
    /// </summary>
    /// <returns>Многострочная строка с датой, суммой, балансом и комментарием каждой операции.</returns>
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
    /// <summary>
    /// Выполняет операции в конце месяца в зависимости от статуса клиента.
    /// </summary>
    public virtual void PerformMountAndTransaction()
    {
        switch (Status)
        {
            case AccountStatus.Regular:
                MakeWithdrawal(50m, DateTime.UtcNow, "Monthly fee Regular");
                break;

            case AccountStatus.Premium:
                if (Balance > 10000m)
                {
                    decimal premiumInterest = (Balance - 10000m) * 0.01m;
                    MakeDeposit(premiumInterest, DateTime.UtcNow, "Premium interest");
                }
                break;

            case AccountStatus.VIP:
                if (Balance > 10000m)
                {
                    decimal vipInterest = (Balance - 10000m) * 0.003m;
                    MakeDeposit(vipInterest, DateTime.UtcNow, "VIP interest");
                }

                // кэшбэк 5% на все списания (по знаку Amount)
                decimal totalWithdrawals = 0m;
                foreach (var t in _allTransactions)
                {
                    if (t.Amount < 0)
                        totalWithdrawals += -t.Amount;
                }
                decimal cashback = totalWithdrawals * 0.05m;
                if (cashback > 0)
                {
                    MakeDeposit(cashback, DateTime.UtcNow, "VIP cashback");
                }
                break;
        }
    }

    //переопределяем метод, котооый унаследовали от обджект
    //Этот метод доожен возвращать строку с состоянием обьекта 

    /// <summary>
    /// Возвращает строковое представление счёта: тип, владелец и номер.
    /// </summary>
    /// <returns>Строка с типом счёта, именем владельца и номером счёта.</returns>
    public override string ToString()
    {
        return $"Type:{GetType().Name} Owner:{Owner} \t Number of account: {Number} ";
    }

}
