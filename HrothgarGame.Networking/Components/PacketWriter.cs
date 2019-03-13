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
            ns.WriteAsync(buff, 0, buff.Length);
        }

        public void SendTcp()
        {

        }
    }
}
