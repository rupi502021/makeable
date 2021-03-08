using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class Gallery
    {
        int galleryId;
        string galleryName;
        string url;
        DateTime date;
        DateTime time;
        string description;
        string[] profession;
        //לא הוספתי 'כמות לייקים' ו'כמות תגובות' פה - האם צריך?

        public Gallery(int galleryId, string galleryName, string url, DateTime date, DateTime time, string description, string[] profession)
        {
            GalleryId = galleryId;
            GalleryName = galleryName;
            Url = url;
            Date = date;
            Time = time;
            Description = description;
            Profession = profession;
        }

        public Gallery() { }

        public int GalleryId { get => galleryId; set => galleryId = value; }
        public string GalleryName { get => galleryName; set => galleryName = value; }
        public string Url { get => url; set => url = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Description { get => description; set => description = value; }
        public string[] Profession { get => profession; set => profession = value; }
    }
}