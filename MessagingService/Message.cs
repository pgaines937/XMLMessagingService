using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MessagingService
{
    [DataContract]
    public class Message
    {
        private string senderID;
        private string receiverID;
        private string timeStamp;
        private string msg;

        public Message(string senderID, string receiverID, string timeStamp, string msg)
        {
            this.senderID = senderID;
            this.receiverID = receiverID;
            this.timeStamp = timeStamp;
            this.msg = msg;
        }
    }
}