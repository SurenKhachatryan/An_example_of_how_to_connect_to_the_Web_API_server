using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsersDataWebAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Fitst_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
    }
}