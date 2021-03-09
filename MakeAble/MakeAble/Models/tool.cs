using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeAble.Models
{
    public class Tool
    {
        int toolId;
        string model;
        string brand;
        bool qualifications;
        string description;

        public Tool(int toolId, string model, string brand, bool qualifications, string description)
        {
            this.toolId = toolId;
            this.model = model;
            this.brand = brand;
            this.qualifications = qualifications;
            this.description = description;
        }

        public Tool() { }

        public int ToolId { get => toolId; set => toolId = value; }
        public string Model { get => model; set => model = value; }
        public string Brand { get => brand; set => brand = value; }
        public bool Qualifications { get => qualifications; set => qualifications = value; }
        public string Description { get => description; set => description = value; }
    }
}