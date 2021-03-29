﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MakeAble.Models.DAL;

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
        string[] images;

        public Gallery(int galleryId, string galleryName, string url, DateTime date, DateTime time, string description, string[] profession, string[] images)
        {
            this.GalleryId = galleryId;
            this.GalleryName = galleryName;
            this.Url = url;
            this.Date = date;
            this.Time = time;
            this.Description = description;
            this.Profession = profession;
            this.Images = images;
        }

        public Gallery() { }

        public int GalleryId { get => galleryId; set => galleryId = value; }
        public string GalleryName { get => galleryName; set => galleryName = value; }
        public string Url { get => url; set => url = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Description { get => description; set => description = value; }
        public string[] Profession { get => profession; set => profession = value; }
        public string[] Images { get => images; set => images = value; }

        public void Read() {}
        public void Insert() { }
    }
}