using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakeAble.Models
{
    public class ProfessionsController : ApiController
    {
        [HttpGet]
        [Route("api/Professions")]
        public List<Profession> Get()
        {
            Profession profession = new Profession();
            List<Profession> pList = profession.Read();
            return pList;
        }

    }
}