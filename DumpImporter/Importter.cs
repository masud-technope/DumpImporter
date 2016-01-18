using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DumpImporter.DAL;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace DumpImporter
{
    class Importter
    {

        string xmlFileName;
        List<Tags> collection;
        List<Badges> badgecollection;
        List<Users> usercoll;
        List<Comments> commcoll;
        List<Votes> votecoll;
        DataTable dtVotes;

        public Importter(string xmlFileName)
        {
            //default constructor
            this.xmlFileName = xmlFileName;
            //collection = new List<Tags>();
            //badgecollection = new List<Badges>();
            //usercoll = new List<Users>();
            //this.commcoll = new List<Comments>();
            //this.votecoll = new List<Votes>();
            dtVotes = new DataTable();
        }

        public void readTagsXMLFile()
        {
            //reading the XML file
            XmlReader reader = XmlReader.Create(this.xmlFileName);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "row")
                    {
                        Tags tag = new Tags();
                        try
                        {
                            tag.Id = int.Parse(reader["Id"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            tag.TagName = reader["TagName"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            tag.Count = int.Parse(reader["Count"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            tag.ExcerptPostId = int.Parse(reader["ExcerpPostId"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            tag.WikiPostId = int.Parse(reader["WikiPostId"]);
                        }
                        catch (Exception e) { }
                        collection.Add(tag);

                    }
                }
                if (collection.Count == 1000)
                {
                    Tags.addBatchInsert(collection);
                    collection.Clear();
                }
            }
            if (collection.Count > 0)
            {
                Tags.addBatchInsert(collection);
            }


        }

        public void readBadgesXMLFile()
        {
            //reading the XML file
            XmlReader reader = XmlReader.Create(this.xmlFileName);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "row")
                    {
                        Badges badge = new Badges();
                        try
                        {
                            badge.Id = int.Parse(reader["Id"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            badge.Name = reader["Name"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            badge.UserId = int.Parse(reader["UserId"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            badge.Date = DateTime.Parse(reader["Date"]);
                        }
                        catch (Exception e) { }

                        badgecollection.Add(badge);
                    }
                }
                if (badgecollection.Count == 1000)
                {
                    Badges.addBatchInsert(badgecollection);
                    badgecollection.Clear();
                }
            }
            if (badgecollection.Count > 0)
            {
                Badges.addBatchInsert(badgecollection);
            }


        }

        public void readUsersXMLFile()
        {
            //reading the XML file
            XmlReader reader = XmlReader.Create(this.xmlFileName);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "row")
                    {
                        Users user = new Users();
                        try
                        {
                            user.Id = int.Parse(reader["Id"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.Reputation = int.Parse(reader["Reputation"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.CreationDate = DateTime.Parse(reader["CreationDate"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.DisplayName = reader["DisplayName"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.LastAccessDate = DateTime.Parse(reader["LastAccessDate"]);
                        }
                        catch (Exception e)
                        {
                        }
                        try
                        {
                            user.WebsiteUrl = reader["WebsiteUrl"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.Location = reader["Location"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.AboutMe = reader["AboutMe"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.Views = int.Parse(reader["Views"]);
                        }
                        catch (Exception e)
                        {
                        }
                        try
                        {
                            user.UpVotes = int.Parse(reader["UpVotes"]);
                        }
                        catch (Exception e)
                        {
                        }
                        try
                        {
                            user.DownVotes = int.Parse(reader["DownVotes"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.ProfileImageUrl = reader["ProfileImageUrl"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.EmailHash = reader["EmailHash"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            user.Age = int.Parse(reader["Age"]);
                        }
                        catch (Exception e)
                        {
                        }
                        try
                        {
                            user.AccountId = int.Parse(reader["AccountId"]);
                        }
                        catch (Exception e)
                        {
                        }

                        usercoll.Add(user);
                    }
                }
                if (usercoll.Count == 1000)
                {
                    Users.addBatchInsert(usercoll);
                    usercoll.Clear();
                }
            }
            if (usercoll.Count > 0)
            {
                Users.addBatchInsert(usercoll);
            }

        }

        public void readCommentsXMLFile()
        {
            //reading the XML file
            XmlReader reader = XmlReader.Create(this.xmlFileName);
            int count = 0;
            int target = 25301000;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "row")
                    {
                        if (count <= target)
                        {
                            count++;
                            continue;
                        }

                        Comments comment = new Comments();
                        try
                        {
                            comment.Id = int.Parse(reader["Id"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            comment.PostId = int.Parse(reader["PostId"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            comment.Score = int.Parse(reader["Score"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            comment.Text = reader["Text"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            comment.CreationDate = DateTime.Parse(reader["CreationDate"]);
                        }
                        catch (Exception e) { }
                        try
                        {
                            comment.UserDisplayName = reader["UserDisplayName"];
                        }
                        catch (Exception e) { }
                        try
                        {
                            comment.UserId = int.Parse(reader["UserId"]);
                        }
                        catch (Exception exc) { }

                        commcoll.Add(comment);
                        count++;
                    }
                }
                if (commcoll.Count == 1000)
                {
                    if (count > target)
                        Comments.addBatchInsert(commcoll);
                    commcoll.Clear();
                }
            }
            if (commcoll.Count > 0)
            {
                Comments.addBatchInsert(commcoll);
            }


        }


        public void readVotesXMLFile()
        {
            string tableName = "Votes";
            Console.WriteLine(DateTime.Now);
            string connectionString = StaticData.connectionString;
            using (SqlConnection destinationConnection =
                            new SqlConnection(connectionString))
            {
                // open the connection
                destinationConnection.Open();
                using (SqlBulkCopy bulkCopy =
                            new SqlBulkCopy(destinationConnection.ConnectionString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsTransfer);
                    bulkCopy.NotifyAfter = 5000;
                    bulkCopy.BatchSize = 10000;
                    //bulkCopy.ColumnMappings.Add("OrderID", "NewOrderID");
                    bulkCopy.DestinationTableName = tableName;

                    //reading the XML file
                    XmlReader reader = XmlReader.Create(this.xmlFileName);

                    //table columns
                    dtVotes.Columns.Add("Id", typeof(int));
                    dtVotes.Columns.Add("PostId", typeof(int));
                    dtVotes.Columns.Add("VoteTypeId", typeof(Int16));
                    dtVotes.Columns.Add("UserId", typeof(int));
                    dtVotes.Columns.Add("CreationDate", typeof(DateTime));
                    dtVotes.Columns.Add("BountyAmount", typeof(int));

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "row")
                            {

                                Votes vote = new Votes();
                                try
                                {

                                    vote.Id = int.Parse(reader["Id"]);
                                }
                                catch (Exception e) { }
                                try
                                {
                                    vote.PostId = int.Parse(reader["PostId"]);
                                }
                                catch (Exception e) { }
                                try
                                {
                                    vote.VoteTypeId = int.Parse(reader["VoteTypeId"]);
                                }
                                catch (Exception e2) { }
                                try
                                {
                                    vote.UserId = int.Parse(reader["UserId"]);
                                }
                                catch (Exception e) { }
                                try
                                {
                                    vote.CreationDate = DateTime.Parse(reader["CreationDate"]);
                                }
                                catch (Exception e) { }
                                try
                                {
                                    vote.BountyAmount = int.Parse(reader["BountyAmount"]);
                                }
                                catch (Exception e3) { }

                                dtVotes.Rows.Add(vote.Id, vote.PostId, vote.VoteTypeId, vote.UserId, vote.CreationDate, vote.BountyAmount);

                                //votecoll.Add(vote);
                                //dtVotes.Rows.Add(reader);
                            }
                        }

                        if (dtVotes.Rows.Count == 10000) // (votecoll.Count == 1000)
                        {
                            //makeBulkInsert(dtVotes, tableName);
                            bulkCopy.WriteToServer(dtVotes);
                            dtVotes.Clear();
                            Console.WriteLine(DateTime.Now);
                            //Votes.addBatchInsert(votecoll);
                            //votecoll.Clear();
                        }
                    }
                    if (dtVotes.Rows.Count > 0)// (votecoll.Count > 0)
                    {
                        //Votes.addBatchInsert(votecoll);
                        //makeBulkInsert(dtVotes, tableName);
                        bulkCopy.WriteToServer(dtVotes);
                    }

                }
            }
        }

        protected void makeBulkInsert(DataTable sourceData, string tableName)
        {
            //performing the bulk insert
            // open the destination data
            string connectionString = StaticData.connectionString;
            using (SqlConnection destinationConnection =
                            new SqlConnection(connectionString))
            {
                // open the connection
                destinationConnection.Open();
                using (SqlBulkCopy bulkCopy =
                            new SqlBulkCopy(destinationConnection.ConnectionString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsTransfer);
                    bulkCopy.NotifyAfter = 100;
                    bulkCopy.BatchSize = 1000;
                    //bulkCopy.ColumnMappings.Add("OrderID", "NewOrderID");
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.WriteToServer(sourceData);
                }
            }

        }


        private static void OnSqlRowsTransfer(object sender, SqlRowsCopiedEventArgs e)
        {
            Console.WriteLine("Copied {0} so far...", e.RowsCopied);
        }


    }
}
