﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserEntity
    {
        public int UsersId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public string Address { get; set; }
        public string CreatedTime { get; set; }

        public string UpdatedTime { get; set; }

        public string Password { get; set; }

        public string Flag { get; set; }    

    }
}
