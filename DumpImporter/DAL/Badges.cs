using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace DumpImporter.DAL
{
    class Badges
    {
        public int Id;
        public int UserId;
        public string Name;
        public DateTime Date;


        public static void addBatchInsert(List<Badges> collection)
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
                string message = "Total " + rows + " added into Badges table at " + DateTime.Now;
                Utility.logMessage(message);
            }
            catch (Exception exc)
            {
                Utility.logMessage(DateTime.Now + " " + exc.Message);
            }

        }

        protected static string prepareQuery(List<Badges> collection)
        {
            string rows = string.Empty;
            string valueLines = string.Empty;
            foreach (Badges badge in collection)
            {
                string line = "( ";
                line += badge.Id + "," + badge.UserId + ",'" + badge.Name +"','" +badge.Date + "'";
                line += " )";
                valueLines += "," + line;
            }
            valueLines = valueLines.Substring(1);
            string fieldList = "[Id], [UserId], [Name], [Date]";
            string query = "INSERT INTO Badges (" + fieldList + ")" + " VALUES " + valueLines;
            return query;
        }
    }
    }
