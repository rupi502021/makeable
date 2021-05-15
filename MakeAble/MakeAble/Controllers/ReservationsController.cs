﻿using MakeAble.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakeAble.Controllers
{
    public class ReservationsController : ApiController
    {
        // GET api/<controller>
        public List<Reservation> Get()
        {
            Reservation request = new Reservation();
            List<Reservation> rList = request.Read();
            return rList;
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Reservations/deleteRQ/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            Reservation r = new Reservation();
            r.ReservationId = id;

            int num = r.Delete();

            if (num == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "id: " + id + " does not exist");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, r);
            }
        }
    }
}