using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MakeAble.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;


        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        public List<User> getusers()
        {

            SqlConnection con = null;
            List<User> uList = new List<User>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    User u = new User();
                    u.UserId = Convert.ToInt32(dr["UserId"]);
                    u.Fname = Convert.ToString(dr["Fname"]);
                    u.Lname = Convert.ToString(dr["Lname"]);
                    u.Email = Convert.ToString(dr["Email"]);
                    u.Password = Convert.ToString(dr["Password"]);
                    u.City = Convert.ToString(dr["City"]);
                    u.Phone = Convert.ToString(dr["Phone"]);
                    u.ProfilePhoto = Convert.ToString(dr["ProfilePhoto"]);
                    u.BirthDay = Convert.ToDateTime(dr["BirthDay"]);
                    u.Description = Convert.ToString(dr["Description"]);
                    u.Have_makerspace = Convert.ToBoolean(dr["Have_makerspace"]);

                    uList.Add(u);

                }

                return uList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
    }
}