using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Biblioteca
{
    class ConnectioString
    {
        static ConnectioString()
        {
            LocalConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

        public static string LocalConnectionString;
    }
}