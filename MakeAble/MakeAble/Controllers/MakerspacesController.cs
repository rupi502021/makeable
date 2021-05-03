using MakeAble.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakeAble.Controllers
{
    public class MakerspacesController : ApiController
    {
        [HttpGet]
        [Route("api/Makerspaces/{email}/")]
        public HttpResponseMessage ReadUserMakers(string email)
        {
            try
            {
                Makerspace makerspace = new Makerspace();
                List<Makerspace> mList = makerspace.ReadUserMakers(email);

                return Request.CreateResponse(HttpStatusCode.Created, mList);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }


        [HttpPost]
        [Route("api/Makerspaces")]
        public HttpResponseMessage Post([FromBody] Makerspace makerspace)
        {

            try
            {
                {

                    makerspace.InsertMakerspace();

                }

                return Request.CreateResponse(HttpStatusCode.Created, makerspace);
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