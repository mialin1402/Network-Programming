using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace DoAnLapTrinhMang
{
    public partial class Chat_Client : Form
    {
        public Chat_Client()
        {
            InitializeComponent();
            btnSend.Enabled = false;
        }

        private TcpClient tcpClient = new TcpClient();
        private NetworkStream networkStream;
        private Chat_User Sender;
        private List<Chat_User> connectedUsers = new List<Chat_User>();

        private async Task SendAsync(byte[] data)
        {
            if (tcpClient.Connected)
            {
                await networkStream.WriteAsync(data, 0, data.Length);
            }
        }

        private async Task SendUserInfoAsync(Chat_User user)
        {
            string userInfo = JsonSerializer.Serialize(user);
            byte[] data = Encoding.UTF8.GetBytes(userInfo);
            Chat_Message info = new Chat_Message("USER_INFO", "", data);
            string jsonMessage = JsonSerializer.Serialize(info);
            await SendAsync(Encoding.UTF8.GetBytes(jsonMessage));
        }

        public async Task SendMessageAsync(string textMessage, Chat_User receiver)
        {
            try
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(textMessage);
                Chat_Message message = new Chat_Message("TEXT", $"{Sender.ID}>{receiver.ID}", messageBytes);
                string jsonMessage = JsonSerializer.Serialize(message);
                await SendAsync(Encoding.UTF8.GetBytes(jsonMessage));
            }
            catch
            {
                MessageBox.Show("Lỗi ở SendMessageAsync(): ", "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public async Task SendFileAsync(string filePath, Chat_User receiver)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            Chat_Message message = new Chat_Message("FILE", $"{Sender.ID}>{receiver.ID}", fileBytes, Path.GetFileName(filePath));
            string jsonMessage = JsonSerializer.Serialize(message);
            await SendAsync(Encoding.UTF8.GetBytes(jsonMessage));
            await SendMessageAsync(filePath, receiver);
        }

        private async void ReceiveMessagesAsync()
        {
            byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
            int bytesRead;
            try
            {
                while (tcpClient.Connected)
                {
                    bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string jsonMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Chat_Message? message = JsonSerializer.Deserialize<Chat_Message>(jsonMessage);
                    if (message == null) break;
                    if (message.type == "USER_INFO")
                    {
                        connectedUsers.Clear();
                        cbUsers.Items.Clear();
                        List<Chat_User> newUser = JsonSerializer.Deserialize<List<Chat_User>>(message.data);
                        foreach (Chat_User user in newUser)
                        {
                            if (user.ID == Sender.ID) continue;
                            connectedUsers.Add(user);
                            cbUsers.Items.Add(user.Name);
                        }

                    }
                    else if (message.type == "TEXT")
                    {
                        string messageContent = System.Text.Encoding.UTF8.GetString(message.data);
                        string[] messageSendTo = message.fromto.Split(">");
                        int senderID = int.Parse(messageSendTo[0]);
                        int receiverID = int.Parse(messageSendTo[1]);
                        String senderName = String.Empty;
                        foreach (Chat_User user in connectedUsers)
                        {
                            if (user.ID == senderID)
                            {
                                senderName = user.Name;
                            }
                        }
                        chatMessages.Items.Add($"{senderName}: {messageContent}");

                    }
                    else if (message.type == "FILE")
                    {
                        try
                        {
                            string fileName = message.filename;
                            string outputFolder = $"{tbUserName.Text}";
                            if (!Directory.Exists(outputFolder))
                            {
                                Directory.CreateDirectory(outputFolder);
                            }
                            string filepath = Path.Combine(outputFolder, fileName);
                            File.WriteAllBytes(filepath, message.data);
                            MessageBox.Show("File đã được lưu thành công!", "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            MessageBox.Show("Lưu File thất bại", "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ở ReceiveMessagesAsync(): " + ex.Message, "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserName.Text) ||
                string.IsNullOrEmpty(tbServerPort.Text) ||
                string.IsNullOrEmpty(tbServerIP.Text))
            {
                MessageBox.Show("Hãy nhập đủ thông tin", "Chat App", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                await tcpClient.ConnectAsync(tbServerIP.Text, int.Parse(tbServerPort.Text));
                networkStream = tcpClient.GetStream();

                btnSend.Enabled = true;
                btnConnect.Enabled = false;

                this.Sender = new Chat_User(new Random().Next(), tbUserName.Text);
                await SendUserInfoAsync(this.Sender);

                Thread receiveThread = new Thread(ReceiveMessagesAsync);
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến server", "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtbMessage.Text))
            {
                MessageBox.Show("Hãy nhập nội dung muốn gửi", "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Hãy chọn người muốn gửi", "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            chatMessages.Items.Add($"{tbUserName.Text}: {rtbMessage.Text}");
            try
            {
                bool found = false;
                foreach (Chat_User user in connectedUsers)
                {
                    if (user.Name.Equals(cbUsers.Text))
                    {
                        found = true;
                        await SendMessageAsync(rtbMessage.Text, user);
                        break;
                    }
                }
                if (found == false)
                {
                    MessageBox.Show($"Không tìm thấy người dùng {cbUsers.Text}", "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                rtbMessage.Text = string.Empty;
            }
            catch
            {
                MessageBox.Show("Không thể gửi tin nhắn", "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void btnSendFile_Click(object sender, EventArgs e)
        {
            if (cbUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Hãy chọn người muốn gửi", "Chat App Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select a File";
                openFileDialog.Filter = "All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    foreach (Chat_User user in connectedUsers)
                    {
                        if (user.Name.Equals(cbUsers.Text))
                        {
                            await SendFileAsync(filePath, user);
                            chatMessages.Items.Add($"{Sender.Name}: {filePath}");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Chat_Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcpClient.Connected) tcpClient.Close();
            networkStream.Close();
        }
    }
}
