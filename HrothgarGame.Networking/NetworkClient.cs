using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HrothgarGame.Networking
{
    public class NetworkClient
    {
        private TcpClient _tcpClient;
        public PacketReader Packet_Reader;
        private PacketWriter _packetWriter;
                

        public NetworkClient(string ip, int port)
        {
            _connect(ip, port);
            Packet_Reader = new PacketReader(_tcpClient);
            _packetWriter = new PacketWriter(_tcpClient);
        }

        public async Task Read()
        {
            await Task.Run(() => Packet_Reader.ReadData());
        }

        public void Write(string data)
        {
            _packetWriter.Write(data);
        }

        private async void _connect(string ip, int port)
        {
            _tcpClient = new TcpClient();
            await _tcpClient.ConnectAsync(ip, port);
        }
    }
}
