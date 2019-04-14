using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace FlightSimulator.Model
{
    class CommandS
    {
        TcpClient tcpClient;
        private static CommandS m_Instance = null;
        
        public static CommandS Instance
        {
            get
            {
                if(m_Instance == null)
                {
                    m_Instance = new CommandS();
                }
                return m_Instance;
            }
        }
 
        public CommandS()  {
            //connect();
        }

        public void openThread(string input) {

         string[] split = Parse(input);
        Thread thread = new Thread(() => sendMessage(split, tcpClient));
            thread.Start();

        }

        public void sendMessage(string[] split, TcpClient tcpClient) {
            NetworkStream ns = tcpClient.GetStream();
             {

                //string[] buffer =;
                foreach(string s in split) {
                    // Send data to server
                    Console.Write("Please enter a number: ");

                    string NewCommand = s;
                    NewCommand += "\r\n";
                    byte[] buff = Encoding.ASCII.GetBytes(NewCommand);
                    ns.Write(buff, 0, buff.Length);
                  
                    //string result = reader.ReadLine();
                    //Console.WriteLine("Result = {0}", result);
                }
            }
        }

        public void connect() {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
            ApplicationSettingsModel.Instance.FlightCommandPort);
            tcpClient = new TcpClient();
            tcpClient.Connect(ep);
            Console.WriteLine("You are connected");
           // tcpClient.Close();
        }

        private string[] Parse(string line)
        {
            string[] newLine = { "\r\n" };
            string[] input = line.Split(newLine, StringSplitOptions.None);
            return input; 
        }
    }
}

  