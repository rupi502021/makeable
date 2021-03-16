using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class Message
    {
        int messageId;
        DateTime date;
        DateTime time;
        string description;
        string urlPhoto;
        string urlVideo;
        
        public Message(int messageId, DateTime date, DateTime time, string description, string urlPhoto, string urlVideo)
        {
            this.messageId = messageId;
            this.date = date;
            this.time = time;
            this.description = description;
            this.urlPhoto = urlPhoto;
            this.urlVideo = urlVideo;
        }

        public Message() { }

        public int MessageId { get => messageId; set => messageId = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Description { get => description; set => description = value; }
        public string UrlPhoto { get => urlPhoto; set => urlPhoto = value; }
        public string UrlVideo { get => urlVideo; set => urlVideo = value; }
    }
}