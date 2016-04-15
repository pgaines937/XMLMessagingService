<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ OutputCache Duration="20" VaryByParam="None" Location ="Client" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <h1>Messaging Service Test Client</h1>
    </div>

    <div class="row">
        <table>
            <tr style="text-align: center">
                <td>Receiver ID</td>
                <td>
                    <asp:TextBox ID="ReceiverIDTextBox" Text="0" Style="vertical-align: middle; " runat="server" Width="279px"></asp:TextBox>
                    <asp:Button ID="DeleteMessagesButton" runat="server" Text="Delete Messages" OnClick="DeleteMessagesButton_Click" Width="175px" />
                </td>
            </tr>
            <tr style="text-align: center">
                <td>Message: </td>
                <td>
                    <asp:TextBox ID="SendMessageTextBox" runat="server" Height="100px" Width="456px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="SendMessageButton" runat="server" Text="Send This Message" OnClick="SendMessageButton_Click" /></td>
            </tr>
        </table>
    </div>


    <div class="row">
        &nbsp;
                <table>
                    <tr style="text-align: center">
                        <td>My ID:</td>
                        <td>
                            <asp:TextBox ID="SenderIDTextBox" Text="1" Style="vertical-align: middle; " runat="server" Width="294px"></asp:TextBox>
                            <asp:Button ID="ReceiveMessageButton" runat="server" Text="Receive Messages" OnClick="ReceiveMessageButton_Click" Width="157px" /></td>
                        <td></td>
                    </tr>
                    <tr style="text-align: center">
                        <td>Display all
                            <br />
                            messages to me: </td>
                        <td>
                    <asp:TextBox ID="ReceiveMessageTextBox" runat="server" Height="100px" Width="456px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
    </div>

</asp:Content>
