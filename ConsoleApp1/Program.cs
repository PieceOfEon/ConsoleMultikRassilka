using System.Net.Sockets;
using System.Net;
using System.Text;

string[] phrases = {
    "\"#Обувь\nХорошая обувь может изменить ваше настроение, даже целый день. Выберите качественную обувь и наслаждайтесь комфортом на протяжении всего дня!\"",
            "\"#Обувь\nОбувь - это не просто модный аксессуар, это часть вашей гардеробной составляющей. Подберите пару, которая отражает ваш стиль и уникальность.\"",
            "\"#Обувь\nКогда вы носите качественную обувь, вы можете быть уверены в своей безопасности и комфорте. В магазине обуви мы предлагаем только лучшее качество и удобство!\"",
            "\"#Обувь\nНе стесняйтесь экспериментировать с различными стилями и цветами обуви. Иногда небольшое изменение может сделать большую разницу в вашем образе!\"",
            "\"#Обувь\nВ магазине обуви мы заботимся о вашем здоровье и комфорте. Приходите к нам, чтобы подобрать идеальную пару обуви для ваших потребностей и стиля жизни.\"",
            "\"#Вино\nПервую чашу пьём мы для утоления жажды, вторую - для увеселения, третью - для наслаждения, а четвёртую - для сумасшествия.\"",
            "\"#Вино\nВино скотинит и зверит человека, ожесточает его и отвлекает от светлых мыслей, тупит его.\"",
            "\"#Вино\nЧеловечество могло бы достигнуть невероятных успехов, если бы оно было более трезвым.\"",
            "\"#Вино\nПьянство — это добровольное сумасшествие.\"",
            "\"#Вино\nМного вина — мало ума.\"",
            "\"#Продукты\nКачество продуктов - это ключевой фактор для здоровья и благополучия. В нашем магазине вы найдете только самые свежие и качественные продукты!\"",
            "\"#Продукты\nПокупайте продукты у нас и получайте больше, чем просто еду. Мы предлагаем широкий ассортимент продуктов и помогаем вам делать лучший выбор для своего здоровья!\"",
            "\"#Продукты\nВ нашем магазине вы найдете все, что нужно для здорового и сбалансированного питания. Мы тщательно отбираем продукты, чтобы предложить вам лучшие варианты!\"",
            "\"#Продукты\nЗабота о клиентах - наш главный приоритет. Мы уверены, что вы найдете все, что вам нужно для своего питания в нашем магазине!\"",
            "\"#Продукты\nПродуктовый магазин - это не просто место, где можно купить продукты. Это место, где вы можете получить знания о том, как правильно питаться и жить здорово!\""

};

int Interval = 200;

Thread Sender = new Thread(new ThreadStart(multicastSend));

Sender.Start();

void multicastSend()
{
    Console.WriteLine("Server");
    Random rnd = new Random();
    while (true)
    {
        Thread.Sleep(Interval);
        string message = phrases[rnd.Next(phrases.Length)];
        Console.WriteLine("Sending message: " + message);
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);

        IPAddress dast = IPAddress.Parse("224.5.5.5");

        soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(dast));

        IPEndPoint ipep = new IPEndPoint(dast, 8080);

        soc.Connect(ipep);
        soc.Send(Encoding.UTF8.GetBytes(message));
        soc.Close();
    }
}

//Thread Receiver = new Thread(new ThreadStart(multicastReceive));

//Receiver.Start();

//void multicastReceive()
//{
//    Console.WriteLine("Listening for messages...");
//    Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//    IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 8080);
//    listener.Bind(localEP);
//    IPAddress multicastIP = IPAddress.Parse("224.5.5.5");
//    listener.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastIP, IPAddress.Any));
//    byte[] buffer = new byte[1024];
//    while (true)
//    {
//        int bytesRead = listener.Receive(buffer);
//        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//        Console.WriteLine("Received message: " + message);
//    }
//}

