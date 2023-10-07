using CashBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashBook.Services
{
    public class ApiResult : IApiResult
    {
        public ApiResult()
        {
            IsSucceed = false;
            HasRight = true;
        }

        public bool IsSucceed { get; set; }
        public bool HasRight { get; set; }
        public string Message { get; set; }
    }

    public class ApiResult<T> : ApiResult, IApiResult<T>
    {
        public ApiResult() : base()
        {
        }


        public T ResultObject { get; set; }
    }

}
