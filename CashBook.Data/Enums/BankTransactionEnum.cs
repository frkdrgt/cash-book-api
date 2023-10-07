using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Data.Enums
{
    public enum BankTransactionEnum
    {
        [Description("Gelir")]
        Income = 1,
        [Description("Gider")]
        Outgoing = 2
    }
}