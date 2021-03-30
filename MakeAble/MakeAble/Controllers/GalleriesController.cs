using MakeAble.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakeAble.Controllers
{
    public class GalleriesController : ApiController
    {
        // GET api/<controller>
        public List<Gallery> Get()
        {
            Gallery gallery = new Gallery();
            List<Gallery> gList = gallery.Read();
            return gList;
        }

        
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Gallery gallery)
        {
            try
            {
                gallery.Insert();
                List<Gallery> gList = gallery.Read();
                for (int i = gList.Count; i <= gList.Count; i++)
                {
                    gList[i-1].InsertProffesion_Gallery(gallery, gList[i - 1].GalleryId);
                }
                return Request.CreateResponse(HttpStatusCode.Created, gallery);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}