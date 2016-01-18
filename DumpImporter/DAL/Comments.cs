using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace DumpImporter.DAL
{
    class Comments
    {
        //public attributes
        public int Id;
        public int PostId;
        public int Score;
        public string Text;
        public DateTime CreationDate;
        public string UserDisplayName;
        public int UserId;


        public static void addBatchInsert(List<Comments> collection)
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
                string message = "Total " + rows + " added into Comments table at " + DateTime.Now;
                Utility.logMessage(message);
            }
            catch (Exception exc)
            {
                Utility.logMessage(DateTime.Now + " " + exc.Message);
            }

        }

        protected static string prepareQuery(List<Comments> collection)
        {
            string rows = string.Empty;
            string valueLines = string.Empty;
            foreach (Comments comment in collection)
            {
                string line = "( ";
                line += comment.Id + "," + comment.PostId + "," + comment.Score + ",'" + comment.Text.Replace('\'', ' ') +
                    "','" + comment.CreationDate + "'," + comment.UserId;
                line += " )";
                valueLines += "," + line;
            }
            valueLines = valueLines.Substring(1);
            string fieldList = "[Id], [PostId], [Score], [Text], [CreationDate], [UserId]";
            string query = "INSERT INTO Comments (" + fieldList + ")" + " VALUES " + valueLines;
            return query;
        }
    }
}
