using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Work2Client
{
    class Program {
        static void Main(string[] args) {
            //Это адрес сервера и порт. Аналогичные должны быть записаны на сервере
            IPAddress serverAddress = IPAddress.Parse("127.0.0.1");
            int port = 1255;

            //Формируем сообщение
            DateTime time = DateTime.Now;
            Console.WriteLine("Моё время " + time.ToString("dd.MM.yyyy   HH:mm:ss"));

            /*
             * Отправляем сообщение на сервер
             */

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);   //Создаём UDP сокет
            IPEndPoint serverEndPoint = new IPEndPoint(serverAddress, port); //"Пункт назначения" пакета данных
            

            byte[] bytes = BitConverter.GetBytes(time.ToBinary());//Переводим текущее время на компьютере в массив байт, который будем отправлять
            socket.SendTo(bytes, serverEndPoint); //Отправляем получившийся массив байт

            //Создаём буфер байт и принимаем в него разницу во времени
            byte[] buffer = new byte[sizeof(long)];
            socket.Receive(buffer);
			socket.Close();

            //Переводим байты во время
            Console.WriteLine("Разница во времени с сервером: " + BitConverter.ToInt32(buffer, 0) + "ms");

            Console.ReadKey();
        }
    }
}
