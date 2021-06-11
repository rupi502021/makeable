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
        // GET api/<controller>
        public List<Reservation> Get()
        {
            Reservation request = new Reservation();
            List<Reservation> rList = request.Read();
            return rList;
        }
        
        [HttpGet]
        [Route("api/Reservations/GetApprovedReservation")]
        public List<Reservation> GetApprovedReservation()
        {
            Reservation request = new Reservation();
            List<Reservation> rList = request.ReadApprovedReservation();
            return rList;
        }

        [HttpGet]
        [Route("api/Reservations/GetHistoryReservation")]
        public List<Reservation> GetHistoryReservation()
        {
            Reservation request = new Reservation();
            List<Reservation> rList = request.ReadHistoryReservation();
            return rList;
        }

        [HttpGet]
        [Route("api/Reservations/getReservationByMonth")]
        public List<Reservation> getReservationByMonth()
        {
            Reservation request = new Reservation();
            List<Reservation> rList = request.ReadReservationByMonth();
            return rList;
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

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] Reservation res)
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