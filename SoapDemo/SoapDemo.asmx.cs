using SoapDemo.Models;
using System;


using System.Web;
using System.Web.Services;

using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SoapDemo
{
    /// <summary>
    /// Summary description for SoapDemo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoapDemo : System.Web.Services.WebService
    {
      

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public  ResponseModel<string> login(string email, string password)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            if (email != null)
            {
                using (SqlConnection conn = new SqlConnection(@"Server=CODERSIGN\SQLEXPRESS01;Database=IdentityDemo;User Id=freecode;Password=freespot;"))
                {
                    SqlCommand cmd = new SqlCommand("sp_loginUser",conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = email;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = password;
                    

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        response.Data = JsonConvert.SerializeObject(dt);
                        response.resultCode = 200;
                    }
                    else
                    {
                        response.message = "User Not Found!";
                        response.resultCode = 500;
                    }


                }
            }
            return response;
        }

    }
}
