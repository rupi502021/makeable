using MakeAble.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class Reservation
    {
        int reservationId;
        DateTime date;
        DateTime startTime_res;
        DateTime endTime_res;
        string description;
        DateTime startTime_req;
        DateTime endTime_req;
        double span;
        bool statusApproved;
        string userName;

        public Reservation() { }

        public Reservation(int reservationId, DateTime date, DateTime startTime_res, DateTime endTime_res, string description, DateTime startTime_req, DateTime endTime_req, double span, bool statusApproved, string userName)
        {
            this.reservationId = reservationId;
            this.date = date;
            this.startTime_res = startTime_res;
            this.endTime_res = endTime_res;
            this.description = description;
            this.startTime_req = startTime_req;
            this.endTime_req = endTime_req;
            this.span = span;
            this.statusApproved = statusApproved;
            this.userName = userName;
        }

        public int ReservationId { get => reservationId; set => reservationId = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime StartTime_res { get => startTime_res; set => startTime_res = value; }
        public DateTime EndTime_res { get => endTime_res; set => endTime_res = value; }
        public string Description { get => description; set => description = value; }
        public DateTime StartTime_req { get => startTime_req; set => startTime_req = value; }
        public DateTime EndTime_req { get => endTime_req; set => endTime_req = value; }
        public double Span { get => span; set => span = value; }
        public bool StatusApproved { get => statusApproved; set => statusApproved = value; }
        public string UserName { get => userName; set => userName = value; }

        public List<Reservation> Read()
        {
            DBServices dbs = new DBServices();
            return dbs.getRequest();
        }
    }
}