using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeather.Application.ViewModel.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T Result)
        {
            IsSuccessed = true;
            Data = Result;
            Message = "Success";
        }
        public ApiSuccessResult()
        {
            IsSuccessed = true;
            Message = "Success";
        }
    }
}
