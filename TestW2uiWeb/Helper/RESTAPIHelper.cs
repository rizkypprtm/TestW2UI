using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestW2uiWeb.Model;


namespace TestW2uiWeb.Helper
{
    public class RESTAPIHelper<T>
    {
        public static T Submit(string jsonBody, Method httpMethod, string endpoint, string token)
        {
            string baseUrl = "https://api-sport-events.php6-02.test.voxteneo.com";

            var requests = new RestRequest(httpMethod);
            requests.AddHeader("Content-Type", "application/json");
            requests.Timeout = 300000;

            var request = HttpContext.Current.Request;

            endpoint = String.Format("{0}/{1}", baseUrl, endpoint);


            if (!string.IsNullOrEmpty(token))
            {
                requests.AddHeader("Authorization", "Basic " + token);
            }

            if (!string.IsNullOrEmpty(jsonBody))
            {
                requests.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            }
            var client = new RestClient(endpoint);
            IRestResponse response = client.Execute(requests);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new System.Exception("UnAuthorize");
            }
            var result = JsonConvert.DeserializeObject<T>(response.Content);
            
            return result;
        }
    }
}
