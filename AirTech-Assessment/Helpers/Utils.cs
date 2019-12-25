using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AirTech_Assessment.Helpers
{
    /// <summary>
    /// Class dealing with the common utility operations.
    /// </summary>
   public class Utils
    {
        public static string LoadJson(string strJSONFilePath)
        {
            string strJSON = string.Empty;

            using(StreamReader sr = new StreamReader(strJSONFilePath))
            {
                strJSON = sr.ReadToEnd();
            }
            return strJSON;
        }
    }
}
