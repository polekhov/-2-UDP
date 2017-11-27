using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Work2Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 1255;

            UdpClient socket = new UdpClient(port); //Создаём сокет
            IPEndPoint senderEndPoint = null; //Здесь будет в результате находиться адрес отправителя

            Console.WriteLine("Ожидаю сообщения");
            byte[] buffer = socket.Receive(ref senderEndPoint); //Принимаем сообщение
            DateTime clientTime = DateTime.FromBinary(BitConverter.ToInt64(buffer, 0)); //Переводим массив байт в DateTime
            Console.WriteLine("Время на клиенте: " + clientTime.ToString("dd.MM.yyyy HH:mm:ss"));
            DateTime time = DateTime.Now; //Получаем текущее время на сервере
            Console.WriteLine("Время на сервере: " + time.ToString("dd.MM.yyyy HH:mm:ss"));
            TimeSpan dtime = time - clientTime;
            long dtimeMillis = dtime.Ticks / 10000;
            Console.WriteLine("Разница во времени " + dtimeMillis + " ms");
            buffer = BitConverter.GetBytes(dtimeMillis);
            socket.Send(buffer, buffer.Length, senderEndPoint);
            Console.ReadKey();
        }
    }
}
