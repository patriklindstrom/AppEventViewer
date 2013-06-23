using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Mvc;
using AppEventViewer.Repository;
using AppEventViewer.ServiceInterface;
using ServiceStack.Configuration;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Logging;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.WebHost.Endpoints;

namespace AppEventViewer.App_Start
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
       List<string> FilterTerm { get; }
        List<string> ServersToQuery { get;  }
    }
    public   class AppConfig : IAppConfig
    {         
        public AppConfig()     
        {
            var  appSettings = new AppSettings();
            string baseApiUrl = appSettings.Get("BaseApiUrl","http://localhost:80/api/");
            _serversToQuery = (List<string>)appSettings.GetList("ListOfServers");
            _filterTerm = (List<string>)appSettings.GetList("FilterTermList");
            //Fake two servers by talke localhost computer name and . that is local computer
            // _serversToQuery = new List<string> { System.Environment.MachineName, "." };
        }
        private static List<string> _serversToQuery;
        private static List<string> _filterTerm;
        public  List<string> FilterTerm
        {
            get { return _filterTerm; }
        }

        public List<string> ServersToQuery
        {
            get { return _serversToQuery; }
        }
    }
}