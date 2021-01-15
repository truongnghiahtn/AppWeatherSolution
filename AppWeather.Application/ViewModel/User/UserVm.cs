using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeather.Application.ViewModel.User
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
