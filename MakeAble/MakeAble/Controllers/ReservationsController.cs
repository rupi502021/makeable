using MakeAble.Models;
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
        [HttpGet]
        [Route("api/Reservations/makerspaceid/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Reservation request = new Reservation();
                List<Reservation> rList = request.Read(id);
               
                return Request.CreateResponse(HttpStatusCode.Created, rList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }

        }
        
        [HttpGet]
        [Route("api/Reservations/GetApprovedReservation/makerspaceid/{id}")]
        public HttpResponseMessage GetApprovedReservation(int id)
        {
            try
            {
                Reservation request = new Reservation();
                List<Reservation> rList = request.ReadApprovedReservation(id);

                return Request.CreateResponse(HttpStatusCode.Created, rList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/Reservations/GetHistoryReservation/makerspaceid/{id}")]
        public HttpResponseMessage GetHistoryReservation(int id)
        {
            try
            {
                Reservation request = new Reservation();
                List<Reservation> rList = request.ReadHistoryReservation(id);

                return Request.CreateResponse(HttpStatusCode.Created, rList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    

        [HttpGet]
        [Route("api/Reservations/getReservationByMonth/makerspaceid/{id}")]
        public HttpResponseMessage getReservationByMonth(int id)
        {
            try
            {
                Reservation request = new Reservation();
                List<Reservation> rList = request.ReadReservationByMonth(id);

                return Request.CreateResponse(HttpStatusCode.Created, rList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
           
        }

        [HttpPost]
        [Route("api/Reservations")]
        public HttpResponseMessage Post([FromBody] Reservation reservation)
        {
            try
            {
                reservation.InsertReservation();

                return Request.CreateResponse(HttpStatusCode.Created, reservation);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPut]
        [Route("api/Reservations")]
        public HttpResponseMessage Put(Reservation res)
        {
            try
            {
                int num = res.Update();

                if (num == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "reservation: " + res + " does not exist");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Reservations/deleteRQ/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
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
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}