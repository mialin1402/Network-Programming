namespace DoAnLapTrinhMang
{
    public enum QueueType : byte
    {
        Download,
        Upload
    }

    public class SendFile_Queue
    {
        public static SendFile_Queue CreateUploadQueue(SendFile_Transfer client, string fileName)
        {
            try
            {
                var queue = new SendFile_Queue();
                queue.Filename = Path.GetFileName(fileName);
                queue.Client = client;
                queue.Type = QueueType.Upload;
                queue.FS = new FileStream(fileName, FileMode.Open);
                queue.Thread = new Thread(new ParameterizedThreadStart(transferProc));
                queue.Thread.IsBackground = true;
                queue.ID = new Random().Next();
                queue.Length = queue.FS.Length;
                return queue;
            }
            catch
            {
                return null;
            }
        }

        public static SendFile_Queue CreateDownloadQueue(SendFile_Transfer client, int id, string saveName, long length)
        {
            try
            {
                var queue = new SendFile_Queue();
                queue.Filename = Path.GetFileName(saveName);
                queue.Client = client;
                queue.Type = QueueType.Download;
                queue.FS = new FileStream(saveName, FileMode.Create);
                queue.FS.SetLength(length);
                queue.Length = length;
                queue.ID = id;
                return queue;
            }
            catch
            {
                return null;
            }
        }

        // * 1 Byte = Header
        // * 4 Bytes = ID
        // * 8 Bytes = Index
        // * 4 Bytes = read
        // ==> file buffer = buffer - 17
        private const int FILE_BUFFER_SIZE = 65536 - 17;
        private static byte[] file_buffer = new byte[FILE_BUFFER_SIZE];
        private ManualResetEvent pauseEvent;
        public int ID;
        public int Progress, LastProgress;
        public long Transferred;
        public long Index;
        public long Length;

        public bool Running;
        public bool Paused;

        public string Filename;

        public QueueType Type;
        public SendFile_Transfer Client;
        public Thread Thread;
        public FileStream FS;

        private SendFile_Queue()
        {
            pauseEvent = new ManualResetEvent(true);
            Running = true;
        }

        public void Start()
        {
            Running = true;
            Thread.Start(this);
        }

        public void Stop()
        {
            Running = false;
        }

        public void Pause()
        {
            if (!Paused)
            {
                pauseEvent.Reset();
            }
            else
            {
                pauseEvent.Set();
            }

            Paused = !Paused;
        }

        public void Close()
        {
            try
            {
                Client.Transfers.Remove(ID);
            }
            catch { }
            Running = false;
            FS.Close();
            pauseEvent.Dispose();

            Client = null;
        }

        public void Write(byte[] bytes, long index)
        {
            lock (this)
            {
                FS.Position = index;
                FS.Write(bytes, 0, bytes.Length);
                Transferred += bytes.Length;
            }
        }

        private static void transferProc(object o)
        {
            SendFile_Queue queue = (SendFile_Queue)o;

            while (queue.Running && queue.Index < queue.Length)
            {
                queue.pauseEvent.WaitOne();

                if (!queue.Running)
                {
                    break;
                }
                lock (file_buffer)
                {
                    queue.FS.Position = queue.Index;

                    int total_read_bytes = queue.FS.Read(file_buffer, 0, file_buffer.Length);

                    PacketWriter pw = new PacketWriter();

                    pw.Write((byte)SendFile_Headers.Chunk);
                    pw.Write(queue.ID);
                    pw.Write(queue.Index);
                    pw.Write(total_read_bytes);
                    pw.Write(file_buffer, 0, total_read_bytes);
                    queue.Transferred += total_read_bytes;
                    queue.Index += total_read_bytes;

                    queue.Client.Send(pw.GetBytes());

                    queue.Progress = (int)((queue.Transferred * 100) / queue.Length);

                    if (queue.LastProgress < queue.Progress)
                    {
                        queue.LastProgress = queue.Progress;

                        queue.Client.callProgressChanged(queue);
                    }
                }
            }
            queue.Close();
        }
    }
}