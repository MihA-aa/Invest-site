using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string Login { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}
