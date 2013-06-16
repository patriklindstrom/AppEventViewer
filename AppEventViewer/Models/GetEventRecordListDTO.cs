using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppEventViewer.Models
{
    /// <summary>
    /// The Request DTO for the Get EventRecordList DTO
    /// </summary>
    public class EventRecordListRequestDTO
    {
        public string SourceNameFilter { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int MaxRows { get; set; } 
    }
    /// <summary>
    /// The Request DTO for the Get EventRecordList DTO
    /// </summary>
    public class EventRecordListResponseDTO
    {
        public List<EventRecord> EventRecords { get; set; }
    }
}