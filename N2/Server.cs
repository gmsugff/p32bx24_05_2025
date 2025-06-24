using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace N2
{
    internal class Server
    {
        private delegate void ConnectDelegate(Socket s);
        private delegate void StartNetwork(Socket s);
        private Socket socket;
        private IPEndPoint endP;

        // Для обновления UI
        private Action<string> logAction;

        public Server(string strAddr, int port, Action<string> logAction)
        {
            this.logAction = logAction;
            endP = new IPEndPoint(IPAddress.Parse(strAddr), port);
        }

        private void Server_Connect(Socket s)
        {
            // Отправка текущего времени клиенту
            string message = DateTime.Now.ToString();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            s.Send(data);

            // Закрываем соединение
            s.Shutdown(SocketShutdown.Both);
            s.Close();

            // Обновляем лог
            logAction?.Invoke($"Отправлено время клиенту: {message}");
        }

        private void Server_Begin(Socket s)
        {
            while (true)
            {
                try
                {
                    Socket ns = s.Accept();
                    string clientEndPoint = ns.RemoteEndPoint.ToString();
                    logAction?.Invoke($"Клиент подключился: {clientEndPoint}");

                    // Обработка соединения в отдельном потоке
                    ConnectDelegate cd = new ConnectDelegate(Server_Connect);
                    cd.BeginInvoke(ns, null, null);
                }
                catch (SocketException ex)
                {
                    logAction?.Invoke($"SocketException: {ex.Message}");
                    break; // Можно завершить цикл при ошибке
                }
                catch (Exception ex)
                {
                    logAction?.Invoke($"Исключение: {ex.Message}");
                    break;
                }
            }
        }

        public void Start()
        {
            if (socket != null)
                return;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endP);
            socket.Listen(10);

            // Запускаем сервер в отдельном потоке
            Task.Run(() =>
            {
                logAction?.Invoke("Сервер запущен");
                var start = new StartNetwork(Server_Begin);
                start.BeginInvoke(socket, null, null);
            });
        }

        public void Stop()
        {
            if (socket != null)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    socket = null;
                    logAction?.Invoke("Сервер остановлен");
                }
                catch (SocketException ex)
                {
                    logAction?.Invoke($"Ошибка при остановке: {ex.Message}");
                }
            }
        }
    }
}
