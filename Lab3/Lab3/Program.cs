using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab3
{
    class Program
    {
        static void SendRequest(IPEndPoint ipe, string request)
        {
            Byte[] bytesReceived = new Byte[6000];
            Byte[] bytesSend = Encoding.ASCII.GetBytes(request);
            string page = "";
            Socket s = 
                new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            s.Connect(ipe);
            Console.WriteLine("Request: \n"+request+"\n");
            Console.WriteLine("Send bytes: "+s.Send(bytesSend));
                    
            int bytes = 0;

            do {
                bytes = s.Receive(bytesReceived);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                        
            }
            while (bytes > 0);
            Console.WriteLine(page);
            s.Close();
        }
        static void Main(string[] args)
        {
            String host = "www.httpbin.org";
            int port = 80;
            IPHostEntry hostEntry = null;
            IPEndPoint ipe = null;
            string getRequest = "GET /get HTTP/1.1\r\n"+
            "Host: "+host+"\r\n"+
            "Connection: close\r\n"+
            "\r\n";
            
            string postRequest = "POST /post HTTP/1.1\r\n"+
                                "Host: "+host+"\r\n"+
                                "Connection: close\r\n"+
                                "\r\n";
            
            string putRequest = "PUT /put HTTP/1.1\r\n"+
                                 "Host: "+host+"\r\n"+
                                 "Connection: close\r\n"+
                                 "\r\n";
            
            string deleteRequest = "DELETE /delete HTTP/1.1\r\n"+
                                 "Host: "+host+"\r\n"+
                                 "Connection: close\r\n"+
                                 "\r\n";
            
            string patchRequest = "PATCH /patch HTTP/1.1\r\n"+
                                 "Host: "+host+"\r\n"+
                                 "Connection: close\r\n"+
                                 "\r\n";
                        
            hostEntry = Dns.GetHostEntry(host);
            foreach(IPAddress address in hostEntry.AddressList)
            {
                ipe = new IPEndPoint(address, port);
                Socket s = 
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                s.Connect(ipe);

                if(s.Connected)
                {
                    s.Close();
                    break;
                }

                ipe = null;
            }

            if (ipe != null)
            {
                SendRequest(ipe, getRequest);
                SendRequest(ipe, postRequest);
                SendRequest(ipe, putRequest);
                SendRequest(ipe, deleteRequest);
                SendRequest(ipe, patchRequest);
            }

        }
    }
}