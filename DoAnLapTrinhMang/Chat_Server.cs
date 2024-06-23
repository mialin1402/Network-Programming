using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace DoAnLapTrinhMang
{
    public partial class Chat_Server : Form
    {
        public Chat_Server()
        {
            InitializeComponent();
        }

        TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
        private List<Chat_User> users = new List<Chat_User>();
        private Dictionary<int, TcpClient> connectedUsers = new Dictionary<int, TcpClient>();

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                lbLogs.Items.Clear();
                listener.Start();
                lbLogs.Items.Add("Server started. Waiting for clients...");

                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    lbLogs.Items.Add($"Client connected:{client.Client.RemoteEndPoint}");

                    Thread clientThread = new Thread(() => ReceiveClientMessage(client));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                lbLogs.Items.Clear();
            }

        }

        private void ReceiveClientMessage(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int bytesRead;
            try
            {
                while (client.Connected)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) { break; }
                    string jsonMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Chat_Message? userMessage = JsonSerializer.Deserialize<Chat_Message>(jsonMessage);
                    if (userMessage == null) break;
                    if (userMessage.type.Equals("USER_INFO"))
                    {
                        try
                        {
                            string userInfo = Encoding.UTF8.GetString(userMessage.data);
                            Chat_User? newUser = JsonSerializer.Deserialize<Chat_User>(userInfo);
                            if (newUser != null)
                            {
                                users.Add(newUser);
                            }
                            else
                            {
                                break;
                            }
                            lbUsers.Items.Add(newUser.Name);
                            connectedUsers.Add(newUser.ID, client);
                            string allUsersJson = JsonSerializer.Serialize(users);
                            byte[] allUsersData = Encoding.UTF8.GetBytes(allUsersJson);
                            Chat_Message serverMassage = new Chat_Message("USER_INFO", "", allUsersData);
                            SendClientMessage(serverMassage);
                        }
                        catch
                        {
                            MessageBox.Show("không thể tạo user mới", "Chat App Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        SendClientMessage(userMessage);
                    }
                }
                listener.Stop();
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chat App Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                stream.Close();
                client.Close();
                return;
            }

        }

        private void SendClientMessage(Chat_Message sendData)
        {
            if (sendData.type.Equals("USER_INFO"))
            {
                // Gửi "info" cho tất cả các client đã kết nối
                String jsonMessage = JsonSerializer.Serialize(sendData);
                byte[] messageBuffer = Encoding.UTF8.GetBytes(jsonMessage);

                foreach (int userID in connectedUsers.Keys)
                {
                    TcpClient client = connectedUsers[userID];
                    NetworkStream stream = client.GetStream();
                    stream.Write(messageBuffer, 0, messageBuffer.Length);
                }
            }
            else if (sendData.type.Equals("TEXT"))
            {
                string[] messageSendTo = sendData.fromto.Split(">");
                int senderID = int.Parse(messageSendTo[0]);
                int receiverID = int.Parse(messageSendTo[1]);
                String message = JsonSerializer.Serialize(sendData);
                byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                TcpClient tcpClient = connectedUsers[receiverID];
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(messageBuffer, 0, messageBuffer.Length);

                String senderName = String.Empty;
                String receiverName = String.Empty;
                foreach (Chat_User user in users)
                {
                    if (user.ID == senderID)
                    {
                        senderName = user.Name;
                    }
                    else if (user.ID == receiverID)
                    {
                        receiverName = user.Name;
                    }
                }
                lbLogs.Items.Add($"{senderName} send text to {receiverName}");

            }
            else if (sendData.type.Equals("FILE"))
            {
                string[] messageSendTo = sendData.fromto.Split(">");
                int senderID = int.Parse(messageSendTo[0]);
                int receiverID = int.Parse(messageSendTo[1]);
                String message = JsonSerializer.Serialize(sendData);
                byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                TcpClient tcpClient = connectedUsers[receiverID];
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(messageBuffer, 0, messageBuffer.Length);

                String senderName=String.Empty;
                String receiverName=String.Empty;
                foreach (Chat_User user in users)
                {
                    if (user.ID == senderID)
                    {
                        senderName = user.Name;
                    } else if (user.ID == receiverID)
                    {
                        receiverName = user.Name;
                    }
                }
                lbLogs.Items.Add($"{senderName} send file to {receiverName}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            listener.Stop();
            lbLogs.Items.Clear();
            lbUsers.Items.Clear();
            connectedUsers.Clear();
            lbLogs.Items.Add("server closed");
        }

        private void Chat_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            listener.Stop();
            lbLogs.Items.Clear();
            lbUsers.Items.Clear();
            connectedUsers.Clear();
            lbLogs.Items.Add("server closed");
        }
    }
}

