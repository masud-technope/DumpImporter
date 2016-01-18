using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DumpImporter
{
    class Utility
    {
        public static void logMessage(string message)
        {
            //logging the message
            StreamWriter writer = new StreamWriter("importer.log",true);
            writer.WriteLine(message);
            writer.Close();
        }


    
    }
}
