using System;
using System.Collections.Generic;
using System.Text;

namespace _11._09_практика
{
    /// <summary>
    /// Подарочный счёт с ежемесячным автоматическим пополнением.
    /// </summary>
    public class GiftCartAccount:BankAccount
    {

        /// <summary>
        /// Сумма ежемесячного пополнения.
        /// </summary>
        private readonly decimal _monthlyDeposit = 0m;


        //monthlyDeposit - параметр по умолчанию 0
        //при создании new GiftCartAccount("nasy",1000); => monthlyDeposit = 0
        //GiftCartAccount("nasy",1000,5000); => monthlyDeposit = 5000

        /// <summary>
        /// Создаёт подарочный счёт с указанной суммой ежемесячного пополнения.
        /// </summary>
        /// <param name="name">Имя владельца счёта.</param>
        /// <param name="initialBalance">Начальный баланс счёта.</param>
        /// <param name="monthlyDeposit">Сумма ежемесячного пополнения. По умолчанию 0.</param>
        public GiftCartAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance) => _monthlyDeposit = monthlyDeposit;

        /// <summary>
        /// Вносит ежемесячное пополнение, если задано.
        /// </summary>
        public override void PerformMountAndTransaction() 
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add montly deposit");
            
            }
        }
        //base.ToString() - вызов базовой реализации => реализации из класса BankAccount

        /// <summary>
        /// Возвращает строковое представление счёта с суммой ежемесячного пополнения.
        /// </summary>
        /// <returns>Строка с базовой информацией счёта и суммой ежемесячного пополнения.</returns>
        public override string ToString()
        {
            return base.ToString() + $"montly deposit: {_monthlyDeposit}";
        }
       
        
    }
}
