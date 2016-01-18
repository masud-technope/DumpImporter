using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace DumpImporter.DAL
{
    class Votes
    {
        //attributes
        public int Id;
        public int PostId;
        public int VoteTypeId;
        public int UserId;
        public DateTime CreationDate;
        public int BountyAmount;


        public static void addBatchInsert(List<Votes> collection)
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
                string message = "Total " + rows + " added into Votes table at " + DateTime.Now;
                Utility.logMessage(message);
            }
            catch (Exception exc)
            {
                Utility.logMessage(DateTime.Now + " " + exc.Message);
            }
        }

        protected static string prepareQuery(List<Votes> collection)
        {
            string rows = string.Empty;
            string valueLines = string.Empty;
            foreach (Votes vote in collection)
            {
                string line = "( ";
                line += vote.Id + "," + vote.PostId + "," + vote.VoteTypeId + "," + vote.UserId + ",'" + vote.CreationDate + "'," + vote.BountyAmount;
                line += " )";
                valueLines += "," + line;
            }
            valueLines = valueLines.Substring(1);
            string fieldList = "[Id], [PostId], [VoteTypeId], [UserId], [CreationDate], [BountyAmount]";
            string query = "INSERT INTO Votes (" + fieldList + ")" + " VALUES " + valueLines;
            return query;
        }

    }
}
