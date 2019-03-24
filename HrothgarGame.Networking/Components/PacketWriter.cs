using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace HrothgarGame.Networking
{
    public class PacketWriter
    {
        private TcpClient _tcpClient;
        public PacketWriter(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public void Write(string data)
        {
            var buff = Encoding.ASCII.GetBytes(data);
            var ns = _tcpClient.GetStream();
            //this for sending stuff
            //Array.Resize(ref buff, buff.Length + 2);
            //var destinationArray = new byte[buff.Length];
            //Array.Copy(buff, 0, destinationArray, 2, buff.Length -2);
            //var len = (short)destinationArray.Length;
            //byte[] size = BitConverter.GetBytes(len);
            //destinationArray[0] = size[0];
            //destinationArray[1] = size[1];            
            //ns.WriteAsync(destinationArray, 0, destinationArray.Length);

            //Drag to here to bypass auth
            ns.WriteAsync(buff, 0, buff.Length);
        }

        public void SendTcp()
        {
            var arr = new byte[64];
        }
    }
}
