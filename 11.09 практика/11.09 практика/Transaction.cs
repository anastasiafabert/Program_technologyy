
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _11._09_практика;
//  recoed -сосотояние обьектов класса нельзя изменить


/// <summary>
/// 
/// </summary>
/// <param name="Amount"></param>
/// <param name="Date"></param>
/// <param name="Note"></param>
public record Transaction(decimal Amount, DateTime Date, string Note); //конструктор кратко


///// <summary>
///// Одна транзакция по банковскому счету. сумма, дата и комментраий.
///// </summary>
//internal record Transaction
//{
//    public decimal Amount { get; }
//    public decimal DateTime Date { get; }
//    public decimal string Note { get; }


//    /// <summary>
//    /// Одна транзакция по банковскому счету.
//    /// </summary>
//    /// <param name="Amount"></param>Сумма транзакции. Положительная — пополнение, отрицательная — списание.</param>
//    /// <param name="Date"></param>Дата и время выполнения операции.</param>
//    /// <param name="Note"></param>Текстовый комментарий к операции.</param>
//    public Transaction(decimal Amount, DateTime Date, string Note)
//    {
//        this.Amount = Amount;
//        this.Date = Date;
//        this.Note = Note;


//    }
//}
