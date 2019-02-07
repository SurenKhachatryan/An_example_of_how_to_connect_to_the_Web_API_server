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
        UserContext db = new UserContext();
        User user = new User();

        /// <summary>
        /// Возвращает всех пользователей
        /// </summary>
        [HttpGet]
        public IEnumerable<User> GetAllUser()
        {
            return db.Users;
        }


        /// <summary>
        /// Возвращает пользователя по определенному Id
        /// </summary>
        [HttpGet]
        public HttpResponseMessage GetUser(int id)
        {
            user = db.Users.Where(u => u.UserId == id).FirstOrDefault();

            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Not Found - {id}");
        }


        /// <summary>
        /// Саздает нового пользователя
        /// </summary>
        [HttpPost]
        public HttpResponseMessage CreateNewUser([FromBody]User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// Обнавляет данные пользователя
        /// </summary>
        [HttpPut]
        public HttpResponseMessage UpdateUser(int id, [FromBody] User user)
        {
            this.user = db.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user != null)
            {
                try
                {
                    this.user.Age = user.Age;
                    this.user.Fitst_Name = user.Fitst_Name;
                    this.user.Last_Name = user.Last_Name;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }


        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            user = db.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user != null)
            {
                db.Users.Remove(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Not Found - {id}");
        }
    }
}
