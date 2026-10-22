using System;

namespace _11._09_практика;

/// <summary>
/// Банк. Управляет переводами между счетами.
/// </summary>
public static class Bank
{
    /// <summary>
    /// Перевести деньги со счёта на счёт.
    /// </summary>
    /// <param name="from">Счёт списания.</param>
    /// <param name="to">Счёт зачисления.</param>
    /// <param name="amount">Сумма перевода.</param>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если при зачислении произошла ошибка.
    /// </exception>
    public static void Transfer(BankAccount from, BankAccount to, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Transfer amount must be positive");

        // Шаг 1. Снять со счёта A
        from.MakeWithdrawal(amount, DateTime.UtcNow, $"Перевод на {to.Number}");

        try
        {
            // Шаг 2. Зачислить на счёт B
            to.MakeDeposit(amount, DateTime.UtcNow, $"Перевод со {from.Number}");
        }
        catch
        {
            // Если зачисление упало — вернуть деньги на счёт A
            from.MakeDeposit(amount, DateTime.UtcNow, "Откат перевода");
            throw;
        }
    }
}