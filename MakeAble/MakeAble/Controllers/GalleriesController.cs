using MakeAble.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakeAble.Controllers
{
    public class GalleriesController : ApiController
    {
        // GET api/<controller>
        //public List<Gallery> Get()
        //{
        //    //Gallery gallery = new Gallery();
        //    //List<Gallery> gList = gallery.Read();
        //    //return gList;
        //    return;
        //}

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Gallery gallery)
        {
            try
            {
                gallery.Insert();
                return Request.CreateResponse(HttpStatusCode.Created, gallery);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
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