using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppEventViewer.Models
{
    /*
      <!-- Select statement with params @FromTime AND @ToTime -->
    <add key="SQLStatement1" value="SELECT  msdb.dbo.agent_datetime(h.run_date,h.run_time) as run_time,run_duration,run_status,   
         ,h.server,s.name as jobname,  h.step_name,  h.message
        FROM  msdb..sysjobs as s
          join  msdb..sysjobhistory as h ON h.job_id = s.job_id
          where  msdb.dbo.agent_datetime(h.run_date,h.run_time)between @FromTime AND @ToTime
          Order by  h.run_date desc, h.run_time desc;"/>
    <!-- Headers for the SQLStatement1. They much match the sqlstatements number of columns
    -->
  <add key="SQLStatement1_Headers_List" value="Time,Duration,Status,Server,JobName,StepName,Message"/>
     */
    public interface ISql1Record
    {
        DateTime TimeGenerated { get; set; }
        int Duration { get; set; }
        int Status { get; set; }
        string Server { get; set; }
        string JobName { get; set; }
        string StepName { get; set; }
        string Message { get; set; }
        string SearchTerm { get; set; }
    }


    public class Sql1Record:ISql1Record
    {
        public Sql1Record(DateTime timeGenerated, int duration, int status, string server, string jobName, string stepName, string message, string searchTerm)
        {
            TimeGenerated = timeGenerated;
            Duration = duration;
            Status = status;
            Server = server;
            JobName = jobName;
            StepName = stepName;
            Message = message;
            SearchTerm = searchTerm;
        }

        public DateTime TimeGenerated { get; set; }
        public int Duration { get; set; }
        public int Status { get; set; }
        public string Server { get; set; }
        public string JobName { get; set; }
        public string StepName { get; set; }
        public string Message { get; set; }
        public string SearchTerm { get; set; }
    }
}