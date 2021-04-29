using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using System.Web.Razor.Text;

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

            if (user.Professions.Count > 0 && user.Professions != null)
            {
                for (int i = 0; i < user.Professions.Count; i++)
                {
                    sb = new StringBuilder();
                    sb.AppendFormat("Values('{0}','{1}')", user.Professions[i], user.Email);
                    prefix = ";INSERT INTO Users_Professions " + "([ProfessionName], [Email])";

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
            String prefix;
            StringBuilder sb = new StringBuilder();
            String delete = "; delete from Users_Professions where Email = '" + user.Email + "'";
            if (user.ProfilePhoto != null)
            {
                command = "UPDATE Users SET Fname='" + user.Fname + "', Lname='" + user.Lname + "'," +
                   "City='" + user.City + "', Password='" + user.Password + "',Phone='" + user.Phone + "', ProfilePhoto='" + user.ProfilePhoto +
                   "', BirthDay='" + user.BirthDay + "', Description='" + user.Description + "', Have_makerspace='" + user.Have_makerspace +
                   "' " + "WHERE Email='" + user.Email + "'";

                if (user.Professions.Count > 0 && user.Professions != null)
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
            else
            {
                command = "UPDATE Users SET Fname='" + user.Fname + "', Lname='" + user.Lname + "'," +
                   "City='" + user.City + "', Password='" + user.Password + "',Phone='" + user.Phone +
                   "', BirthDay='" + user.BirthDay + "', Description='" + user.Description + "', Have_makerspace='" + user.Have_makerspace +
                   "' " + "WHERE Email='" + user.Email + "'";

                if (user.Professions.Count > 0 && user.Professions != null)
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

                String selectSTR = " select gl.GalleryId, gl.GalleryName , gl.Url, gl.UploadTime, gl.UploadDate, gl.Description, gl.UserEmail, gl.IsActive, glp.PhotoUrl, ProfessionName , usFav.Email as Email_liked from Gallery as gl left join Gallery_Photo as glp on gl.GalleryId = glp.GalleryId left join Professions_Gallery as progl on gl.GalleryId = progl.GalleryId left join Users_Gallery_Fav as usFav on usFav.GalleryId=gl.GalleryId";
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

                        g.Email_liked= Convert.ToString(dr["Email_liked"]);

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

                String selectSTR = "select gl.GalleryId, gl.GalleryName , gl.Url, gl.UploadTime, gl.UploadDate, gl.Description, gl.UserEmail, gl.IsActive, glp.PhotoUrl, ProfessionName from Gallery as gl left join Gallery_Photo as glp on gl.GalleryId = glp.GalleryId left join Professions_Gallery as progl on gl.GalleryId = progl.GalleryId where exists(select GalleryId from Users_Gallery_Fav as ufav where ufav.GalleryId=gl.GalleryId AND ufav.Email='" + email + "')";
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

                String selectSTR = "select gl.GalleryId, gl.GalleryName , gl.Url, gl.UploadTime, gl.UploadDate, gl.Description, gl.UserEmail, gl.IsActive, glp.PhotoUrl, ProfessionName from Gallery as gl left join Gallery_Photo as glp on gl.GalleryId = glp.GalleryId left join Professions_Gallery as progl on gl.GalleryId = progl.GalleryId Where gl.UserEmail='" + email + "'";
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
            command = "DELETE FROM Users_Gallery_Fav WHERE GalleryId = " + gallery.GalleryId +"AND Email='"+gallery.Email+"'" ;
            return command;
        }


        //--------MakerSpace---------

        public int Insert(Makerspace makerspace)
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

            String cStr = BuildInsertCommand(makerspace);      // helper method to build the insert string

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
        private String BuildInsertCommand(Makerspace makerspace)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}'",
            makerspace.PhoneNumber, makerspace.Url, makerspace.NoPepole,
            makerspace.Size, makerspace.Price, makerspace.Rating, makerspace.Aircondition,
            makerspace.Accessibility, makerspace.Serving_coffee, makerspace.Online_payment, makerspace.Free_parking,
            makerspace.MakerspaceName, makerspace.Descrip, makerspace.City, makerspace.Street,
            makerspace.Num_street);

            String prefix = "INSERT INTO Makerspace " + "([PhoneNumber],[Url],[NoPepole],[SizeInM],[PricePerHour],[Rating],[Aircondition],[Accessibility],[Serving_coffee],[Online_payment],[Free_parking],[MakerspaceName],[Descrip],[City],[Street],[Num_street])";

            command = prefix + sb.ToString();
            return command;
        }
    }
}


