using System.Net;
using System.Net.Sockets;

namespace DoAnLapTrinhMang
{
    public delegate void TransferEventHandler(object sender, SendFile_Queue queue);
    public delegate void ConnectCallback(object sender, string error);

    public class SendFile_Transfer
    {
        private Socket baseSocket;
        private byte[] buffer_size = new byte[65536];
        private ConnectCallback _connectCallback;
        private Dictionary<int, SendFile_Queue> transfers = new Dictionary<int, SendFile_Queue>();

        public Dictionary<int, SendFile_Queue> Transfers
        {
            get { return transfers; }
        }
        public bool Closed
        {
            get;
            private set;
        }

        public string OutputFolder
        {
            get;
            set;
        }
        public IPEndPoint EndPoint
        {
            get;
            private set;
        }

        public event TransferEventHandler Queued; 
        public event TransferEventHandler ProgressChanged; 
        public event TransferEventHandler Stopped; 
        public event TransferEventHandler Complete;
        public event EventHandler Disconnected; 
        public SendFile_Transfer()
        {
            baseSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public SendFile_Transfer(Socket sock)
        {
            baseSocket = sock;
            EndPoint = (IPEndPoint)baseSocket.RemoteEndPoint;
        }

        public void Connect(string hostName, int port, ConnectCallback callback)
        {
            _connectCallback = callback;
            baseSocket.BeginConnect(hostName, port, connectCallback, null);
        }

        private void connectCallback(IAsyncResult ar)
        {
            string error = null;
            try 
            {
                baseSocket.EndConnect(ar);
                EndPoint = (IPEndPoint)baseSocket.RemoteEndPoint;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            _connectCallback(this, error);
        }

        public void Run()
        {
            try
            {
                baseSocket.BeginReceive(buffer_size, 0, buffer_size.Length, SocketFlags.Peek, receiveCallback, null);
            }
            catch
            {
                Close();
            }
        }

        public void QueueTransfer(string fileName)
        {
            try
            {
                SendFile_Queue queue = SendFile_Queue.CreateUploadQueue(this, fileName);
                transfers.Add(queue.ID, queue);
                PacketWriter pw = new PacketWriter();
                pw.Write((byte)SendFile_Headers.Queue);
                pw.Write(queue.ID);
                pw.Write(queue.Filename);
                pw.Write(queue.Length);
                Send(pw.GetBytes());
                if (Queued != null)
                {
                    Queued(this, queue);
                }
            }
            catch
            {
            }
        }

        public void StartTransfer(SendFile_Queue queue)
        {
            PacketWriter pw = new PacketWriter();
            pw.Write((byte)SendFile_Headers.Start);
            pw.Write(queue.ID);
            Send(pw.GetBytes());
        }

        public void StopTransfer(SendFile_Queue queue)
        {
            if (queue.Type == QueueType.Upload)
            {
                queue.Stop();
            }

            PacketWriter pw = new PacketWriter();
            pw.Write((byte)SendFile_Headers.Cancel);
            pw.Write(queue.ID);
            Send(pw.GetBytes());
            queue.Close();
        }

        public void PauseTransfer(SendFile_Queue queue)
        {
            queue.Pause();

            PacketWriter pw = new PacketWriter();
            pw.Write((byte)SendFile_Headers.Pause);
            pw.Write(queue.ID);
            Send(pw.GetBytes());
        }

        public int GetOverallProgress()
        {
            int overall = 0;
            try
            {
                foreach (KeyValuePair<int, SendFile_Queue> pair in transfers)
                {
                    overall += pair.Value.Progress;
                }

                if (overall > 0)
                {
                    overall = (overall * 100) / (transfers.Count * 100);
                }
            }
            catch { overall = 0; }

            return overall;
        }

        public void Send(byte[] data)
        {
            if (Closed) return;

            lock (this)
            {
                try
                {
                    //Send the size of the packet.
                    baseSocket.Send(BitConverter.GetBytes(data.Length), 0, 4, SocketFlags.None);
                    //And then the actual packet.
                    baseSocket.Send(data, 0, data.Length, SocketFlags.None);
                }
                catch
                {
                    Close();
                }
            }
        }

        public void Close()
        {
            if (Closed) return;
            Closed = true;
            baseSocket.Close(); 
            transfers.Clear(); 
            transfers = null;
            buffer_size = null;
            OutputFolder = null;

            if (Disconnected != null)
                Disconnected(this, EventArgs.Empty);
        }

        private void process()
        {
            PacketReader pr = new PacketReader(buffer_size);

            SendFile_Headers header = (SendFile_Headers)pr.ReadByte();

            switch (header)
            {
                case SendFile_Headers.Queue:
                    {
                        int id = pr.ReadInt32();
                        string fileName = pr.ReadString();
                        long length = pr.ReadInt64();
                        SendFile_Queue queue = 
                            SendFile_Queue.CreateDownloadQueue(this, id,
                                Path.Combine(OutputFolder, Path.GetFileName(fileName)), length);

                        transfers.Add(id, queue);

                        if (Queued != null)
                        {
                            Queued(this, queue);
                        }
                    }
                    break;
                case SendFile_Headers.Start:
                    {
                        int id = pr.ReadInt32();

                        if (transfers.ContainsKey(id))
                        {
                            transfers[id].Start();
                        }
                    }
                    break;
                case SendFile_Headers.Cancel:
                    {
                        int id = pr.ReadInt32();

                        if (transfers.ContainsKey(id))
                        {
                            SendFile_Queue queue = transfers[id];

                            queue.Stop();
                            queue.Close();

                            if (Stopped != null) Stopped(this, queue);

                            transfers.Remove(id);
                        }
                    }
                    break;
                case SendFile_Headers.Pause:
                    {
                        int id = pr.ReadInt32();

                        if (transfers.ContainsKey(id))
                        {
                            transfers[id].Pause();
                        }
                    }
                    break;
                case SendFile_Headers.Chunk:
                    {
                        int id = pr.ReadInt32();
                        long index = pr.ReadInt64();
                        int size = pr.ReadInt32();
                        byte[] buffer = pr.ReadBytes(size);

                        SendFile_Queue queue = transfers[id];

                        queue.Write(buffer, index);

                        queue.Progress = (int)((queue.Transferred * 100) / queue.Length);

                        if (queue.LastProgress < queue.Progress)
                        {
                            queue.LastProgress = queue.Progress;

                            if (ProgressChanged != null)
                            {
                                ProgressChanged(this, queue);
                            }

                            if (queue.Progress == 100)
                            {
                                queue.Close();

                                if (Complete != null)
                                {
                                    Complete(this, queue);
                                }
                            }
                        }
                    }
                    break;
            }
            pr.Dispose();
        }

        private void receiveCallback(IAsyncResult ar)
        {
            try
            {
                int found = baseSocket.EndReceive(ar);
                if (found >= 4)
                {
                    baseSocket.Receive(buffer_size, 0, 4, SocketFlags.None);
                    int size = BitConverter.ToInt32(buffer_size, 0);
                    int read = baseSocket.Receive(buffer_size, 0, size, SocketFlags.None);

                    while (read < size)
                    {
                        read += baseSocket.Receive(buffer_size, read, size - read, SocketFlags.None);
                    }

                    process();
                }

                Run();
            }
            catch
            {
                Close();
            }
        }

        internal void callProgressChanged(SendFile_Queue queue)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, queue);
            }
        }
    }
}