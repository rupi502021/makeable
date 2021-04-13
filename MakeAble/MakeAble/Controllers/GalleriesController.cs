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
        [Route("api/Galleries/{email}/")]
        public HttpResponseMessage ReadPubGalleries(string email)
        {

            try
            {

                Gallery gallery = new Gallery();
                List<Gallery> gList = gallery.ReadPubGalleries(email);
                List<Gallery> gFinal = new List<Gallery>();

                List<string> professions = new List<string>();
                List<string> images = new List<string>();
                var id = 0;

                for (int i = 0; i < gList.Count; i++)
                {
                    
                        if (gList[i].GalleryId == id || id == 0)
                        {

                        }
                        else
                        {
                           
                            List<string> p = new List<string>(professions);
                            List<string> im = new List<string>(images);

                            if (email == gList[i-1].Email)
                            {
                                gList[i - 1].Professions = p;
                                gList[i - 1].Images = im;
                                gFinal.Add(gList[i - 1]);
                            }
                             
                            professions.Clear();
                            images.Clear();
                        }

                        if (professions.Count == 0)
                        {
                            professions.Add(gList[i].Profession);
                        }
                        bool exist = false;
                        foreach (var item in professions)
                        {
                            if (item == gList[i].Profession)
                            {
                                exist = true;
                            }
                        }
                        if (exist != true)
                        {
                            professions.Add(gList[i].Profession);
                        }
                        //תמונה
                        if (images.Count == 0)
                        {
                            images.Add(gList[i].Image);
                        }
                        exist = false;
                        foreach (var item in images)
                        {
                            if (item == gList[i].Image)
                            {
                                exist = true;
                            }
                        }
                        if (exist != true)
                        {
                            images.Add(gList[i].Image);
                        }
                        //בודק איבר אחרון
                        if (gList.Count == i + 1)
                        {
                            gList[i].Professions = professions;
                            gList[i].Images = images;
                            gFinal.Add(gList[i]);

                        }

                        id = gList[i].GalleryId;
                    }
                
                
                return Request.CreateResponse(HttpStatusCode.Created, gFinal);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Galleries/All")]
        public HttpResponseMessage GetAllGalleriesAl()
        {

            try
            {

                Gallery gallery = new Gallery();
                List<Gallery> gList = gallery.ReadAllGalleriesAl();
                List<Gallery> gFinal = new List<Gallery>();

                List<string> professions = new List<string>();
                List<string> images = new List<string>();
                var id = 0;
                for (int i = 0; i < gList.Count; i++)
                {
                    if (gList[i].GalleryId == id || id == 0)
                    {

                    }
                    else
                    {
                        //for (int j = 0; j < professions.Count; j++)
                        //{
                        //    Console.WriteLine(professions[j]);
                        //    (gList[i - 1].Professions).Add(professions[j]);
                        //}
                        List<string> p = new List<string>(professions);
                        List<string> im = new List<string>(images);
                        

                        gList[i - 1].Professions = p;
                        gList[i - 1].Images = im;
                        gFinal.Add(gList[i - 1]);


                        professions.Clear();
                        images.Clear();
                    }

                    if (professions.Count == 0)
                    {
                        professions.Add(gList[i].Profession);
                    }
                    bool exist = false;
                    foreach (var item in professions)
                    {
                        if (item == gList[i].Profession)
                        {
                            exist = true;
                        }
                    }
                    if (exist != true)
                    {
                        professions.Add(gList[i].Profession);
                    }
                    //תמונה
                    if (images.Count == 0)
                    {
                        images.Add(gList[i].Image);
                    }
                    exist = false;
                    foreach (var item in images)
                    {
                        if (item == gList[i].Image)
                        {
                            exist = true;
                        }
                    }
                    if (exist != true)
                    {
                        images.Add(gList[i].Image);
                    }
                    //בודק איבר אחרון
                    if (gList.Count == i + 1)
                    {
                        gList[i].Professions = professions;
                        gList[i].Images = images;
                        gFinal.Add(gList[i]);

                    }

                    id = gList[i].GalleryId;
                }

                return Request.CreateResponse(HttpStatusCode.Created, gFinal);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
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