using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using System.Web.Razor.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public List<Profession> getprofession()
        {

            SqlConnection con = null;
            List<Profession> pList = new List<Profession>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Professions";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Profession p = new Profession();
                    p.ProfessionId = Convert.ToInt32(dr["ProfessionId"]);
                    p.ProfessionName = Convert.ToString(dr["ProfessionName"]);

                    pList.Add(p);
                }

                return pList;
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
        public List<User> getusersPro(string email)
        {

            SqlConnection con = null;
            List<User> uList = new List<User>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "  SELECT * FROM Users inner join Users_Professions on Users_Professions.Email=Users.Email where Users.Email='" + email + "'";
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
                    u.Profession = Convert.ToString(dr["ProfessionName"]);

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
            try
            {
                if (user.Professions.Count > 0)
                {
                    for (int i = 0; i < user.Professions.Count; i++)
                    {
                        sb = new StringBuilder();
                        sb.AppendFormat("Values('{0}','{1}')", user.Professions[i], user.Email);
                        prefix = ";INSERT INTO Users_Professions " + "([ProfessionName], [Email])";

                        command += prefix + sb.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("צריך לבחור לפחות תחום עיסוק אחד");

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
            String prefix;
            StringBuilder sb = new StringBuilder();
            String delete = "; delete from Users_Professions where Email = '" + user.Email + "'";
            if (user.ProfilePhoto != null)
            {
                command = "UPDATE Users SET Fname='" + user.Fname + "', Lname='" + user.Lname + "'," +
                   "City='" + user.City + "', Password='" + user.Password + "',Phone='" + user.Phone + "', ProfilePhoto='" + user.ProfilePhoto +
                   "', BirthDay='" + user.BirthDay + "', Description='" + user.Description + "', Have_makerspace='" + user.Have_makerspace +
                   "' " + "WHERE Email='" + user.Email + "'";

                try
                {
                    if (user.Professions.Count > 0)
                    {
                        command += delete;
                        for (int i = 0; i < user.Professions.Count; i++)
                        {
                            sb = new StringBuilder();
                            sb.AppendFormat("Values('{0}','{1}')", user.Professions[i], user.Email);
                            prefix = "; INSERT INTO Users_Professions " + "([ProfessionName], [Email])";

                            command += prefix + sb.ToString();
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("צריך לבחור לפחות תחום עיסוק אחד");

                }

            }
            else
            {
                command = "UPDATE Users SET Fname='" + user.Fname + "', Lname='" + user.Lname + "'," +
                   "City='" + user.City + "', Password='" + user.Password + "',Phone='" + user.Phone +
                   "', BirthDay='" + user.BirthDay + "', Description='" + user.Description + "', Have_makerspace='" + user.Have_makerspace +
                   "' " + "WHERE Email='" + user.Email + "'";
                try
                {
                    if (user.Professions.Count > 0)
                    {
                        command += delete;
                        for (int i = 0; i < user.Professions.Count; i++)
                        {
                            sb = new StringBuilder();
                            sb.AppendFormat("Values('{0}','{1}')", user.Professions[i], user.Email);
                            prefix = ";INSERT INTO Users_Professions " + "([ProfessionName], [Email])";

                            command += prefix + sb.ToString();
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw new ArgumentException("צריך לבחור לפחות תחום עיסוק אחד");

                }


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
                int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // execute the command
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
            String end = "; SELECT CAST(scope_identity() AS int)";
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}')", gallery.GalleryName, ((DateTime)gallery.Time).ToString("t"), ((DateTime)gallery.Date).ToString("u"), gallery.Description, gallery.Email, gallery.IsActive);

            String prefix = "INSERT INTO Gallery " + "([GalleryName],[UploadTime],[UploadDate],[Description],[UserEmail],[IsActive])";

            command = prefix + sb.ToString() + end;


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

                    g.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    g.Email = Convert.ToString(dr["UserEmail"]);
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
        public List<Gallery> getAllGalleriesAl()
        {
            SqlConnection con = null;
            List<Gallery> gList = new List<Gallery>();


            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " select gl.GalleryId, gl.GalleryName , gl.Url, gl.UploadTime, gl.UploadDate, gl.Description, gl.UserEmail, gl.IsActive, glp.PhotoUrl, ProfessionName , usFav.Email as Email_liked,MG.MakerspaceId,MG.MakerspaceName from Gallery as gl left join Gallery_Photo as glp on gl.GalleryId = glp.GalleryId left join Professions_Gallery as progl on gl.GalleryId = progl.GalleryId left join Users_Gallery_Fav as usFav on usFav.GalleryId=gl.GalleryId  inner join Makerspace_Gallery as MG on gl.GalleryId=MG.GalleryId";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Gallery g = new Gallery();
                    g.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (g.IsActive == true)
                    {
                        g.GalleryId = Convert.ToInt32(dr["GalleryId"]);
                        g.GalleryName = Convert.ToString(dr["GalleryName"]);
                        g.Url = Convert.ToString(dr["Url"]);
                        g.Date = Convert.ToDateTime(dr["UploadDate"]);
                        g.Time = Convert.ToDateTime(dr["UploadTime"]);
                        g.Description = Convert.ToString(dr["Description"]);
                        g.Email = Convert.ToString(dr["UserEmail"]);
                        g.Profession = Convert.ToString(dr["ProfessionName"]);
                        g.Image = Convert.ToString(dr["PhotoUrl"]);
                        g.Email_liked = Convert.ToString(dr["Email_liked"]);
                        g.MakerspaceName = Convert.ToString(dr["MakerspaceName"]);
                        g.MakerspaceId = Convert.ToInt32(dr["MakerspaceId"]);
                        gList.Add(g);
                    }

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

        public List<Gallery> getGalleriesliked(string email)
        {
            SqlConnection con = null;
            List<Gallery> gList = new List<Gallery>();


            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = " select gl.GalleryId, gl.GalleryName , gl.Url, gl.UploadTime, gl.UploadDate, gl.Description, gl.UserEmail, gl.IsActive, glp.PhotoUrl, ProfessionName,MG.MakerspaceId,MG.MakerspaceName from Gallery as gl left join Gallery_Photo as glp on gl.GalleryId = glp.GalleryId left join Professions_Gallery as progl on gl.GalleryId = progl.GalleryId  inner join Makerspace_Gallery as MG on gl.GalleryId=MG.GalleryId where exists(select GalleryId from Users_Gallery_Fav as ufav where ufav.GalleryId=gl.GalleryId AND ufav.Email='" + email + "')";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Gallery g = new Gallery();
                    g.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    g.GalleryId = Convert.ToInt32(dr["GalleryId"]);
                    g.GalleryName = Convert.ToString(dr["GalleryName"]);
                    g.Url = Convert.ToString(dr["Url"]);
                    g.Date = Convert.ToDateTime(dr["UploadDate"]);
                    g.Time = Convert.ToDateTime(dr["UploadTime"]);
                    g.Description = Convert.ToString(dr["Description"]);
                    g.Email = Convert.ToString(dr["UserEmail"]);
                    g.Profession = Convert.ToString(dr["ProfessionName"]);
                    g.Image = Convert.ToString(dr["PhotoUrl"]);
                    g.MakerspaceName = Convert.ToString(dr["MakerspaceName"]);
                    g.MakerspaceId = Convert.ToInt32(dr["MakerspaceId"]);
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

        public List<Gallery> getPubGalleries(string email)
        {
            SqlConnection con = null;
            List<Gallery> gList = new List<Gallery>();


            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select gl.GalleryId, gl.GalleryName , gl.Url, gl.UploadTime, gl.UploadDate, gl.Description, gl.UserEmail, gl.IsActive, glp.PhotoUrl, ProfessionName,MG.MakerspaceId,MG.MakerspaceName from Gallery as gl left join Gallery_Photo as glp on gl.GalleryId = glp.GalleryId left join Professions_Gallery as progl on gl.GalleryId = progl.GalleryId inner join Makerspace_Gallery as MG on gl.GalleryId=MG.GalleryId Where gl.UserEmail='" + email + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Gallery g = new Gallery();
                    g.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    g.GalleryId = Convert.ToInt32(dr["GalleryId"]);
                    g.GalleryName = Convert.ToString(dr["GalleryName"]);
                    g.Url = Convert.ToString(dr["Url"]);
                    g.Date = Convert.ToDateTime(dr["UploadDate"]);
                    g.Time = Convert.ToDateTime(dr["UploadTime"]);
                    g.Description = Convert.ToString(dr["Description"]);
                    g.Email = Convert.ToString(dr["UserEmail"]);
                    g.Profession = Convert.ToString(dr["ProfessionName"]);
                    g.Image = Convert.ToString(dr["PhotoUrl"]);
                    g.MakerspaceName = Convert.ToString(dr["MakerspaceName"]);
                    g.MakerspaceId = Convert.ToInt32(dr["MakerspaceId"]);

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
        public int InsertProffesion_Gallery(Gallery gallery, int id)
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

        private String BuildInsertProfCommand(Gallery gallery, int id)
        {
            String command = "";
            if (gallery.ProfArr != null && gallery.ProfArr.Length > 0)
            {

                for (int i = 0; i < gallery.ProfArr.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Values('{0}','{1}')", id, gallery.ProfArr[i]);
                    String prefix = "INSERT INTO Professions_Gallery" + "([GalleryId],[ProfessionName])";

                    command += prefix + sb.ToString();
                }
            }
            for (int i = 0; i < gallery.ImageArr.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Values('{0}','{1}')", id, gallery.ImageArr[i]);
                String prefix = "INSERT INTO Gallery_Photo " + "([GalleryId],[PhotoUrl])";
                command += prefix + sb.ToString();
            }
            return command;
        }

        public int InsertMakerspace_Gallery(Gallery gallery, int id)
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

            String cStr = BuildInsertMakerspace_GalleryCommand(gallery, id);      // helper method to build the insert string

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

        private String BuildInsertMakerspace_GalleryCommand(Gallery gallery, int id)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("Values('{0}','{1}','{2}','{3}')", id, gallery.MakerspaceId, gallery.MakerspaceName, gallery.GalleryName);

            String prefix = "INSERT INTO Makerspace_Gallery " + "([GalleryId],[MakerspaceId],[MakerspaceName],[GalleryName])";

            command = prefix + sb.ToString();
            return command;
        }

        public int UpdateGalPublish(Gallery gallery)
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

            String cStr = BuildUpdateGalPublish(gallery);      // helper method to build the insert string

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
        private String BuildUpdateGalPublish(Gallery gallery)
        {
            String command;
            command = "UPDATE Gallery SET IsActive='1' WHERE GalleryId='" + gallery.GalleryId + "'";
            return command;
        }
        public int UpdateGalSave(Gallery gallery)
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

            String cStr = BuildUpdateGalSave(gallery);      // helper method to build the insert string

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
        private String BuildUpdateGalSave(Gallery gallery)
        {
            String command;
            command = "UPDATE Gallery SET IsActive='0' WHERE GalleryId='" + gallery.GalleryId + "'";
            return command;
        }
        public int InsertUserFavGal(Gallery gallery)
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

            String cStr = BuildInsertUserFavGalCommand(gallery);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to add a new gallery to favorite, Try again!", ex);
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

        private String BuildInsertUserFavGalCommand(Gallery gallery)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("Values('{0}','{1}')", gallery.GalleryId, gallery.Email);

            String prefix = "INSERT INTO Users_Gallery_Fav " + "([GalleryId],[Email])";

            command = prefix + sb.ToString();


            return command;
        }

        public int Delete(Gallery gallery)
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

            String cStr = BuildDeleteCommand(gallery);      // helper method to build the insert string

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

        private String BuildDeleteCommand(Gallery gallery)
        {
            String command;
            command = "DELETE FROM Users_Gallery_Fav WHERE GalleryId = " + gallery.GalleryId + "AND Email='" + gallery.Email + "'";
            return command;
        }


        //--------MakerSpace---------

        public int InsertMakerspace(Makerspace makerspace)
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

            String cStr = BuildInsertMakerspaceCommand(makerspace);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to add a new makerspace, Try again!", ex);
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



        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertMakerspaceCommand(Makerspace makerspace)
        {
            String command;
            String prefix;
            String end = "; SELECT CAST(scope_identity() AS int)";
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            if (makerspace.Photo_makerspace != null)
            {
                sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
               makerspace.User_email, makerspace.PhoneNumber, makerspace.Url, makerspace.NoPeople,
               makerspace.Size, makerspace.Price, makerspace.Rating, makerspace.Aircondition,
               makerspace.Accessibility, makerspace.Serving_coffee, makerspace.Online_payment, makerspace.Free_parking,
               makerspace.MakerspaceName, makerspace.Descrip, makerspace.City, makerspace.Street,
                makerspace.Num_street, makerspace.Photo_makerspace);

                prefix = "INSERT INTO Makerspace " + "([UserEmail],[PhoneNumber],[Url],[NoPeople],[SizeInM],[PricePerHour],[Rating],[Aircondition],[Accessibility],[Serving_coffee],[Online_payment],[Free_parking],[MakerspaceName],[Descrip],[City],[Street],[Num_street],[Photo_makerspace])";
            }
            else
            {
                sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')",
                 makerspace.User_email, makerspace.PhoneNumber, makerspace.Url, makerspace.NoPeople,
                 makerspace.Size, makerspace.Price, makerspace.Rating, makerspace.Aircondition,
                 makerspace.Accessibility, makerspace.Serving_coffee, makerspace.Online_payment, makerspace.Free_parking,
                 makerspace.MakerspaceName, makerspace.Descrip, makerspace.City, makerspace.Street,
                 makerspace.Num_street);

                prefix = "INSERT INTO Makerspace " + "([UserEmail],[PhoneNumber],[Url],[NoPeople],[SizeInM],[PricePerHour],[Rating],[Aircondition],[Accessibility],[Serving_coffee],[Online_payment],[Free_parking],[MakerspaceName],[Descrip],[City],[Street],[Num_street])";
            }


            command = prefix + sb.ToString() + end;

            return command;
        }

        public int InsertMakerspaceOpenningHours(Makerspace makerspace, int id)
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

            String cStr = BuildInsertMakerspaceOpenningHours(makerspace, id);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("לא הכנסת שעות פעילות", ex);
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

        private String BuildInsertMakerspaceOpenningHours(Makerspace makerspace, int id)
        {
            String command = "";

            dynamic stuff = JObject.Parse(makerspace.Days_hours);
            String Sunday_start = stuff.Sunday[0];
            String Sunday_end = stuff.Sunday[1];
            String Monday_start = stuff.Monday[0];
            String Monday_end = stuff.Monday[1];
            String Tuesday_start = stuff.Tuesday[0];
            String Tuesday_end = stuff.Tuesday[1];
            String Wednesday_start = stuff.Wednesday[0];
            String Wednesday_end = stuff.Wednesday[1];
            String Thursday_start = stuff.Thursday[0];
            String Thursday_end = stuff.Thursday[1];
            String Friday_start = stuff.Friday[0];
            String Friday_end = stuff.Friday[1];
            String Saturday_start = stuff.Saturday[0];
            String Saturday_end = stuff.Saturday[1];

            string[] hours_start = new string[] { Sunday_start, Monday_start, Tuesday_start, Wednesday_start, Thursday_start, Friday_start, Saturday_start };
            string[] hours_end = new string[] { Sunday_end, Monday_end, Tuesday_end, Wednesday_end, Thursday_end, Friday_end, Saturday_end };
            // use a string builder to create the dynamic string
            for (int i = 0; i < 7; i++)
            {
                if ((hours_start[i] != "") || (hours_end[i] != ""))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Values('{0}','{1}','{2}','{3}');", id, (i + 1), hours_start[i], hours_end[i]);
                    String prefix = "INSERT INTO MakerSpace_OpenningHours" + "([MakerspaceId],[DayonWeek],[Hour_start],[Hour_end])";

                    command += prefix + sb.ToString();
                }

            }



            return command;
        }

        public List<Makerspace> getMakerspaceUser(string email)
        {

            SqlConnection con = null;
            List<Makerspace> mList = new List<Makerspace>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Makerspace inner join Makerspace_Professions as MP on Makerspace.MakerspaceId=MP.MakerspaceId inner join MakerSpace_OpenningHours as MOH on Makerspace.MakerspaceId=MOH.MakerspaceId where UserEmail ='" + email + "' order by Makerspace.MakerspaceId asc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Makerspace m = new Makerspace();
                    m.MakerspaceId = Convert.ToInt32(dr["MakerspaceId"]);
                    m.MakerspaceName = Convert.ToString(dr["MakerspaceName"]);
                    m.PhoneNumber = Convert.ToString(dr["PhoneNumber"]);
                    m.Url = Convert.ToString(dr["Url"]);
                    m.User_email = Convert.ToString(dr["UserEmail"]);
                    m.Photo_makerspace = Convert.ToString(dr["Photo_makerspace"]);
                    m.NoPeople = Convert.ToInt32(dr["NoPeople"]);
                    m.Size = Convert.ToInt32(dr["SizeInM"]);
                    m.Price = Convert.ToInt32(dr["PricePerHour"]);
                    m.Aircondition = Convert.ToBoolean(dr["Aircondition"]);
                    m.Accessibility = Convert.ToBoolean(dr["Accessibility"]);
                    m.Serving_coffee = Convert.ToBoolean(dr["Serving_coffee"]);
                    m.Online_payment = Convert.ToBoolean(dr["Online_payment"]);
                    m.Free_parking = Convert.ToBoolean(dr["Free_parking"]);
                    m.Descrip = Convert.ToString(dr["Descrip"]);
                    m.City = Convert.ToString(dr["City"]);
                    m.Street = Convert.ToString(dr["Street"]);
                    m.Num_street = Convert.ToInt32(dr["Num_street"]);
                    m.Profession = Convert.ToString(dr["ProfessionName"]);
                    m.Dayonweek = Convert.ToInt32(dr["DayonWeek"]);
                    m.H_start = Convert.ToString(dr["Hour_start"]);
                    m.H_end = Convert.ToString(dr["Hour_end"]);



                    mList.Add(m);

                }

                return mList;
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
        public List<Makerspace> getAllMakerspaces()
        {

            SqlConnection con = null;
            List<Makerspace> mList = new List<Makerspace>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Makerspace order by Makerspace.MakerspaceId asc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Makerspace m = new Makerspace();
                    m.MakerspaceId = Convert.ToInt32(dr["MakerspaceId"]);
                    m.MakerspaceName = Convert.ToString(dr["MakerspaceName"]);
                    m.PhoneNumber = Convert.ToString(dr["PhoneNumber"]);
                    m.Url = Convert.ToString(dr["Url"]);
                    m.Photo_makerspace = Convert.ToString(dr["Photo_makerspace"]);
                    m.User_email = Convert.ToString(dr["UserEmail"]);
                    m.NoPeople = Convert.ToInt32(dr["NoPeople"]);
                    m.Size = Convert.ToInt32(dr["SizeInM"]);
                    m.Price = Convert.ToInt32(dr["PricePerHour"]);
                    m.Aircondition = Convert.ToBoolean(dr["Aircondition"]);
                    m.Accessibility = Convert.ToBoolean(dr["Accessibility"]);
                    m.Serving_coffee = Convert.ToBoolean(dr["Serving_coffee"]);
                    m.Online_payment = Convert.ToBoolean(dr["Online_payment"]);
                    m.Free_parking = Convert.ToBoolean(dr["Free_parking"]);
                    m.Descrip = Convert.ToString(dr["Descrip"]);
                    m.City = Convert.ToString(dr["City"]);
                    m.Street = Convert.ToString(dr["Street"]);
                    m.Num_street = Convert.ToInt32(dr["Num_street"]);

                    mList.Add(m);

                }

                return mList;
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
        public List<Makerspace> getLikedMakers(string email)
        {

            SqlConnection con = null;
            List<Makerspace> mList = new List<Makerspace>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "  select * from Makerspace as m inner join Users_Makerspace_Fav as umf on m.MakerspaceId=umf.MakerspaceId where umf.Email='"+email+"' order by m.MakerspaceId asc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Makerspace m = new Makerspace();
                    m.MakerspaceId = Convert.ToInt32(dr["MakerspaceId"]);
                    m.MakerspaceName = Convert.ToString(dr["MakerspaceName"]);
                    m.PhoneNumber = Convert.ToString(dr["PhoneNumber"]);
                    m.Url = Convert.ToString(dr["Url"]);
                    m.Photo_makerspace = Convert.ToString(dr["Photo_makerspace"]);
                    m.User_email = Convert.ToString(dr["UserEmail"]);
                    m.NoPeople = Convert.ToInt32(dr["NoPeople"]);
                    m.Size = Convert.ToInt32(dr["SizeInM"]);
                    m.Price = Convert.ToInt32(dr["PricePerHour"]);
                    m.Aircondition = Convert.ToBoolean(dr["Aircondition"]);
                    m.Accessibility = Convert.ToBoolean(dr["Accessibility"]);
                    m.Serving_coffee = Convert.ToBoolean(dr["Serving_coffee"]);
                    m.Online_payment = Convert.ToBoolean(dr["Online_payment"]);
                    m.Free_parking = Convert.ToBoolean(dr["Free_parking"]);
                    m.Descrip = Convert.ToString(dr["Descrip"]);
                    m.City = Convert.ToString(dr["City"]);
                    m.Street = Convert.ToString(dr["Street"]);
                    m.Num_street = Convert.ToInt32(dr["Num_street"]);

                    mList.Add(m);

                }

                return mList;
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
        public int InsertMakerspaceProf(Makerspace makerspace, int id)
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

            String cStr = BuildInsertMakerspaceProfCommand(makerspace, id);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to add a new makerProf, Try again!", ex);
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

        private String BuildInsertMakerspaceProfCommand(Makerspace makerspace, int id)
        {
            String command = "";

            for (int i = 0; i < makerspace.ProfessionArr.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Values('{0}','{1}')", id, makerspace.ProfessionArr[i]);
                String prefix = "INSERT INTO Makerspace_Professions" + "([MakerspaceId],[ProfessionName])";

                command += prefix + sb.ToString();
            }


            return command;
        }
        public int DeleteMakerspace(Makerspace makerspace)
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

            String cStr = BuildDeleteMakerspaceCommand(makerspace);      // helper method to build the insert string

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

        private String BuildDeleteMakerspaceCommand(Makerspace makerspace)
        {
            String command;
            command = "DELETE FROM Makerspace WHERE MakerspaceId = " + makerspace.MakerspaceId;
            return command;
        }
        public int DeleteMakerspaceFav(Makerspace makerspace)
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

            String cStr = BuildDeleteMakerspaceFavCommand(makerspace);      // helper method to build the insert string

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

        private String BuildDeleteMakerspaceFavCommand(Makerspace makerspace)
        {
            String command;
            command = "DELETE FROM Users_Makerspace_Fav WHERE MakerspaceId = " + makerspace.MakerspaceId + " AND Email='" + makerspace.User_email + "'";
            return command;
        }
        public int MakerspaceLiked(Makerspace makerspace)
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

            String cStr = BuildMakerspaceLikedCommand(makerspace);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to like a makerspace, Try again!", ex);
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

        private String BuildMakerspaceLikedCommand(Makerspace makerspace)
        {
            String command = "";

            
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Values('{0}','{1}')", makerspace.MakerspaceId, makerspace.User_email);
                String prefix = "INSERT INTO Users_Makerspace_Fav" + "([MakerspaceId],[Email])";

                command += prefix + sb.ToString();
            
            return command;
        }

        //Tools
        public int InsertTool(Tool tool)
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

            String cStr = BuildInsertTool(tool);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = Convert.ToInt32(cmd.ExecuteScalar());  // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to add a new Tool, Try again!", ex);
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

        private String BuildInsertTool(Tool tool)
        {
            String command = "";
            String end = "; SELECT CAST(scope_identity() AS int)";
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}')", tool.Model, tool.Brand, tool.Qualifications, tool.Description, tool.ToolName);
            String prefix = "INSERT INTO Tool" + "([Model],[Brand],[Qualifications],[Description],[ToolName])";

            command += prefix + sb.ToString() + end;
            return command;
        }
        public int InsertTool_Makerspace(Tool tool, int id)
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

            String cStr = BuildInsertTool_MakerspaceCommand(tool, id);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to add a new Tool_makerspace, Try again!", ex);
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

        private String BuildInsertTool_MakerspaceCommand(Tool tool, int id)
        {
            String command = "";
            String prefix;
            StringBuilder sb = new StringBuilder();

            if (tool.Url_photo != null)
            {
                sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}');", tool.MakerspaceId, id, tool.Quantity, tool.Url_photo, tool.Description);
                prefix = "INSERT INTO Makerspace_Tool" + "([MakerspaceId],[ToolId],[Quantity],[Photo_Tool],[Description])";

                command += prefix + sb.ToString();
            }
            else
            {
                sb.AppendFormat("Values('{0}','{1}','{2}','{3}');", tool.MakerspaceId, id, tool.Quantity, tool.Description);
                prefix = "INSERT INTO Makerspace_Tool" + "([MakerspaceId],[ToolId],[Quantity],[Description])";

                command += prefix + sb.ToString();
            }

            return command;
        }
        public List<Tool> getToolsUser(string email)
        {

            SqlConnection con = null;
            List<Tool> tList = new List<Tool>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Makerspace_Tool as MT inner join Tool on Tool.ToolId=MT.ToolId inner join Makerspace as M on M.MakerspaceId=MT.MakerspaceId where UserEmail ='" + email + "' order by M.MakerspaceId asc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Tool t = new Tool();
                    t.ToolId = Convert.ToInt32(dr["ToolId"]);
                    t.MakerspaceId = Convert.ToInt32(dr["MakerspaceId"]);
                    t.ToolName = Convert.ToString(dr["ToolName"]);
                    t.Brand = Convert.ToString(dr["Brand"]);
                    t.Model = Convert.ToString(dr["Model"]);
                    t.Url_photo = Convert.ToString(dr["Photo_Tool"]);
                    t.Qualifications = Convert.ToBoolean(dr["Qualifications"]);
                    t.Description = Convert.ToString(dr["Description"]);
                    t.Quantity = Convert.ToInt32(dr["Quantity"]);
                    tList.Add(t);
                }
                return tList;
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
        public List<Tool> getTools()
        {

            SqlConnection con = null;
            List<Tool> tList = new List<Tool>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Tool where ToolId in (Select max(ToolId) FROM Tool group by ToolName)";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Tool t = new Tool();
                    t.ToolId = Convert.ToInt32(dr["ToolId"]);
                    t.ToolName = Convert.ToString(dr["ToolName"]);
                    t.Brand = Convert.ToString(dr["Brand"]);
                    t.Model = Convert.ToString(dr["Model"]);
                    t.Qualifications = Convert.ToBoolean(dr["Qualifications"]);
                    t.Description = Convert.ToString(dr["Description"]);

                    tList.Add(t);
                }
                return tList;
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
        public List<Reservation> getRequest()
        {

            SqlConnection con = null;
            List<Reservation> rList = new List<Reservation>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Reservation as rq inner join Users as us on rq.UserEmail=us.Email where StatusApproved=0" +
                    "order by rq.ReservationDate";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Reservation r = new Reservation();

                    r.ReservationId = Convert.ToInt32(dr["reservationId"]);
                    r.Date = Convert.ToDateTime(dr["reservationDate"]);
                    r.StartTime_req = Convert.ToDateTime(dr["StartTime_req"]);
                    r.EndTime_req = Convert.ToDateTime(dr["EndTime_req"]);
                    //r.StartTime_res = Convert.ToDateTime(dr["StartTime_res"]);
                    //r.EndTime_res = Convert.ToDateTime(dr["EndTime_res"]);
                    //r.Description = Convert.ToString(dr["Description"]);
                    r.Span = Convert.ToDouble(dr["Span"]);
                    r.StatusApproved = Convert.ToBoolean(dr["StatusApproved"]);
                    r.UserName = Convert.ToString(dr["Fname"]) + " " + Convert.ToString(dr["Lname"]);

                    rList.Add(r);
                }
                return rList;
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


        public int DeleteRQ(Reservation request)
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

            String cStr = BuildDeleteCommand(request);      // helper method to build the insert string

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

        private String BuildDeleteCommand(Reservation request)
        {
            String command;
            command = "delete from Reservation where ReservationId =" + request.ReservationId;
            return command;
        }


        public int UpdateRES(Reservation res)
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

            String cStr = BuildUpdateCommand(res);      // helper method to build the insert string

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

        private String BuildUpdateCommand(Reservation res)
        {
            String command;

            command = "update Reservation set StartTime_res = StartTime_req, EndTime_res = EndTime_req, StatusApproved = 1 where ReservationId ='" + res.ReservationId + "'";

            return command;
        }

        public List<Reservation> getApprovedReservation()
        {

            SqlConnection con = null;
            List<Reservation> rList = new List<Reservation>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Reservation as rq inner join Users as us on rq.UserEmail = us.Email " +
                    "where StatusApproved = 1 and ReservationDate > GETDATE() order by rq.ReservationDate";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Reservation r = new Reservation();

                    r.ReservationId = Convert.ToInt32(dr["reservationId"]);
                    r.Date = Convert.ToDateTime(dr["reservationDate"]);
                    r.StartTime_req = Convert.ToDateTime(dr["StartTime_req"]);
                    r.EndTime_req = Convert.ToDateTime(dr["EndTime_req"]);
                    //r.StartTime_res = Convert.ToDateTime(dr["StartTime_res"]);
                    //r.EndTime_res = Convert.ToDateTime(dr["EndTime_res"]);
                    //r.Description = Convert.ToString(dr["Description"]);
                    r.Span = Convert.ToDouble(dr["Span"]);
                    r.StatusApproved = Convert.ToBoolean(dr["StatusApproved"]);
                    r.UserName = Convert.ToString(dr["Fname"]) + " " + Convert.ToString(dr["Lname"]);

                    rList.Add(r);
                }
                return rList;
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

        public List<Reservation> getHistoryReservation()
        {

            SqlConnection con = null;
            List<Reservation> rList = new List<Reservation>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Reservation as rq inner join Users as us on rq.UserEmail = us.Email " +
                    "where StatusApproved = 1 and ReservationDate < GETDATE() order by rq.ReservationDate desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Reservation r = new Reservation();

                    r.ReservationId = Convert.ToInt32(dr["reservationId"]);
                    r.Date = Convert.ToDateTime(dr["reservationDate"]);
                    r.StartTime_req = Convert.ToDateTime(dr["StartTime_req"]);
                    r.EndTime_req = Convert.ToDateTime(dr["EndTime_req"]);
                    //r.StartTime_res = Convert.ToDateTime(dr["StartTime_res"]);
                    //r.EndTime_res = Convert.ToDateTime(dr["EndTime_res"]);
                    //r.Description = Convert.ToString(dr["Description"]);
                    r.Span = Convert.ToDouble(dr["Span"]);
                    r.StatusApproved = Convert.ToBoolean(dr["StatusApproved"]);
                    r.UserName = Convert.ToString(dr["Fname"]) + " " + Convert.ToString(dr["Lname"]);

                    rList.Add(r);
                }
                return rList;
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


