using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MessagingService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void sendMsg(string senderID, string receiverID, string timeStamp, string msg);

        [OperationContract]
        string[] receiveMsg(string receiverID);

        [OperationContract]
        int deleteMsg(string senderID, string receiverID);
    }
}
