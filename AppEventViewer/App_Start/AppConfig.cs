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

    interface IAppConfig
    {
        string FilterTerm { get; set; }
        System.Collections.Generic.List<string> ServersToQuery { get; set; }
    }
    public   class AppConfig : AppEventViewer.IAppConfig
    {
        public string FilterTerm { get; set; }
        public List<string> ServersToQuery  { get; set; }
    }
}