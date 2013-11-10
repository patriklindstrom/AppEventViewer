using System;
using System.Collections;
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
       string Sql1Statement { get; }
       List<string> SqlHeaderList { get; }
       IEnumerable SQL1ServersToQuery { get; }
    }

    public   class AppConfig : IAppConfig
    {
        private const String DEFAULT_SQL_STATEMENT =
            @"SELECT  msdb.dbo.agent_datetime(h.run_date,h.run_time) as run_time,run_duration,run_status,h.server,s.name as jobname,  h.step_name,  h.message
        FROM  msdb..sysjobs as s
          join  msdb..sysjobhistory as h ON h.job_id = s.job_id
          where  msdb.dbo.agent_datetime(h.run_date,h.run_time)between @FromTime AND @ToTime
          Order by  h.run_date desc, h.run_time desc; ";
        public AppConfig()     
        {
            var  appSettings = new AppSettings();
            string baseApiUrl = appSettings.Get("BaseApiUrl","http://localhost:80/api/");
            _serversToQuery = (List<string>)appSettings.GetList("ListOfServers");
            _sql1Statement = appSettings.Get("SQLStatement1", DEFAULT_SQL_STATEMENT);
            _sql1HeaderList = (List<string>)appSettings.GetList("SQLStatement1");
            _filterTerm = (List<string>)appSettings.GetList("FilterTermList");
           // _sqlStatement = (SqlStatement) appSettings.
            //Fake two servers by talke localhost computer name and . that is local computer
            // _serversToQuery = new List<string> { System.Environment.MachineName, "." };
        }
        private static List<string> _serversToQuery;
        private static List<string> _filterTerm;
        private static string _sql1Statement;
        private static List<string> _sql1HeaderList;
        private IEnumerable _sql1ServersToQuery;

        public  List<string> FilterTerm
        {
            get { return _filterTerm; }
        }



        public String Sql1Statement
        {
            get { return _sql1Statement; }
        }

      
        public  List<string> SqlHeaderList
        {
            get { return _serversToQuery; }
        }

        public IEnumerable SQL1ServersToQuery
        {
            get { return _serversToQuery; }
            
        }
    }
}