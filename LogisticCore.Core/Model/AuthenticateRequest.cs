﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Core.Model
{
    public class AuthenticateRequest
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
