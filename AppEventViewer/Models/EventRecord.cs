using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management;


namespace AppEventViewer.Models
{
    public interface IEventRecord
    {
        string Category { get; set; }
        string ComputerName { get; set; }
        string EventCode { get; set; }
        string EventType { get; set; }
        string InsertionStrings { get; set; }
        string Logfile { get; set; }
        string Message { get; set; }
        string RecordNumber { get; set; }
        string SourceName { get; set; }
        string TimeGenerated { get; set; }
       // string TimeWritten { get; set; }
        string Type { get; set; }

        string SearchTerm { get; set; }
    }

    public class EventRecord : IEventRecord
    {
        private string[] _insertionStrings;

        public EventRecord(ManagementObject vmi)
        {
            if (vmi == null) return; //vmi can be null eg for testing
            Category = (vmi["Category"] != null) ? vmi["Category"].ToString() : String.Empty;
            ComputerName = (vmi["ComputerName"] != null) ? vmi["ComputerName"].ToString() : String.Empty;
            EventCode = (vmi["EventCode"] != null) ? vmi["EventCode"].ToString() : String.Empty;
            EventType = (vmi["EventType"] != null) ? vmi["EventType"].ToString() : String.Empty;


            Logfile = (vmi["Logfile"] != null) ? vmi["Logfile"].ToString() : String.Empty;
            Message = (vmi["Message"] != null) ? vmi["Message"].ToString() : String.Empty;
            RecordNumber = (vmi["RecordNumber"] != null) ? vmi["RecordNumber"].ToString() : String.Empty;
            SourceName = (vmi["SourceName"] != null) ? vmi["SourceName"].ToString() : String.Empty;
            TimeGenerated = (vmi["TimeGenerated"] != null) ? vmi["TimeGenerated"].ToString() : String.Empty;
          //  TimeWritten = (vmi["TimeWritten"] != null) ? vmi["TimeWritten"].ToString() : String.Empty;
            Type = (vmi["Type"] != null) ? vmi["Type"].ToString() : String.Empty;
            if (vmi["InsertionStrings"] != null )
            {
                var strList = (String[]) vmi["InsertionStrings"];
                foreach (var insString in strList)
                {
                    this.InsertionStrings  += " " + insString ;
                }
            }
            else
            {
                this.InsertionStrings = String.Empty;
            }
        }

        public string Category { get; set; }
        public string ComputerName { get; set; }
        public string EventCode { get; set; }
        public string EventType { get; set; }
        public string InsertionStrings { get; set; }
        public string Logfile { get; set; }
        public string Message { get; set; }
        public string RecordNumber { get; set; }
        public string SourceName { get; set; }
        public string TimeGenerated { get; set; }
      //  public string TimeWritten { get; set; }
        public string Type { get; set; }
        public string SearchTerm { get; set; }

    }
}