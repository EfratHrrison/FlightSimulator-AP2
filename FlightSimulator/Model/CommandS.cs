using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

/********************
 * class CommandS.
 * this class is responsible for the server side.
 * he will send command to the simulator and will make him fly in the sky. 
 *******************/
namespace FlightSimulator.Model
{
    class CommandS
    {
        bool isConnnect = false;
        TcpClient tcpClient;
        Thread threadCommand;
        private static CommandS m_Instance = null;

        public static CommandS Instance
        {
            get
            {
                if(null == m_Instance)
                {
                    m_Instance = new CommandS();
                }
                return m_Instance;
            }
        }
 
        public CommandS()  {}

        //this function opens the thread
        public void openThread(string input) {

         string[] split = Parse(input);
        threadCommand = new Thread(() => sendMessage(split, tcpClient));
            threadCommand.Start();

        }

        public void disconnect()
        {
            isConnnect = false;
            tcpClient.Close();
        }

        //parser the string and send the command to the simulator 
        public void sendMessage(string[] split, TcpClient tcpClient) {
            if (!isConnnect){
                return;
            }
            NetworkStream ns = tcpClient.GetStream();
             {

               
                foreach(string s in split) {
                    string NewCommand = s;
                    NewCommand = NewCommand + "\r\n";
                    byte[] buff = Encoding.ASCII.GetBytes(NewCommand);
                    ns.Write(buff, 0, buff.Length);
                    Thread.Sleep(2000);
                }
                
            }
        }

        //connect to the simulator as server side 
        public void connect() {
         
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
            ApplicationSettingsModel.Instance.FlightCommandPort);
            tcpClient = new TcpClient();
            tcpClient.Connect(ep);
            isConnnect = true;
            Console.WriteLine("You are connected");
        }

        private string[] Parse(string line)
        {
            string[] newLine = { "\r\n" };
            string[] input = line.Split(newLine, StringSplitOptions.None);
            return input; 
        }

        public bool getIsConnected()
        {
            return isConnnect;
        }
    }
}

  