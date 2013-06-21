using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppEventViewer
{
    public static class Global_Const {
        public const string SOURCE = "application";
        public const string DATE_FORMAT = "yyyyMMddHHmmss";
        public const string DATE_FORMAT_STR = "{0:yyyyMMddHHmmss}";
        public const int MAXGETROWS = 5000;
        public const int TIMEOUT_S = 2;
    }

   public interface IAppConfig
    {
        string FilterTerm { get; set; }
        System.Collections.Generic.List<string> ServersToQuery { get; set; }
    }
    public   class AppConfig : AppEventViewer.IAppConfig
    {
        public AppConfig()     
        {
            //Fake two servers by talke localhost computer name and . that is local computer
            _serversToQuery = new List<string> { System.Environment.MachineName, "." };
            _filterTerm = "TCM";
            _filterTerm = "TCM";
        }

        private static List<string> _serversToQuery;
        private string _filterTerm;
        public  string FilterTerm
        {
            get { return _filterTerm; }
            set { _filterTerm = value; }
        }

        List<string> IAppConfig.ServersToQuery
        {
            get { return ServersToQuery; }
            set { ServersToQuery = value; }
        }

        public static List<string> ServersToQuery
        {
            get
            {
                //_serversToQuery = new List<string> {"Herkules", "."};
                
                return _serversToQuery;
            }
            set { _serversToQuery = value; }
        }
    }
}