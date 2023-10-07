using System;
using System.Collections.Generic;
using System.Text;

namespace CashBook.Services
{
    interface IApiResult
    {
        bool IsSucceed { get; set; }
        string Message { get; }
    }
    interface IApiResult<T> : IApiResult
    {
        T ResultObject { get; set; }
    }
}
