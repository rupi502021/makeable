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
        string email_liked;
        string emails_liked;
        List<string> emails_likedList;

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
        public string Email_liked { get => email_liked; set => email_liked = value; }
        public string Emails_liked { get => emails_liked; set => emails_liked = value; }
        public List<string> Emails_likedList { get => emails_likedList; set => emails_likedList = value; }

        public Gallery() { }

        public Gallery(int galleryId, string galleryName, string url, DateTime date, DateTime time, string description, List<string> professions, string[] profArr, string profession, List<string> images, string[] imageArr, string image, bool isActive, string email, string email_liked, string emails_liked, List<string> emails_likedList)
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
            Email_liked = email_liked;
            Emails_liked = emails_liked;
            Emails_likedList = emails_likedList;
        }

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
        public List<Gallery> ReadAllGalleriesAlWithoutUser(string email)
        {
            DBServices dbs = new DBServices();
            List<Gallery> g = dbs.getAllGalleriesAl();
            for (int i = 0; i < g.Count; i++)
            {
                if (email == g[i].Email)
                {
                    g.Remove(g[i]);
                    i--;
                }
            }
            return g;
        }

        public List<Gallery> ReadGalleriesliked(string email)
        {
            DBServices dbs = new DBServices();
            List<Gallery> g = dbs.getGalleriesliked(email);
            
            return g;
        }
        public List<Gallery> ReadPubGalleries(string email)
        {
            DBServices dbs = new DBServices();
            List<Gallery> g = dbs.getPubGalleries(email);
            return g;
        }
        public void Insert()
        {
            DBServices dbs = new DBServices();
            int id = dbs.Insert(this);
            dbs.InsertProffesion_Gallery(this, id);
        }
        public int UpdateGalPublish()
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateGalPublish(this);

        }

        public int UpdateGalSave()
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateGalSave(this);
        }

        public void InsertUserFavGal()
        {
            DBServices dbs = new DBServices();
            dbs.InsertUserFavGal(this);
        }

        public int Delete()
        {
            DBServices dbs = new DBServices();
            return dbs.Delete(this);
        }
    }
}