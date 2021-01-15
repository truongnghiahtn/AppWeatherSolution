﻿namespace AppWeather.Application.ViewModel.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
