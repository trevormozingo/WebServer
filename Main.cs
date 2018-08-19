using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Code;

namespace Code {

    class MAIN {

        public static void Main() {
            
            string ipv4 = "ip goes here";
            Handler handler = new Handler(ipv4);
            handler.send("Hi this is trevor");
        }
    }
}