﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AppWeather.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<History> Histories { get; set; }

    }
}
