using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestW2uiWeb.Helper;
using TestW2uiWeb.Model;

namespace TestW2uiWeb.Controllers
{
    public class SportEventController : Controller
    {
        // GET: SportEvent
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getEventList()
        {
            try
            {
                var endpoint = ("api/v1/sport-events?page=1&perPage=500");

                var result = RESTAPIHelper<SportEventModel>.Submit(null, Method.GET, endpoint, Session["token"].ToString());

                string JSONresult = JsonConvert.SerializeObject(result);

                var jObj = JObject.Parse(JSONresult);
                foreach (var item in jObj["data"])
                {
                    item["recid"] = item["id"];
                }
                var newjson = jObj["data"].ToString(Formatting.Indented);

                return new ContentResult
                {
                    Content = newjson,
                    ContentType = "application/json"
                };
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", messege = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult insert(FormCollection Data)
        {
            try
            {
                var endpoint = ("api/v1/sport-events");
                var param = new insertEventParam();
                param.eventDate = Data["eventDate"];
                param.eventType = Data["eventType"];
                param.eventName = Data["eventName"];
                param.organizerId = Data["idEo"];


                var json = JsonConvert.SerializeObject(param);

                var result = RESTAPIHelper<BaseResponseModel>.Submit(json, Method.POST, endpoint, Session["token"].ToString());
                if (result != null)
                {
                    if (result.errors != null)
                    {
                        return Json(new { Result = "error : " + result.message });
                    }
                    else
                    {
                        return Json(new { Result = "OK" });
                    }
                }
                else
                {
                    return Json(new { Result = "OK" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", messege = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult update(FormCollection Data)
        {
            try
            {
                var param = new updateEventParam();
                param.eventDate = Data["eventDate"];
                param.eventType = Data["eventType"];
                param.eventName = Data["eventName"];
                param.organizerId = Data["idEo"];

                var endpoint = ("api/v1/sport-events/"+ Data["id"]);
                
                var json = JsonConvert.SerializeObject(param);

                var result = RESTAPIHelper<BaseResponseModel>.Submit(json, Method.PUT, endpoint, Session["token"].ToString());
                if (result != null)
                {
                    if (result.errors != null)
                    {
                        return Json(new { Result = "error : " + result.message });
                    }
                    else
                    {
                        return Json(new { Result = "OK" });
                    }
                }
                else
                {
                    return Json(new { Result = "OK" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", messege = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult delete(FormCollection Data)
        {
            try
            {
                var endpoint = ("api/v1/sport-events/" + Data["id"]);
                var result = RESTAPIHelper<BaseResponseModel>.Submit("", Method.DELETE, endpoint, Session["token"].ToString());
                if (result != null)
                {
                    if (result.errors != null)
                    {
                        return Json(new { Result = "error : " + result.message });
                    }
                    else
                    {
                        return Json(new { Result = "OK" });
                    }
                }
                else
                {
                    return Json(new { Result = "OK" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", messege = ex.Message });
            }
        }
    }
}