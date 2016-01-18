using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DumpImporter
{
    class DatabaseAccessDemo
    {
        static string CONNECTION_STRING = StaticData.connectionString;
        public void checkUser() {

            //list to hold users
            List<String> users = new List<string>();
            users = loadQueryResults();
            //now check the presence of the user of interest
            int usercount = 0;
            foreach (string user in users)
            {
                if (user == "Francois")
                {
                    usercount++;
                }
            }
            Console.WriteLine("No of Francois :" + usercount);
        }
        
        protected List<string> loadQueryResults() {

            //list to hold the users
            List<String> users = new List<string>();
            //collecting the users
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNECTION_STRING;
            conn.Open();
            string getUsers = "SELECT name from users";
            SqlCommand cmd = new SqlCommand(getUsers, conn);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while(reader.Read()){
                string userName = reader["name"].ToString();
                users.Add(userName);
            }
            conn.Close();
            //returning the users
            return users;
        }
    }
}
