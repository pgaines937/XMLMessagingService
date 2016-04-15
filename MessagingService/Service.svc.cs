using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace MessagingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        // It allows the client to send a string message to the messaging service, and the message will be stored in the database (XML file) with senderID, receiverID, and a time stamp.
        public void sendMsg(string senderID, string receiverID, string timeStamp, string msg)
        {
            var fLocation = HostingEnvironment.MapPath(@"~\App_Data\MessageQueue.xml");
            if (File.Exists(fLocation))
            {
                FileStream fState = new FileStream(fLocation, FileMode.Open, FileAccess.ReadWrite); // Open a text file and load it into XmlDocument: first open 
                XmlDocument messageQueue = new XmlDocument();
                messageQueue.Load(fState);
                fState.Close();                                                                 // It is important to close the file

                XmlDocument newMessage = new XmlDocument();
                newMessage.LoadXml("<message>" +
                "<senderID>" + senderID + "</senderID>" +
                "<receiverID>" + receiverID + "</receiverID>" +
                "<timeStamp>" + timeStamp + "</timeStamp>" +
                "<msg>" + msg + "</msg>" +
                "</message>");

                XmlNode importNode = messageQueue.ImportNode(newMessage.SelectSingleNode("/message"), true);
                messageQueue.DocumentElement.AppendChild(importNode);

                fState = new FileStream(fLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite);     // Open the text file again to overwrite it with the newly updated messageQueue
                messageQueue.Save(fState);
                fState.Close();
            }
            else {
                FileStream fState = new FileStream(fLocation, FileMode.CreateNew, FileAccess.ReadWrite); // Open a text file: first open
                XmlTextWriter writer = new XmlTextWriter(fState, System.Text.Encoding.Unicode); // Open an XML file: second open
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("messages");
                writer.WriteStartElement("message");
                writer.WriteElementString("senderID", senderID);
                writer.WriteElementString("receiverID", receiverID);
                writer.WriteElementString("timeStamp", timeStamp);
                writer.WriteElementString("msg", msg);
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Close();
                fState.Close();
            }
        }

        // It allows the client to receive all the messages sent to the receiverID.
        public string[] receiveMsg(string receiverID)
        {
            string msgout;
            string[] strValue = new string[100];

            List<Message> messageList = new List<Message>();

            var fLocation = HostingEnvironment.MapPath(@"~\App_Data\MessageQueue.xml");
            if (File.Exists(fLocation))
            {
                string xpathQuery;
                FileStream fS = new FileStream(fLocation, FileMode.Open);
                XmlDocument doc = new XmlDocument();
                doc.Load(fS);

                xpathQuery = "descendant::message[receiverID='" + receiverID + "']";

                XmlNodeList messages = doc.SelectNodes(xpathQuery);

                int i = 0;
                int j = 0;
                foreach (XmlNode child in messages.Item(i))
                {
                    if (child.NextSibling != null)
                    {
                        if (child.NextSibling.InnerText.Equals(receiverID))
                        {
                            msgout = child.NextSibling.NextSibling.NextSibling.InnerText;
                            strValue[j] = msgout;
                            j++;
                        }
                        i++;
                    }
                }
                fS.Close(); // It is important to close the file
            }

            return strValue;
        }


        // It allows the sender to delete all the messages sent to a given receiverID.
        public int deleteMsg(string senderID, string receiverID)
        {
            int count = 0;

            List<XmlNode> toRemove = new List<XmlNode>();

            var fLocation = HostingEnvironment.MapPath(@"~\App_Data\MessageQueue.xml");
            if (File.Exists(fLocation))
            {
                FileStream fS = new FileStream(fLocation, FileMode.Open);
                XmlDocument messageQueue = new XmlDocument();
                messageQueue.Load(fS);
                XmlNode messages = messageQueue.LastChild;
                XmlNodeList nodeList = messages.ChildNodes;

                foreach (XmlNode xmlElement in nodeList)
                {
                    string senderIDValue = xmlElement.FirstChild.InnerText;
                    string receiverIDValue = xmlElement.FirstChild.NextSibling.InnerText;
                    if (senderIDValue.Equals(senderID) && receiverIDValue.Equals(receiverID))
                    {
                        toRemove.Add(xmlElement);
                        count++;
                    }
                }

                foreach (XmlNode xmlElement in toRemove)
                {
                    XmlNode node = xmlElement.ParentNode;
                    node.RemoveChild(xmlElement);
                }

                fS.Close(); // It is important to close the file
            }
            return count;
        }
    }
}