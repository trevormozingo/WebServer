using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Code {

    public interface DataReceiver
    {
        void dataReceived(Byte[] data);
    }

    public class WebServer {

        private static UdpClient       _udpClient;
        public  static bool            _running;
        private static DataReceiver    _receiver;
        private static Thread          _listenerThread;
        private static string          _remoteIP;
        private static int             _remotePort;

        public static bool start(string remoteIP, int remotePort, 
                    DataReceiver receiver) {

            Console.WriteLine("ERROR - START ");


            _running = false;
            _receiver = receiver;
            _remoteIP = remoteIP;
            _remotePort = remotePort;

            ThreadStart threadStart = delegate {
                listen();
			};

            _udpClient = new UdpClient(_remotePort);

            _listenerThread = new Thread(threadStart);
            _listenerThread.Start();

            return true;
        }

        private static void listen() {

            Console.WriteLine("ERROR - LISTEN ");

            try {
                _udpClient.Connect(_remoteIP, _remotePort);
                _running = true;
            }
            catch (Exception e) {
                System.Console.WriteLine(e.ToString());
            }

            while (_running) {

                try {
                    //IPEndPoint object will allow us to read datagrams sent from any source.
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, _remotePort);
                    // Blocks until a message returns on this socket from a remote host.
                    //Byte[] receiveBytes = _udpClient.Receive(ref RemoteIpEndPoint);
                    //_receiver.dataReceived(receiveBytes);
                }  
                catch (Exception e ) {
                        Console.WriteLine(e.ToString());
                }
            }

            _udpClient.Close();
        }

        public static void send(Byte[] sendBytes) {
            Console.WriteLine("SENT");
            Console.WriteLine(_udpClient == null);
            _udpClient.Send(sendBytes, sendBytes.Length);
        }

        public static void kill() {
        }
    }

    public class Handler : DataReceiver {

        public Handler(string ipv4)
        {
            Console.WriteLine("ERROR - HANDLER ");
            WebServer.start(ipv4, 4220, this);
            Console.WriteLine("Started");
        }

        public void dataReceived(Byte[] data)
        {
            Console.WriteLine("ERROR - DRECV ");
            Console.WriteLine(Encoding.ASCII.GetString(data));
        }

        public void send(string message)
        {
            Console.WriteLine("ERROR - DSEND ");
            WebServer.send(System.Text.Encoding.ASCII.GetBytes(message));
        }
    }
}