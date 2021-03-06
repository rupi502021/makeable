﻿using MakeAble.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace MakeAble.Controllers
{
    public class ToolsController : ApiController
    {
        [HttpGet]
        [Route("api/Tools")]
        public HttpResponseMessage ReadTools()
        {
            try
            {
                Tool tool = new Tool();
                List<Tool> tList = tool.Read();

                return Request.CreateResponse(HttpStatusCode.Created, tList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
      

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("api/Tools")]
        public HttpResponseMessage Post([FromBody] Tool tool)
        {

            try
            {

                tool.InsertTool();

                return Request.CreateResponse(HttpStatusCode.Created, tool);
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