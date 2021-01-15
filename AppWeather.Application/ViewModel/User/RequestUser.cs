using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeather.Application.ViewModel.User
{
    public class RequestUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
