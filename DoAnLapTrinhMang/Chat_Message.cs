using System.Net.Sockets;
using System.Text;

namespace DoAnLapTrinhMang
{
    internal class Chat_Message
    {
        public string type { get; set; }
        public string fromto { get; set; }
        public byte[] data { get; set; } 
        public string filename { get; set; }

        public Chat_Message() { }
        public Chat_Message(string type, string fromto, byte[] data) 
        {
            this.type = type;
            this.fromto = fromto;
            this.data = data;
            this.filename = string.Empty;
        }

        public Chat_Message(string type, string fromto, byte[] data, string filepath) : this(type, fromto, data)
        {
            this.filename = filepath;
        }
    }
}
