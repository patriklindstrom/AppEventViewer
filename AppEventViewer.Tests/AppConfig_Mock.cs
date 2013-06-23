using System.Collections.Generic;
using AppEventViewer.App_Start;

namespace AppEventViewer.Tests
{
 

    public   class AppConfig_Mock : IAppConfig
    {


        public List<string> FilterTerm
        {
            get { return new List<string> { "TCM", "SQL", "MQ", "jabberjowitch" }; }

        }

        public List<string> ServersToQuery
        {
                        //Fake two servers by talke localhost computer name and . that is local computer
            get { return new List<string> {System.Environment.MachineName, "."}; }
        }

    }

    public class AppConfig_Null_Mock : IAppConfig
    {


        public List<string> FilterTerm
        {
            get { return null; }
        
        }

        public List<string> ServersToQuery
        {
            get { return null; }
        }
    }

    public class AppConfig_ZeroServer_Mock : IAppConfig
    {
        public List<string> FilterTerm
        {
            get { return new List<string> { "TCM", "SQL", "MQ" }; ; }

        }

        public List<string> ServersToQuery
        {
            get { return new List<string> {};  }
        }
    }
}