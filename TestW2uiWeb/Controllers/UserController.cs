using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestW2uiWeb.Model;
using TestW2uiWeb.Helper;
using System.Web.Security;

namespace TestW2uiWeb.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult UserDetail()
        {
            return View();
        }
        
        // GET: User
        public ActionResult SignOn(FormCollection form)
        {
            string baseUrl = "https://api-sport-events.php6-02.test.voxteneo.com";


            //form["Username"],
            //form["Password"]

            var requests = new RestRequest(Method.POST);
            requests.AddHeader("Content-Type", "application/json");
            requests.Timeout = 300000;
            requests.AddJsonBody(
                new
                {
                    email = form["Username"],
                    password = form["Password"],
                });

            var request = HttpContext.Request;

            var endpoint = String.Format("{0}/{1}", baseUrl, "api/v1/users/login");
            
            var client = new RestClient(endpoint);
            IRestResponse response = client.Execute(requests);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new System.Exception("UnAuthorize");
            }
            var result = JsonConvert.DeserializeObject<dynamic>(response.Content);
            string token = result.token;
            if (token != null)
            {
                Session["email"] = result.email;
                Session["token"] = token;
                Session["id"] = result.id;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Logout(FormCollection form)
        {
            Session["token"] = "";
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult getUser(int Id)
        {
            try
            {
                var endpoint = ("api/v1/users/"+Id);
                
                var result = RESTAPIHelper<UserModel>.Submit(null, Method.GET, endpoint , Session["token"].ToString());
                //var listProduct = new List<ProductEntities>();
                
                string JSONresult = JsonConvert.SerializeObject(result);

                return new ContentResult
                {
                    Content = JSONresult,
                    ContentType = "application/json"
                };
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", messege = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveUser(FormCollection Data)
        {
            try
            {
                var endpoint = ("api/v1/users");
                var param = new CreateUserModelParameters();
                param.firstName = Data["firstName"];
                param.lastName = Data["lastName"];
                param.email = Data["email"];
                param.password = Data["password"];
                param.repeatPassword = Data["repeatPassword"];

                var json = JsonConvert.SerializeObject(param);

                var result = RESTAPIHelper<BaseResponseModel>.Submit(json, Method.POST, endpoint, Session["token"].ToString());

                if(result.errors != null)
                {
                    return Json(new { Result = "error : " + result.message });
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
        public ActionResult UpdateUser(FormCollection Data)
        {
            try
            {
                string Id = Data["id"];
                
                var endpoint = ("api/v1/users/" + Id);
                var param = new UpdateUserModelParameters();
                param.firstName = Data["firstName"];
                param.lastName = Data["lastName"];
                param.email = Data["email"];

                var json = JsonConvert.SerializeObject(param);

                var result = RESTAPIHelper<BaseResponseModel>.Submit(json, Method.PUT, endpoint, Session["token"].ToString());

                if (result.errors != null)
                {
                    return Json(new { Result = "error : " + result.message });
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
        public ActionResult ChangePassword(FormCollection Data)
        {
            try
            {
                var param = new ChangePasswordModelParameters();
                string Id = Data["id"];
                

                var endpoint = ("api/v1/users/"+Id+"/password");

                param.oldPassword = Data["oldPassword"];
                param.newPassword = Data["newPassword"];
                param.repeatPassword = Data["repeatPassword"];


                var json = JsonConvert.SerializeObject(param);

                var result = RESTAPIHelper<BaseResponseModel>.Submit(json, Method.PUT, endpoint, Session["token"].ToString());

                if (result.errors != null)
                {
                    return Json(new { Result = "error : " + result.message });
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