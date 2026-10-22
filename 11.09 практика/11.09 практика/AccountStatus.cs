using System;
using System.Collections.Generic;
using System.Text;

namespace _11._09_практика;

/// <summary>
/// Статус клиента банка, влияющий на условия обслуживания.
/// </summary>
public enum AccountStatus
{
    /// <summary>Обычный клиент. Комиссия 50 руб/мес.</summary>
    Regular,

    /// <summary>Премиум-клиент. Без комиссии, 1% на остаток свыше 10 000.</summary>
    Premium,

    /// <summary>VIP-клиент. Без комиссии, 0,3% на остаток + кэшбэк 5% на списания.</summary>
    VIP
}