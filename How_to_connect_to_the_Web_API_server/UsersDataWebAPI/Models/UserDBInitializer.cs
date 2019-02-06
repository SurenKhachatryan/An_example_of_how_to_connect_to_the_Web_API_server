using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace UsersDataWebAPI.Models
{
    public class UserDBInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            db.Users.Add(new User { Fitst_Name = "Tom", Last_Name = "Hardy", Age = 41 });
            db.Users.Add(new User { Fitst_Name = "Jim", Last_Name = "Carrey", Age = 57 });
            db.Users.Add(new User { Fitst_Name = "Robin", Last_Name = "Williams", Age = 63 });
            base.Seed(db);
        }
    }
}