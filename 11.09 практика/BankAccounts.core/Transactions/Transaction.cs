namespace BankAccounts.Core.Transactions;

// Простой DTO (data transfer object): сумма, дата, примечание (класс хранит только данные неизменяемые)
public sealed record Transaction(decimal Amount, DateTime Date, string Note);
