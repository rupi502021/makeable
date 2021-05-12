using MakeAble.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class Tool
    {
        int toolId;
        string toolName;
        int makerspaceId;
        string url_photo;
        int quantity;
        string model;
        string brand;
        bool qualifications;
        string description;


        public Tool() { }

        public Tool(int toolId, string toolName, int makerspaceId, string url_photo, int quantity, string model, string brand, bool qualifications, string description)
        {
            ToolId = toolId;
            ToolName = toolName;
            MakerspaceId = makerspaceId;
            Url_photo = url_photo;
            Quantity = quantity;
            Model = model;
            Brand = brand;
            Qualifications = qualifications;
            Description = description;
        }

        public int ToolId { get => toolId; set => toolId = value; }
        public string ToolName { get => toolName; set => toolName = value; }
        public int MakerspaceId { get => makerspaceId; set => makerspaceId = value; }
        public string Url_photo { get => url_photo; set => url_photo = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Model { get => model; set => model = value; }
        public string Brand { get => brand; set => brand = value; }
        public bool Qualifications { get => qualifications; set => qualifications = value; }
        public string Description { get => description; set => description = value; }


        public void InsertTool()
        {
            DBServices dbs = new DBServices();
            int ToolId=dbs.InsertTool(this);
            dbs.InsertTool_Makerspace(this, ToolId);
        }
    }
}