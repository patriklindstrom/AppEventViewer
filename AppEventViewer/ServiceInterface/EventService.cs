using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web;
using AppEventViewer.Models;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace AppEventViewer.ServiceInterface
{
    //Define Request and Response DTOs


    /// <summary>
    /// The Request DTO for the Get EventRecordList DTO
    /// lag is in hours
    /// </summary>
    /// 
    [Route("/Events/{lag}")]
    public class EventRecordList
    {
        public int lag { get; set; }
        //public string SourceNameFilter { get; set; }
        //public DateTime FromDate { get; set; }
        //public DateTime ToDate { get; set; }
        //public int MaxRows { get; set; }
    }
    /// <summary>
    /// The Request DTO for the Get EventRecordList DTO
    /// </summary>
    public class EventRecordListResponse
    {
        public List<EventRecord> EventRecords { get; set; }
    }

    //Implementation
    public class EventService : Service
    {
        public object Get(EventRecordList request)
        {
            DateTime FromTime = DateTime.Now.AddHours(-1*request.lag);
            string strFromTime = String.Format("{0:yyyyMMddHHmmss}", FromTime) + ".000000+000";
            string wmiQuery =
            String.Format("SELECT * FROM Win32_NTLogEvent WHERE Logfile = 'Application' AND TimeGenerated > '{0}'",
                  strFromTime);
            var mos = new ManagementObjectSearcher(wmiQuery);
             object o;
            List<EventRecord> EventRecordList = new List<EventRecord>();
            foreach (ManagementObject mo in mos.Get())
            {
                EventRecordList.Add(new EventRecord(mo));
            }

            return EventRecordList;
        }



        public object Any(EventRecordList request)
        {
            EventRecordListResponse eventRecordListResponseDTO = null;
          //  eventRecordListResponseDTO.EventRecords.
            return  eventRecordListResponseDTO ;
            //C# client can call with:
            //var response = client.Get(new Hello { Name = "ServiceStack" });
        }
        }
}