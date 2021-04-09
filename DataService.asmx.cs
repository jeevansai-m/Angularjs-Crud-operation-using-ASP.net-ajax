using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace Task
{
    /// <summary>
    /// Summary description for DataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
   [System.Web.Script.Services.ScriptService]
    public class DataService : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetRegister()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<Register> Registers = new List<Register>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select* from TblRegister", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Register Register = new Register();
                    Register.Id = Convert.ToInt32(rdr["Id"]);
                    Register.Name = rdr["Name"].ToString();
                    Register.Surname = rdr["Surname"].ToString();
                    Register.Pnum = Convert.ToInt64(rdr["Pnum"]);
                    Register.Age = Convert.ToInt32(rdr["Age"]);
                    Register.Idtype = rdr["Idtype"].ToString();
                    Register.Proof = rdr["Proof"].ToString();
                    Register.Gender = rdr["Gender"].ToString();
                    Registers.Add(Register);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Registers));
        }
        [WebMethod]
        public void InsertRegister(Register data)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("InsertUpdateRegister_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", data.Id);
                cmd.Parameters.AddWithValue("@Name", data.Name);
                cmd.Parameters.AddWithValue("@surname", data.Surname);
                cmd.Parameters.AddWithValue("@Pnum", data.Pnum);
                cmd.Parameters.AddWithValue("@Age", data.Age);
                cmd.Parameters.AddWithValue("@Idtype", data.Idtype);
                cmd.Parameters.AddWithValue("@Proof", data.Proof);
                cmd.Parameters.AddWithValue("@Gender", data.Gender);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        [WebMethod]
        public int Delete(int ID)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteRegister_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ID);
                i = cmd.ExecuteNonQuery();
            }
            return i; 
        }
        [WebMethod]
        public void On_Click_EditRegister(int ID)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<Register> Registers = new List<Register>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("EditRegister_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Register Register = new Register();
                    Register.Id = Convert.ToInt32(rdr["Id"]);
                    Register.Name = rdr["Name"].ToString();
                    Register.Surname = rdr["Surname"].ToString();
                    Register.Pnum = Convert.ToInt64(rdr["Pnum"]);
                    Register.Age = Convert.ToInt32(rdr["Age"]);
                    Register.Idtype = rdr["Idtype"].ToString();
                    Register.Proof = rdr["Proof"].ToString();
                    Register.Gender = rdr["Gender"].ToString();
                    Registers.Add(Register);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Registers));
        }
    }
}
