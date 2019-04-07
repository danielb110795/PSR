using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Serialization;
using System.IO;

namespace PSR
{
    class Host
    {

        private static TcpListener tcpLsn;
        private Socket s;
        private Thread threadLsn;
        private Thread threadUsr;
        private Thread threadMsgReader;
        private SharedData sharedData;

        public Host()
        {

        }

        public void Start()
        {
            sharedData = new SharedData();

            threadLsn = new Thread(() => { doListenerWork(); });
            threadLsn.Start();

            threadUsr = new Thread(() => { doListenUser(); });
            threadUsr.Start();

            //threadMsgReader = new Thread(() => { doReaderWork(); });
            //threadMsgReader.Start();


            threadUsr.Join();
            
        }
        private void doReaderWork()
        {
            for(; ; )
            {
                if(!sharedData.IsReaded)
                {
                    Message m = sharedData.Message;
                    Console.WriteLine("Readed msg {0} {1}", m.msg, m.type);
                }
            }
        }
        private void doListenUser()
        {
            string cmd= "";
            do
            {
                cmd = Console.ReadLine();

                if(cmd.CompareTo("cls")==0)
                {
                    Console.Clear();
                }


            } while (cmd.CompareTo("exit") != 0);

            threadLsn.Join();
            //threadMsgReader.Abort();
            //threadMsgReader.Join();

        }

        private void doListenerWork()
        {

            tcpLsn = new TcpListener(IPAddress.Parse("127.0.0.1"), 2222);
            tcpLsn.Start();
            //Socket sckt = tcpLsn.AcceptSocket();
            Console.WriteLine("Oczekiwanie na klienta.");

            TcpClient tcpClient = tcpLsn.AcceptTcpClient();
     
            
            Message m = new Message(MESSAGE_TYPE.START,"");
            NetworkStream stream = tcpClient.GetStream();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Message));
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);


            xmlSerializer.Serialize(stream, m);
            tcpClient.Client.Shutdown(SocketShutdown.Send);



            m = (Message)xmlSerializer.Deserialize(stream);


            if(m.type == MESSAGE_TYPE.RESULT)
            {
                Console.WriteLine("Wynik klienta:{0}", m.value);

            } else if(m.type == MESSAGE_TYPE.FAIL)
            {
                Console.WriteLine("Nie udało się wykonać algorytmu.");
            }
               


                /*
                int ret = sckt.Receive(recivedBytes, recivedBytes.Length, 0);
                string tmp = null;

                tmp = System.Text.Encoding.ASCII.GetString(recivedBytes);

                if (tmp.Length > 0)
                {
                    Console.WriteLine("Odebralem komunikat:");
                    Console.WriteLine(tmp);
                }
                */


            tcpLsn.Stop();
        }

    }
}
