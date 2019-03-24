using HrothgarGame.Networking;
using System;

namespace HrothgarGame.Logic
{
    public class NetworkManager
    {
        public NetworkClient Client;
        public bool Authenticated = false;
        public event EventHandler<EventArgs> DataReceived;
        public event EventHandler<EventArgs> OnAuthenticated;

        public async void ConnectToServer(string username, string password)
        {
            Client = new NetworkClient(Global.IpAddress, Global.Port);
            await Client.Read();
            Client.Packet_Reader.OnReceiveData += OnReceiveData;
            WriteData($"{username}|{password}|");
        }

        public void WriteData(string data)
        {
            Client.Write(data);
        }

        public void OnReceiveData(object sender, DatReceivedEventArgs args)
        {
            Console.WriteLine(args.Data);
            if(!Authenticated)
            {
                if(int.TryParse(args.Data, out var responseCode))
                {
                    switch ((eServerConnectResponse)responseCode)
                    {
                        case eServerConnectResponse.Success:
                            Authenticated = true;
                            OnAuthenticated(this, null);
                            break;
                        case eServerConnectResponse.IncorrectUsernameOrPassword:
                            break;
                        default:
                            break;
                    }
                }
                return;
            }
        }
    }
}
