using MakeAble.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakeAble.Controllers
{
    public class MakerspacesController : ApiController
    {
        [HttpGet]
        [Route("api/Makerspaces/{email}/")]
        public HttpResponseMessage ReadUserMakers(string email)
        {
            try
            {
                Makerspace makerspace = new Makerspace();
                List<Makerspace> mList = makerspace.ReadUserMakers(email);

                List<Makerspace> mFinal = new List<Makerspace>();

                List<string> professions = new List<string>();
                List<string> dailyhours = new List<string>();

                var id = 0;
                for (int i = 0; i < mList.Count; i++)
                {

                    if (mList[i].MakerspaceId == id || id == 0)
                    {

                    }
                    else
                    {

                        List<string> p = new List<string>(professions);
                        List<string> d = new List<string>(dailyhours);

                        if (email == mList[i - 1].User_email)
                        {
                            mList[i - 1].Professions = p;
                            mList[i - 1].Daily_hours = d;

                            mFinal.Add(mList[i - 1]);
                        }

                        professions.Clear();
                        dailyhours.Clear();

                    }
                    //תחומי עיסוק
                    if (professions.Count == 0)
                    {
                        professions.Add(mList[i].Profession);
                    }
                    bool exist = false;
                    foreach (var item in professions)
                    {
                        if (item == mList[i].Profession)
                        {
                            exist = true;
                        }
                    }
                    if (exist != true)
                    {
                        professions.Add(mList[i].Profession);
                    }
                    //שעות פעילות
                    String day = "";
                    switch (mList[i].Dayonweek)
                    {
                        case 1:
                            day = "יום ראשון";
                            break;
                        case 2:
                            day = "יום שני";
                            break;
                        case 3:
                            day = "יום שלישי";
                            break;
                        case 4:
                            day = "יום רביעי";
                            break;
                        case 5:
                            day = "יום חמישי";
                            break;
                        case 6:
                            day = "יום שישי";
                            break;
                        case 7:
                            day = "יום שבת";
                            break;
                    }

                    if (dailyhours.Count == 0)
                    {
                        string str = day + " " + mList[i].H_start + " - " + mList[i].H_end;
                        dailyhours.Add(str);
                    }
                    exist = false;
                    foreach (var item in dailyhours)
                    {
                        string str = day + " " + mList[i].H_start + " - " + mList[i].H_end;
                        if (item == str)
                        {
                            exist = true;
                        }
                    }
                    if (exist != true)
                    {
                        string str = day + " " + mList[i].H_start + " - " + mList[i].H_end;
                        dailyhours.Add(str);
                    }
                    //בודק איבר אחרון
                    if (mList.Count == i + 1)
                    {
                        mList[i].Professions = professions;
                        mList[i].Daily_hours = dailyhours;

                        mFinal.Add(mList[i]);

                    }

                    id = mList[i].MakerspaceId;
                }



                return Request.CreateResponse(HttpStatusCode.Created, mFinal);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }


        [HttpPost]
        [Route("api/Makerspaces")]
        public HttpResponseMessage Post([FromBody] Makerspace makerspace)
        {

            try
            {

                int id = makerspace.InsertMakerspace();

                return Request.CreateResponse(HttpStatusCode.Created, id);
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