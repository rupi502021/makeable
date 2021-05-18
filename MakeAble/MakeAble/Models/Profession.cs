using MakeAble.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class Profession
    {
        int professionId;
        string professionName;

        public Profession(int professionId, string professionName)
        {
            ProfessionId = professionId;
            ProfessionName = professionName;
        }
        
        public int ProfessionId { get => professionId; set => professionId = value; }
        public string ProfessionName { get => professionName; set => professionName = value; }

        public Profession() { }

        public List<Profession> Read()
        {
            DBServices dbs = new DBServices();
            List<Profession> p = dbs.getprofession();
            return p;
        }
    }
}