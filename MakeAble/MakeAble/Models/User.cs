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
        //DateTime birthDay;
        string description;
        bool have_makerspace;
        List<string> professions;
        string profession;

        public int UserId { get => userId; set => userId = value; }
        public string Email { get => email; set => email = value; }
        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string City { get => city; set => city = value; }
        public string Password { get => password; set => password = value; }
        public string Phone { get => phone; set => phone = value; }
        public string ProfilePhoto { get => profilePhoto; set => profilePhoto = value; }
        //public DateTime BirthDay { get => birthDay; set => birthDay = value; }
        public string Description { get => description; set => description = value; }
        public bool Have_makerspace { get => have_makerspace; set => have_makerspace = value; }
        public List<string> Professions { get => professions; set => professions = value; }
        public string Profession { get => profession; set => profession = value; }

        public User(int userId, string email, string fname, string lname, string city, string password, string phone, string profilePhoto, /*DateTime birthDay,*/ string description, bool have_makerspace, List<string> professions, string profession)
        {
            UserId = userId;
            Email = email;
            Fname = fname;
            Lname = lname;
            City = city;
            Password = password;
            Phone = phone;
            ProfilePhoto = profilePhoto;
            //BirthDay = birthDay;
            Description = description;
            Have_makerspace = have_makerspace;
            Professions = professions;
            Profession = profession;
        }
        public User() { }
        public List<User> ReadUsers()
        {
            DBServices dbs = new DBServices();
            List<User> u = dbs.getusers();
            return u;
        }
        public List<User> ReadUsersPro(string email)
        {
            DBServices dbs = new DBServices();
            List<User> u = dbs.getusersPro(email);
            return u;
        }
        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.Insert(this);
        }
        
        public int Update()
        {
            DBServices dbs = new DBServices();
            return dbs.Update(this);
        }
    }

}