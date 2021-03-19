using MakeAble.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class User
    {
        int userId;
        string email;
        string fname;
        string lname;
        string city;
        string password;
        string phone;
        string profilePhoto;
        DateTime birthDay;
        string description;
        bool have_makerspace;
        string[] profession;

        public User(int userId, string email, string fname, string lname, string city, string password, string phone, string profilePhoto, DateTime birthDay, string description, bool have_makerspace, string[] profession)
        {
            UserId = userId;
            Email = email;
            Fname = fname;
            Lname = lname;
            City = city;
            Password = password;
            Phone = phone;
            ProfilePhoto = profilePhoto;
            BirthDay = birthDay;
            Description = description;
            Have_makerspace = have_makerspace;
            Profession = profession;
        }

        public User() { }

        public int UserId { get => userId; set => userId = value; }
        public string Email { get => email; set => email = value; }
        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string City { get => city; set => city = value; }
        public string Password { get => password; set => password = value; }
        public string Phone { get => phone; set => phone = value; }
        public string ProfilePhoto { get => profilePhoto; set => profilePhoto = value; }
        public DateTime BirthDay { get => birthDay; set => birthDay = value; }
        public string Description { get => description; set => description = value; }
        public bool Have_makerspace { get => have_makerspace; set => have_makerspace = value; }
        public string[] Profession { get => profession; set => profession = value; }


        public List<User> ReadUsers()
        {
            DBServices dbs = new DBServices();
            List<User> u = dbs.getusers();
            return u;
        }
    }

}