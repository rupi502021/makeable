using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
namespace MakeAble.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBServices()
        {
            //
            // TODO: Add constructor logic here
            //
        }
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
        public int Insert(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to connect to DB", ex);
            }

            String cStr = BuildInsertCommand(user);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to add a new user, Try again!", ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(User user)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
            user.Email, user.Fname, user.Lname,
            user.City, user.Password, user.Phone, user.ProfilePhoto, user.BirthDay, user.Description, user.Have_makerspace);

            String prefix = "INSERT INTO Users " + "([Email],[Fname],[Lname],[City],[Password],[Phone],[ProfilePhoto],[BirthDay],[Description],[Have_makerspace])";

            command = prefix + sb.ToString();

            if (user.Profession.Length > 0 && user.Profession != null)
            {
                for (int i = 0; i < user.Profession.Length; i++)
                {
                    sb = new StringBuilder();
                    sb.AppendFormat("Values('{0}','{1}')", user.Profession[i], user.Email);
                    prefix = "INSERT INTO Users_Professions " + "([ProfessionName], [Email])";

                    command += prefix + sb.ToString();
                }
            }
            return command;
        }

        public int Update(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildUpdateCommand(user);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
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
                    // close the db connection
                    con.Close();
                }
            }
        }

        private String BuildUpdateCommand(User user)
        {
            String command;
            if (user.ProfilePhoto != null)
            {
                command = "UPDATE Users SET Fname='" + user.Fname + "', Lname='" + user.Lname + "'," +
                   "City='" + user.City + "', Password='" + user.Password + "',Phone='" + user.Phone + "', ProfilePhoto='" + user.ProfilePhoto +
                   "', BirthDay='" + user.BirthDay + "', Description='" + user.Description + "', Have_makerspace='" + user.Have_makerspace +
                   "' " + "WHERE Email='" + user.Email + "'";
            }
            else
            {
                command = "UPDATE Users SET Fname='" + user.Fname + "', Lname='" + user.Lname + "'," +
                   "City='" + user.City + "', Password='" + user.Password + "',Phone='" + user.Phone +
                   "', BirthDay='" + user.BirthDay + "', Description='" + user.Description + "', Have_makerspace='" + user.Have_makerspace +
                   "' " + "WHERE Email='" + user.Email + "'";
            }
            return command;
        }

        public int Insert(Gallery gallery)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to connect to DB", ex);
            }

            String cStr = BuildInsertCommand(gallery);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to add a new gallery, Try again!", ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private String BuildInsertCommand(Gallery gallery)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}')", gallery.GalleryName, ((DateTime)gallery.Time).ToString("t"), ((DateTime)gallery.Date).ToString("u"), gallery.Description, gallery.Email);

            String prefix = "INSERT INTO Gallery " + "([GalleryName],[UploadTime],[UploadDate],[Description],[UserEmail])";

            command = prefix + sb.ToString();
            return command;
        }

        public List<Gallery> getgallery()
        {
            SqlConnection con = null;
            List<Gallery> gList = new List<Gallery>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Gallery";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Gallery g = new Gallery();
                   
                    g.GalleryId = Convert.ToInt32(dr["GalleryId"]);
                    g.GalleryName = Convert.ToString(dr["GalleryName"]);
                    g.Url = Convert.ToString(dr["Url"]);
                    g.Date = Convert.ToDateTime(dr["UploadDate"]);
                    g.Time = Convert.ToDateTime(dr["UploadTime"]);

                    g.Description = Convert.ToString(dr["Description"]);

                    gList.Add(g);
                        
                }
                return gList;
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

        public int InsertProffesion_Gallery(Gallery gallery,int id)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to connect to DB", ex);
            }

            String cStr = BuildInsertProfCommand(gallery, id);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to add a new gallery, Try again!", ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private String BuildInsertProfCommand(Gallery gallery,int id)
        {
            String command = "";
            if (gallery.Profession != null && gallery.Profession.Length > 0  )
            {
                
                for (int i = 0; i < gallery.Profession.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Values('{0}','{1}')", id, gallery.Profession[i]);
                    String prefix = "INSERT INTO Professions_Gallery" + "([GalleryId],[ProfessionName])";

                    command += prefix + sb.ToString();
                }
            }
           
            return command;
        }
    }
}