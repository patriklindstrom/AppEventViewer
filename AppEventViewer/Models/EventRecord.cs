using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management;


namespace AppEventViewer.Models
{
    public class EventRecord
    {
        EventRecord(ManagementObject vmi) {
            Category = vmi["Category"].ToString();
            Category = vmi["ComputerName"].ToString();
            Category = vmi["EventCode"].ToString();
            Category = vmi["EventType"].ToString();
            Category = vmi["Category"].ToString();
            Category = vmi["Category"].ToString();
            Category = vmi["Category"].ToString();
            Category = vmi["Category"].ToString();
            Category = vmi["Category"].ToString();
            Category = vmi["Category"].ToString();
            Category = vmi["Category"].ToString();
        
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