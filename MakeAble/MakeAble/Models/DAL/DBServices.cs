﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using System.Web.Configuration;

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

            return command;
        }
        public int InsertUser_prof(User user)
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

            String cStr = BuildInsertUser_profCommand(user);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("You didnt succeed to add a proffesion to user, Try again!", ex);
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
        private String BuildInsertUser_profCommand(User user)
        {
            String command="";

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            for (int i = 0; i < user.Profession.Length; i++)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("Values( '{0}','{1}','{2}','{3}')", customer.Id, hlist[i].Id, hlist[i].Name, customer.Price_range);
                String prefix = "INSERT INTO PreHigh_2021 " + "([id_cus], [id_highlight],[name_highlight],[price_range])";

                command += prefix + sb.ToString();
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
            if (user.ProfilePhoto!=null)
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


    }
}