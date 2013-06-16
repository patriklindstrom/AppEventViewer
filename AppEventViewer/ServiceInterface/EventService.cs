using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppEventViewer.Models;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace AppEventViewer.ServiceInterface
{
            [Route("/Events/{time}")]
    public class EventService : Service
    {


        public object Any(EventRecordListRequestDTO request)
        {
             EventRecordListResponseDTO eventRecordListResponseDTO = null;
            eventRecordListResponseDTO.EventRecords.
            return  eventRecordListResponseDTO ();

            //C# client can call with:
            //var response = client.Get(new Hello { Name = "ServiceStack" });
        }
        }
}