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

        //[HttpGet]
        //[Route("api/Galleries/fav")]
        //public List<Gallery> GetFav()
        //{
        //    Gallery gallery = new Gallery();
        //    List<Gallery> gList = gallery.ReadFavGal();
        //    return gList;
        //}
        public List<Gallery> Get()
        {
                Gallery gallery = new Gallery();
                List<Gallery> gList = gallery.Read();
                return gList;
        }
        [HttpGet]
        [Route("api/Users/{email}/")]
        public List<Gallery> GetAllGalleriesAl()
        {
            Gallery gallery = new Gallery();
            List<Gallery> gList = gallery.ReadAllGalleriesAl();
            return gList;
        }

        // POST api/<controller>

        //[HttpPost]
        //[Route("api/Galleries/fav")]
        //public void PostFav([FromBody] Gallery gallery)
        //{
        //        gallery.InsertToFav();
        //}

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
        //public HttpResponseMessage Delete(int id)
        //{
        //    Gallery g = new Gallery();
        //    int num = g.Delete(id);
        //    List<Gallery> gList = g.ReadFavGal();
        //    if (num == 0)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "id: " + id + " does not exist");
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, gList);
        //    }
        //}
    }
}