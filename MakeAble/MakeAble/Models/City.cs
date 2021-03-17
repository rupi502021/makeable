using MakeAble.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class City
    {
        string name;
        int id;

        public City(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }
       
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }

        public City() { }

        //public List<City> Read()
        //{
        //    DBServices dbs = new DBServices();
        //    List<City> c = dbs.getcities();
        //    return c;
        //}
    }
}