using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Text;

namespace DumpImporter.DAL
{
    class Users
    {
        //attributes
        public int Id;
        public int Reputation;
        public DateTime CreationDate;
        public string DisplayName;
        public DateTime LastAccessDate;
        public string WebsiteUrl;
        public string Location;
        public string AboutMe;
        public int Views;
        public int UpVotes;
        public int DownVotes;
        public string ProfileImageUrl;
        public string EmailHash;
        public int Age;
        public int AccountId;

        public static void addBatchInsert(List<Users> collection)
        {
            //adding multiple rows at once
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = StaticData.connectionString;
                conn.Open();
                string insertQuery = prepareQuery(collection);
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                string message = "Total " + rows + " added into Users table at " + DateTime.Now;
                Utility.logMessage(message);
            }
            catch (Exception exc)
            {
                Utility.logMessage(DateTime.Now + " " + exc.Message);
            }

        }

        protected static string prepareQuery(List<Users> collection)
        {
            string rows = string.Empty;
            string valueLines = string.Empty;
            foreach (Users user in collection)
            {
                string line = "( ";
                line += user.Id + "," + user.Reputation + ",'" + user.CreationDate + "','" +    user.DisplayName.Replace('\'',' ') + "','" + user.LastAccessDate + "','" + 
                    user.Location + "','"+user.WebsiteUrl +"'," + user.Views + "," + user.UpVotes + "," + user.DownVotes + ",'" +
                    user.EmailHash + "'," + user.Age + "," + user.AccountId;
                line += " )";
                valueLines += "," + line;
            }
            valueLines = valueLines.Substring(1);
            string fieldList = "[Id], [Reputation], [CreationDate], [DisplayName], [LastAccessDate], [Location],[WebsiteUrl],[Views]" +
                ", [UpVotes], [DownVotes], [EmailHash], [Age], [AccountId]";
            string query = "INSERT INTO [Users] (" + fieldList + ")" + " VALUES " + valueLines;
            return query;
        }


    
    }
}
