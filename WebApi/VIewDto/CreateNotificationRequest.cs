using System;

namespace WebApi.VIewDto
{
    public class Notifications
    {
        public string NotificationType { get; set; }

        public DateTime RaisedTime { get; set; }

        public int UserLinkId { get; set; }

        public string Payload { get; set; }
    }


}
