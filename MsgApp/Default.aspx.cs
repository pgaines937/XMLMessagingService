using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Text;
using System.Web.Caching;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SendMessageButton_Click(object sender, EventArgs e)
    {
        MessagingServiceReference.ServiceClient messagingServiceReference = new MessagingServiceReference.ServiceClient();
        messagingServiceReference.sendMsg(SenderIDTextBox.Text, ReceiverIDTextBox.Text, Stopwatch.GetTimestamp().ToString(), SendMessageTextBox.Text);

    }

    protected void ReceiveMessageButton_Click(object sender, EventArgs e)
    {
        MessagingServiceReference.ServiceClient messagingServiceReference = new MessagingServiceReference.ServiceClient();
        string[] messageList = messagingServiceReference.receiveMsg(SenderIDTextBox.Text);
        StringBuilder messages = new StringBuilder();
        foreach (string message in messageList)
        {
            messages.AppendLine(message);
        }
        ReceiveMessageTextBox.Text = messages.ToString();
        Cache.Insert("MessagesKey", messages, null, DateTime.Now.AddSeconds(20), TimeSpan.Zero, CacheItemPriority.Default, CacheRemovedCallBack);

    }

    protected void DeleteMessagesButton_Click(object sender, EventArgs e)
    {
        MessagingServiceReference.ServiceClient messagingServiceReference = new MessagingServiceReference.ServiceClient();
        messagingServiceReference.deleteMsg(SenderIDTextBox.Text, ReceiverIDTextBox.Text);
        ReceiveMessageTextBox.Text = "";

    }

    private void CacheRemovedCallBack(string IndexKey, object value,
                                            CacheItemRemovedReason reason)
    { // remove the Cartridge from the cache if messages is removed
        Cache.Remove("MessagesKey");

    }
}
