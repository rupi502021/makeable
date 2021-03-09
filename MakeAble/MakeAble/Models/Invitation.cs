using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class Invitation
    {
        int invitationId;
        DateTime date;
        DateTime startTime_inv;
        DateTime endTime_inv;
        string description;
        DateTime startTime_req;
        DateTime endTime_req;
        int span;

        public Invitation(int invitationId, DateTime date, DateTime startTime_inv, DateTime endTime_inv, string description, DateTime startTime_req, DateTime endTime_req, int span)
        {
            this.invitationId = invitationId;
            this.date = date;
            this.startTime_inv = startTime_inv;
            this.endTime_inv = endTime_inv;
            this.description = description;
            this.startTime_req = startTime_req;
            this.endTime_req = endTime_req;
            this.span = span;
        }

        public Invitation() { }

        public int InvitationId { get => invitationId; set => invitationId = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime StartTime_inv { get => startTime_inv; set => startTime_inv = value; }
        public DateTime EndTime_inv { get => endTime_inv; set => endTime_inv = value; }
        public string Description { get => description; set => description = value; }
        public DateTime StartTime_req { get => startTime_req; set => startTime_req = value; }
        public DateTime EndTime_req { get => endTime_req; set => endTime_req = value; }
        public int Span { get => span; set => span = value; }
    }
}