using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UsersDataWebAPI.Models;

namespace UsersDataWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private UserContext db = new UserContext();
        public IEnumerable<User> GetAllUser()
        {
            return db.Users;
        }
    }
}
