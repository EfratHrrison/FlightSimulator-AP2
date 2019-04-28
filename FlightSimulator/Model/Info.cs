using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using FlightSimulator.ViewModels;


/********************
 * class Info.
 * this class is responsible for the client side.
 * he will get the lon and let from the simulator and drew the map by them. 
 *******************/
namespace FlightSimulator.Model
{
    class Info : BaseNotify
    {
        TcpClient _client;
        double lon;
        double lat;
        Thread threadInfo;

        public double Lon
        {
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
            get { 
            return lon;
            }
        }

        public double Lat
        {
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
            get { 
               return lat; 
            }
        }

        public bool shouldContinue
        {
            set;
            get;
        }

        private static Info m_Instance = null;
        public static Info Instance
        {
            get
            {
                if (null == m_Instance)
                {
                    m_Instance = new Info();
                }
                return m_Instance;
            }
        }

        public Info()
        {
            shouldContinue = true;
        }

        //connect to the simulator as a client 
        public void connect()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightInfoPort);
            TcpListener listener = new TcpListener(ep);
            listener.Start();
            _client = listener.AcceptTcpClient();
            
            threadInfo = new Thread(() => listen(_client));
            threadInfo.Start();
        }

        //disconnect the simulator 
        public void disconnect()
        {
            _client.Close();
            shouldContinue = false;
        }

        public void listen(TcpClient _client)
        {
            Byte[] bytes;
            string[] splitMsg = new string[25];
            
            NetworkStream ns = _client.GetStream();

            //get the values - lon and let from the simulator 
            while (shouldContinue)
            {
                if (_client.ReceiveBufferSize > 0)
                {
                    bytes = new byte[_client.ReceiveBufferSize];
                    ns.Read(bytes, 0, _client.ReceiveBufferSize);
                    string msg = Encoding.ASCII.GetString(bytes); 
                    splitMsg = msg.Split(',');
                    Lon = float.Parse(splitMsg[0]);
                    Lat = float.Parse(splitMsg[1]);
                }
            }
            //close the socket 
            ns.Close();
            _client.Close();
        }


    }
}
