using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestW2uiWeb.Controllers
{
    public class BaseController : Controller
    {

        public class SqlParam_
        {

            public string Name { get; set; }
            public string Value { get; set; }

        }

        public class Jsons
        {
            public int ID { get; set; }
        }

        public static DataTable GetDataTable(string ProcName, string Param = "")
        {
            DataTable dtTable = new DataTable();
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = ConfigurationManager.ConnectionStrings["TestConnection"].ToString();
            if (cnn.State == System.Data.ConnectionState.Open) { cnn.Close(); } else { cnn.Open(); }
            SqlCommand sCommand = new SqlCommand(ProcName, cnn);
            sCommand.CommandType = CommandType.StoredProcedure;
            if (Param != "")
            {
                var list = JsonConvert.DeserializeObject<List<SqlParam_>>(Param);
                foreach (var item in list)
                {
                    sCommand.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
            SqlDataReader dtReader = sCommand.ExecuteReader();
            dtTable.Load(dtReader);
            cnn.Close();

            return dtTable;

        }
    }
}