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
            Category =  (vmi["Category"] != null) ?  vmi["Category"].ToString() : String.Empty;
            ComputerName = (vmi["ComputerName"]!= null) ? vmi["ComputerName"].ToString() : String.Empty;
            EventCode = (vmi["EventCode"]!= null) ? vmi["EventCode"].ToString() : String.Empty;
            EventType = (vmi["EventType"]!= null) ? vmi["EventType"].ToString() : String.Empty;
            InsertionStrings = (vmi["InsertionStrings"]!= null) ? vmi["InsertionStrings"].ToString() : String.Empty;
            Logfile = (vmi["Logfile"]!= null) ? vmi["Logfile"].ToString() : String.Empty;
            Message = (vmi["Message"]!= null) ? vmi["Message"].ToString() : String.Empty;
            RecordNumber = (vmi["RecordNumber"]!= null) ? vmi["RecordNumber"].ToString() : String.Empty;
            SourceName = (vmi["SourceName"]!= null) ? vmi["SourceName"].ToString() : String.Empty;
            TimeGenerated = (vmi["TimeGenerated"]!= null) ? vmi["TimeGenerated"].ToString() : String.Empty;
            TimeWritten = (vmi["TimeWritten"]!= null) ? vmi["TimeWritten"].ToString() : String.Empty;
            Type = (vmi["Type"] != null) ? vmi["Type"].ToString() : String.Empty; 
        
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