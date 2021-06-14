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
        string user_email;
        int makerspaceId;

        int month1;
        int month2;
        int month3;
        int month4;
        int month5;
        int month6;
        int month7;
        int month8;
        int month9;
        int month10;
        int month11;
        int month12;

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
        public string User_email { get => user_email; set => user_email = value; }
        public int MakerspaceId { get => makerspaceId; set => makerspaceId = value; }
        public int Month1 { get => month1; set => month1 = value; }
        public int Month2 { get => month2; set => month2 = value; }
        public int Month3 { get => month3; set => month3 = value; }
        public int Month4 { get => month4; set => month4 = value; }
        public int Month5 { get => month5; set => month5 = value; }
        public int Month6 { get => month6; set => month6 = value; }
        public int Month7 { get => month7; set => month7 = value; }
        public int Month8 { get => month8; set => month8 = value; }
        public int Month9 { get => month9; set => month9 = value; }
        public int Month10 { get => month10; set => month10 = value; }
        public int Month11 { get => month11; set => month11 = value; }
        public int Month12 { get => month12; set => month12 = value; }

        public Reservation() { }

        public Reservation(int reservationId, DateTime date, DateTime startTime_res, DateTime endTime_res, string description, DateTime startTime_req, DateTime endTime_req, double span, bool statusApproved, string userName, string user_email, int makerspaceId, int month1, int month2, int month3, int month4, int month5, int month6, int month7, int month8, int month9, int month10, int month11, int month12)
        {
            ReservationId = reservationId;
            Date = date;
            StartTime_res = startTime_res;
            EndTime_res = endTime_res;
            Description = description;
            StartTime_req = startTime_req;
            EndTime_req = endTime_req;
            Span = span;
            StatusApproved = statusApproved;
            UserName = userName;
            User_email = user_email;
            MakerspaceId = makerspaceId;
            Month1 = month1;
            Month2 = month2;
            Month3 = month3;
            Month4 = month4;
            Month5 = month5;
            Month6 = month6;
            Month7 = month7;
            Month8 = month8;
            Month9 = month9;
            Month10 = month10;
            Month11 = month11;
            Month12 = month12;
        }

        public List<Reservation> Read(int id)
        {
            DBServices dbs = new DBServices();
            return dbs.getRequest(id);
        }
        
        public List<Reservation> ReadApprovedReservation(int id)
        {
            DBServices dbs = new DBServices();
            return dbs.getApprovedReservation(id);
        }

        public List<Reservation> ReadHistoryReservation(int id)
        {
            DBServices dbs = new DBServices();
            return dbs.getHistoryReservation(id);
        }

        public List<Reservation> ReadReservationByMonth(int id)
        {
            DBServices dbs = new DBServices();
            return dbs.getReservationByMonth(id);
        }

        public int Delete()
        {
            DBServices dbs = new DBServices();
            return dbs.DeleteRQ(this);
        }

        public int Update()
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateRES(this);
        }
        public int InsertReservation()
        {
            DBServices dbs = new DBServices();
            return dbs.InsertReservation(this);
        }
        
    }
}