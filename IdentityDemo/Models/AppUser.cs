﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Models
{
    public class AppUser:IdentityUser
    {
        public bool IsNewUser { get; set; }
        public int Counter { get; set; }
    }
}
