using System.Collections.Generic;

namespace AppEventViewer.Tests
{
 

    public   class AppConfig_Mock : IAppConfig
    {
        public AppConfig_Mock()     
        {
            //Fake two servers by talke localhost computer name and . that is local computer
            ServersToQuery = new List<string> { System.Environment.MachineName, "." }; 
            FilterTerm = "TCM";
        }

        public string FilterTerm { get; set; }

        List<string> IAppConfig.ServersToQuery
        {
            get { return ServersToQuery; }
            set { ServersToQuery = value; }
        }

        public static List<string> ServersToQuery { get; set; }
    }

    public class AppConfig_Null_Mock : IAppConfig
    {
        private string _filterTerm =null;
        private List<string> _serversToQuery = null;

        public string FilterTerm
        {
            get { return _filterTerm; }
            set { _filterTerm = value; }
        }

        public List<string> ServersToQuery
        {
            get { return _serversToQuery; }
            set { _serversToQuery = value; }
        }
    }
}