using System.Net.Sockets;
using System.Net;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


int Interval = 1000;

string strSearch="";
Thread listen;
listen = new Thread(new ThreadStart(Listener));
listen.Start();



void Listener()
{
    char vvod;

    do
    {
        Console.Clear();
        Console.WriteLine("Client");
        Console.WriteLine("1 - Обувь");
        Console.WriteLine("2 - Продукты");
        Console.WriteLine("3 - Вино");
        Console.WriteLine("Выходи тут ->>ESC<<-");

        vvod = Console.ReadKey().KeyChar;
        switch (vvod)
        {
            case '1':
                {
                    //Console.Clear();
                    strSearch = "#Обувь";
                    Console.ReadLine();
                    break;
                }
            case '2':
                {
                    //Console.Clear();
                    strSearch = "#Продукты";
                    Console.ReadLine();
                    break;
                }
            case '3':
                {
                    //Console.Clear();
                    strSearch = "#Вино";
                    Console.ReadLine();
                    break;
                }
        }
    } while (vvod < '1' || vvod > '3');

    while (true)
    {
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 8080);

        soc.Bind(ipep);

        IPAddress ip = IPAddress.Parse("224.5.5.5");

        soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));

        byte[] buff = new byte[4096];

        soc.Receive(buff);
        if (Encoding.UTF8.GetString(buff).Contains(strSearch))
            Console.WriteLine(Encoding.UTF8.GetString(buff));
        soc.Close();
    }
}
//void SendMessageToServer(string message)
//{
//    Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

//    IPAddress ip = IPAddress.Parse("224.5.5.5");
//    IPEndPoint ipep = new IPEndPoint(ip, 8080);

//    byte[] data = Encoding.UTF8.GetBytes(message);
//    soc.SendTo(data, ipep);

//    soc.Close();
//}

