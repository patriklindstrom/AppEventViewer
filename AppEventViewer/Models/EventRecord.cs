using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management;


namespace AppEventViewer.Models
{
    public class EventRecord
    {
        
       public  EventRecord(ManagementObject vmi) {
            Category = vmi["Category"].ToString();
            ComputerName = vmi["ComputerName"].ToString();
            EventCode = vmi["EventCode"].ToString();
            EventType = vmi["EventType"].ToString();
            InsertionStrings = vmi["InsertionStrings"].ToString();
            Logfile = vmi["Logfile"].ToString();
            Message = vmi["Message"].ToString();
            RecordNumber = vmi["RecordNumber"].ToString();
            SourceName = vmi["SourceName"].ToString();
            TimeGenerated = vmi["TimeGenerated"].ToString();
            TimeWritten = vmi["TimeWritten"].ToString();
            Type = vmi["Type"].ToString();
        
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
        public string TimeWritten { get; set; }
        public string Type { get; set; }
    }
}