using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;



namespace FlightSimulator.Model
{
    class Info
    {
        TcpClient _client;

        private static Info m_Instance = null;
        public static Info Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Info();
                }
                return m_Instance;
            }
        }

        public Info()
        {
        }
        public void connect()
        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightInfoPort);
            TcpListener listener = new TcpListener(ep);
            listener.Start();
            _client = listener.AcceptTcpClient();
            Console.WriteLine("Info channel: Client connected");
            Thread thread = new Thread(() => listen(_client));
            thread.Start();
        }

        public static void listen(TcpClient _client)
        {
            Byte[] bytes;
            string[] splitMsg = new string[23];
            string lon, lat;
            NetworkStream ns = _client.GetStream();

            while (true)
            {
                if (_client.ReceiveBufferSize > 0)
                {
                    bytes = new byte[_client.ReceiveBufferSize];
                    ns.Read(bytes, 0, _client.ReceiveBufferSize);
                    string msg = Encoding.ASCII.GetString(bytes); //the message incoming
                    splitMsg = msg.Split(',');
                    lon = splitMsg[0];
                    lat = splitMsg[1];
                    Console.WriteLine(msg);

                }
            }
        }
    }
}
