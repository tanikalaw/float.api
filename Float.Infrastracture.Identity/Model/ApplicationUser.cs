﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Float.Infrastracture.Identity.Model
{
   public class ApplicationUser : IdentityUser
    {
        public string Token { get; set; }
        public string DateCreated { get; set; }
    }
}
