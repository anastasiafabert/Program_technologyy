using System;
using System.Collections.Generic;

namespace _11._09_практика;

/// <summary>
/// Журнал аудита операций.
/// </summary>
public static class AuditLog
{
    private static List<string> _records = new List<string>();

    /// <summary>
    /// Записать операцию в журнал.
    /// </summary>
    public static void Write(string accountNumber, string type, decimal amount, DateTime date)
    {
        _records.Add($"{date:dd.MM.yyyy HH:mm} | {accountNumber} | {type} | {amount}");
    }

    /// <summary>
    /// Вывести весь журнал в консоль.
    /// </summary>
    public static void Print()
    {
        Console.WriteLine(" Журнал аудита: ");
        foreach (var record in _records)
        {
            Console.WriteLine(record);
        }
    }
}