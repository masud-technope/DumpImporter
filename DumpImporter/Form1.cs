using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace DumpImporter
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        
        public Form1()
        {
            InitializeComponent();
            createLogFile();
            AllocConsole();
        }



        protected void createLogFile()
        {
            if(!File.Exists("importer.log"))
            File.Create("importer.log");
        }


        private void ImportButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                return;
            }
            else { 
                    //collect data from XML file
                string xmlFile = textBox1.Text;
                Importter importer = new Importter(xmlFile);
                importer.readVotesXMLFile();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //database connection test
            try
            {
                SqlConnection conn = new SqlConnection(StaticData.connectionString);
                conn.Open();
                if (conn != null)
                {
                    MessageBox.Show("Connection established");
                }
            }
            catch (Exception exc) {
                MessageBox.Show("Cannot create the connection");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DatabaseAccessDemo demo = new DatabaseAccessDemo();
            //demo.loadQueryResults();
        }
    }
}
