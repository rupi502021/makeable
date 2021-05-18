using MakeAble.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class Makerspace
    {
        int makerspaceId;
        string user_email;
        string city;
        string street;
        int num_street;
        string phoneNumber;
        string url;
        string days_hours;//ימים ושעות פעילות
        int noPeople;
        int size;
        int price;
        int rating;
        bool aircondition;
        bool accessibility;
        bool serving_coffee;
        bool online_payment;
        bool free_parking;
        string[] professionArr;
        List<string> professions;
        string profession;
        string makerspaceName;
        string descrip;
        List<string> daily_hours;
        int dayonweek;
        string h_start;
        string h_end;
        List<Tool> toolsList;

        public int MakerspaceId { get => makerspaceId; set => makerspaceId = value; }
        public string User_email { get => user_email; set => user_email = value; }
        public string City { get => city; set => city = value; }
        public string Street { get => street; set => street = value; }
        public int Num_street { get => num_street; set => num_street = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Url { get => url; set => url = value; }
        public string Days_hours { get => days_hours; set => days_hours = value; }
        public int NoPeople { get => noPeople; set => noPeople = value; }
        public int Size { get => size; set => size = value; }
        public int Price { get => price; set => price = value; }
        public int Rating { get => rating; set => rating = value; }
        public bool Aircondition { get => aircondition; set => aircondition = value; }
        public bool Accessibility { get => accessibility; set => accessibility = value; }
        public bool Serving_coffee { get => serving_coffee; set => serving_coffee = value; }
        public bool Online_payment { get => online_payment; set => online_payment = value; }
        public bool Free_parking { get => free_parking; set => free_parking = value; }
        public string[] ProfessionArr { get => professionArr; set => professionArr = value; }
        public List<string> Professions { get => professions; set => professions = value; }
        public string Profession { get => profession; set => profession = value; }
        public string MakerspaceName { get => makerspaceName; set => makerspaceName = value; }
        public string Descrip { get => descrip; set => descrip = value; }
        public List<string> Daily_hours { get => daily_hours; set => daily_hours = value; }
        public int Dayonweek { get => dayonweek; set => dayonweek = value; }
        public string H_start { get => h_start; set => h_start = value; }
        public string H_end { get => h_end; set => h_end = value; }
        public List<Tool> ToolsList { get => toolsList; set => toolsList = value; }

        public Makerspace() { }

        public Makerspace(int makerspaceId, string user_email, string city, string street, int num_street, string phoneNumber, string url, string days_hours, int noPeople, int size, int price, int rating, bool aircondition, bool accessibility, bool serving_coffee, bool online_payment, bool free_parking, string[] professionArr, List<string> professions, string profession, string makerspaceName, string descrip, List<string> daily_hours, int dayonweek, string h_start, string h_end, List<Tool> toolsList)
        {
            MakerspaceId = makerspaceId;
            User_email = user_email;
            City = city;
            Street = street;
            Num_street = num_street;
            PhoneNumber = phoneNumber;
            Url = url;
            Days_hours = days_hours;
            NoPeople = noPeople;
            Size = size;
            Price = price;
            Rating = rating;
            Aircondition = aircondition;
            Accessibility = accessibility;
            Serving_coffee = serving_coffee;
            Online_payment = online_payment;
            Free_parking = free_parking;
            ProfessionArr = professionArr;
            Professions = professions;
            Profession = profession;
            MakerspaceName = makerspaceName;
            Descrip = descrip;
            Daily_hours = daily_hours;
            Dayonweek = dayonweek;
            H_start = h_start;
            H_end = h_end;
            ToolsList = toolsList;
        }

        public int InsertMakerspace()
        {
            DBServices dbs = new DBServices();
            int id = dbs.InsertMakerspace(this);
            dbs.InsertMakerspaceOpenningHours(this, id);
            dbs.InsertMakerspaceProf(this, id);
            return id;
        }


        public List<Makerspace> ReadUserMakers(string email)
        {
            DBServices dbs = new DBServices();

            List<Makerspace> m = dbs.getMakerspaceUser(email);
            return m;
        }

        public List<Makerspace> ReadAll()
        {
            DBServices dbs = new DBServices();

            List<Makerspace> m = dbs.getAllMakerspaces();
            return m;
        }
       
        public int Delete()
        {
            DBServices dbs = new DBServices();
            return dbs.DeleteMakerspace(this);
        }
    }
}
