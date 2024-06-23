using System.Net.Sockets;

namespace DoAnLapTrinhMang
{
    public class Chat_User
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Chat_User(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public override string ToString()
        {
            return $"ID: {ID}\nName: {Name}";
        }
    }
}
