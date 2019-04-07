using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PSR
{
 


    class Client
    {
        private static Socket s;
        private static TcpClient tcpClient;
        private static SharedData data;
        private static int numberOfThreads = 2;

        public Client()
        {
            data = new SharedData();
        }
        public void Start(int _numberOfThreads)
        {
            numberOfThreads = _numberOfThreads;

            connect();
            send();

            Thread thread = new Thread(() => { listener(); });

        }
        private static void connect()
        {
            /*
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostadd = IPAddress.Parse("127.0.0.1");
            int port = 2222;

            IPEndPoint EPhost = new IPEndPoint(hostadd, port);
            s.Connect(EPhost);
            */
            tcpClient = new TcpClient("127.0.0.1", 2222);
        }
        private static void send()
        {
            Message m = new Message(MESSAGE_TYPE.JOIN, Environment.UserName);

            NetworkStream stream = tcpClient.GetStream();
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Message));
            
            m = (Message)xmlSerializer.Deserialize(stream);
         

            if (m.type==MESSAGE_TYPE.START)
            {
                Console.WriteLine("odpowiedz serwera: {0}", m.msg);

                Graph graph = new Graph(@"../../macierz.txt");
                ConcurrentProgram concurrent = new ConcurrentProgram(numberOfThreads, graph.matrix);
                concurrent.Start();
                m.type = MESSAGE_TYPE.RESULT;
                m.value = concurrent.BestResult;

                xmlSerializer.Serialize(stream, m);
                
            }
            tcpClient.Client.Shutdown(SocketShutdown.Send);


            //int w = 20;

            //byte[] bytes = Encoding.ASCII.GetBytes(w.ToString());

            //stream.Write(bytes, 0, bytes.Length);

            //Byte[] byteData = Encoding.ASCII.GetBytes(msg.ToCharArray());


            //s.Send(byteData, byteData.Length, 0);
        }
        private static void listener()
        {

        }
    }
}
