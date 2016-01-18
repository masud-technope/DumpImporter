using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace DumpImporter.DAL
{
    class Tags
    {
        public int Id;
        public string TagName;
        public int Count;
        public int ExcerptPostId;
        public int WikiPostId;


        public static void addBatchInsert(List<Tags> collection)
        { 
            //adding multiple rows at once
            try {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = StaticData.connectionString;
                conn.Open();
                string insertQuery=prepareQuery(collection);
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                int rows=cmd.ExecuteNonQuery();
                conn.Close();
                string message = "Total "+rows+" added into Tags table at "+DateTime.Now;
                Utility.logMessage(message);
            }
            catch (Exception exc) {
                Utility.logMessage(DateTime.Now + " "+exc.Message);
            }
        
        }

        protected static string prepareQuery(List<Tags> collection)
        {
            string rows = string.Empty;
            string valueLines = string.Empty;
            foreach(Tags tag in collection){
                string line = "( ";
                line += tag.Id + ",'" + tag.TagName + "'," + tag.Count + "," + tag.ExcerptPostId + "," + tag.WikiPostId;
                line += " )";
                valueLines += "," + line;
            }
            valueLines = valueLines.Substring(1);
            string fieldList = "[Id], [TagName], [Count], [ExcerptPostId], [WikiPostId]";
            string query = "INSERT INTO Tags (" + fieldList + ")" + " VALUES " + valueLines;
            return query;
        }

    }
}
