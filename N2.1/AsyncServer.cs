using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace N2._1
{
    internal class AsyncServer
    {
        public event Action<string> OnLog; // событие для логов

        private IPEndPoint endP;
        private Socket socket;

        public AsyncServer(string strAddr, int port)
        {
            endP = new IPEndPoint(IPAddress.Parse(strAddr), port);
        }

        private void Log(string message)
        {
            OnLog?.Invoke(message);
        }

        void MyAcceptCallbackFunction(IAsyncResult ia)
        {
            Socket listenSocket = (Socket)ia.AsyncState;
            Socket ns = listenSocket.EndAccept(ia);

            // Логируем подключение
            Log($"Клиент подключился: {ns.RemoteEndPoint}");

            // Отправляем текущие время клиенту
            byte[] sendBuffer = System.Text.Encoding.ASCII.GetBytes(DateTime.Now.ToString());
            ns.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None,
                new AsyncCallback(MySendCallbackFunction), ns);

            // Продолжаем принимать клиентов
            listenSocket.BeginAccept(new AsyncCallback(MyAcceptCallbackFunction), listenSocket);
        }

        void MySendCallbackFunction(IAsyncResult ia)
        {
            Socket ns = (Socket)ia.AsyncState;
            try
            {
                int n = ns.EndSend(ia);
                // Можно закрывать соединение после отправки
                ns.Shutdown(SocketShutdown.Send);
                ns.Close();

                Log($"Данные отправлены клиенту: {n} байт");
            }
            catch (Exception ex)
            {
                Log($"Ошибка при отправке: {ex.Message}");
            }
        }

        public void StartServer()
        {
            if (socket != null)
                return;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endP);
            socket.Listen(10);
            Log("Сервер запущен");
            socket.BeginAccept(new AsyncCallback(MyAcceptCallbackFunction), socket);
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
                    Log("Сервер остановлен");
                }
                catch (Exception ex)
                {
                    Log($"Ошибка при остановке: {ex.Message}");
                }
            }
        }
    }
}
