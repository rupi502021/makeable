using MakeAble.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakeAble.Controllers
{
    public class UsersController : ApiController
    {
       
        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Users/{email}/")]
        public HttpResponseMessage Get(string email)
        {
            try
            {
                User user = new User();
                List<User> uList = user.ReadUsersPro(email);
                List<string> professions = new List<string>();
                var last = uList.LastOrDefault();
                foreach (var item in uList)
                {
                    professions.Add(item.Profession);
                    if (item.Equals(last))
                    {
                        item.Professions = professions;
                        return Request.CreateResponse(HttpStatusCode.OK, item);
                    }

                }
                user = null;
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Users/{email}/{pass}/")]
        public HttpResponseMessage Get(string email,string pass)
        {
            try
            {
                User user = new User();
                List<User> uList = user.ReadUsers();
                foreach (var item in uList)
                {
                    if (email == item.Email&& pass ==item.Password)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, item);
                    }
                }
                user = null;
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        public HttpResponseMessage Post([FromBody] User user)
        {

            try
            {
                //לאתחל משתמש חדש
                {
                    user.Insert();

                }

                return Request.CreateResponse(HttpStatusCode.Created, user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] User user)
        {
            try
            {
            
                int num = user.Update();

                if (num == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user: " + user + " does not exist");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
           

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}