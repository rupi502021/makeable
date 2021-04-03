using System;
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
        List<string> professions;
        string[] profArr;
        string profession;
        List<string> images;
        string[] imageArr;
        string image;
        bool isActive;
        string email;

        public int GalleryId { get => galleryId; set => galleryId = value; }
        public string GalleryName { get => galleryName; set => galleryName = value; }
        public string Url { get => url; set => url = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Description { get => description; set => description = value; }
        public List<string> Professions { get => professions; set => professions = value; }
        public string[] ProfArr { get => profArr; set => profArr = value; }
        public string Profession { get => profession; set => profession = value; }
        public List<string> Images { get => images; set => images = value; }
        public string[] ImageArr { get => imageArr; set => imageArr = value; }
        public string Image { get => image; set => image = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string Email { get => email; set => email = value; }

        public Gallery() { }

        public Gallery(int galleryId, string galleryName, string url, DateTime date, DateTime time, string description, List<string> professions, string[] profArr, string profession, List<string> images, string[] imageArr, string image, bool isActive, string email)
        {
            GalleryId = galleryId;
            GalleryName = galleryName;
            Url = url;
            Date = date;
            Time = time;
            Description = description;
            Professions = professions;
            ProfArr = profArr;
            Profession = profession;
            Images = images;
            ImageArr = imageArr;
            Image = image;
            IsActive = isActive;
            Email = email;
        }




        //public List<Gallery> ReadFavGal()
        //{
        //    DBServices dbs = new DBServices();
        //    List<Gallery> g = dbs.getFavGallery();
        //    return g;
        //}

        public List<Gallery> Read()
        {
            DBServices dbs = new DBServices();
            List<Gallery> g = dbs.getgallery();
            return g;
        }

        public List<Gallery> ReadAllGalleriesAl()
        {
            DBServices dbs = new DBServices();
            List<Gallery> g = dbs.getAllGalleriesAl();
            return g;
        }

        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.Insert(this);
        }
        public void InsertProffesion_Gallery(Gallery gallery, int id)
        {
            DBServices dbs = new DBServices();
            dbs.InsertProffesion_Gallery(gallery, id);
        }

        //public void InsertToFav()
        //{
        //    DBServices dbs = new DBServices();
        //    dbs.InsertToFav(this);
        //}

        //public int Delete(int id)
        //{
        //    DBServices dbs = new DBServices();
        //    return dbs.Delete(id);
        //}
    }
}