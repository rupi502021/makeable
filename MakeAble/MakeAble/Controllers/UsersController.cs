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
        [Route("api/Users/{email}/{pass}")]
        public HttpResponseMessage Get(string email,string pass)
        {
            try
            {
                User user = new User();
                List<User> uList = user.ReadUsers();
                foreach (var item in uList)
                {
                    if (email == item.Email && pass==item.Password)
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

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}