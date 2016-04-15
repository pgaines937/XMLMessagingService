MessagingService

A simple messaging service to buffer messages in an XML file and a Web client to allow users to send and receive messages.

Ensure the following are loaded in Visual Studio 2015:

* MessagingService
* MsgApp

Run MsgApp first, and MessagingService second (view in browser).

MessagingService: A WSDL service that can buffer messages before the receivers fetch the messages. The messages are saved in an XML file within the service.

Service Operations: 

* Operation 1: sendMsg: It allows the client to send a string message to the messaging service, and the message will be stored in the database (XML file) with senderID, receiverID, and a time stamp.
* Inputs: string senderID, string receiverID, string timeStamp, string msg
* Output: void
* Operation 2: receiveMsg: It allows the client to receive all the messages send to the receiverID.
* Inputs: string receiverID
* Output: string[] an array of messages, with each element containing the related information: senderID, sending time, and message.
* Operation 3: deleteMsg: It allows the sender to delete all the messages sent to a given receiverID.
* Inputs: string senderID, string receiverID
* Output: int count, indicating how many messages are deleted.

MsgApp: An ASP .NET Web Site application (client) that can send messages to the Messaging service and receive messages from the service.

MsgApp demonstrates data caching for the received message. The message is buffered for 20 seconds. However, if the sender deletes the message, the cache must be removed.
