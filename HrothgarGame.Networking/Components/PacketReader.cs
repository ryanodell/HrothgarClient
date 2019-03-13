using System;
using System.Net.Sockets;
using System.Text;

namespace HrothgarGame.Networking
{
    public class PacketReader
    {
        private TcpClient _tcpClient;
        public event EventHandler<EventArgs> OnReceiveData;

        public PacketReader(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public async void ReadData()
        {
            var buffer = new byte[4096];
            var ns = _tcpClient.GetStream();
            try
            {
                while(true)
                {
                    var bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) return; // Stream was closed
                    var readStr = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    OnReceiveData(this, new DatReceivedEventArgs(readStr));
                    //Console.WriteLine(readStr);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    public class DatReceivedEventArgs : EventArgs
    {
        public string Data;
        public DatReceivedEventArgs(string data)
        {
            Data = data;
        }
    }
}
