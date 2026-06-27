using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LANChatClient;

public class UdpChatService {

    private UdpClient _listener, _sender;

    public UdpChatService(int listeningPort) {
        _listener = new UdpClient(listeningPort);

        _sender = new UdpClient();
    }

    public async Task StartListeningAsync(Action<string> onMessageReceived) {
        while(true) {
            UdpReceiveResult result = await _listener.ReceiveAsync();
            onMessageReceived(Encoding.UTF8.GetString(result.Buffer));
        }
    }

    public void SendMessageToAll(string givenMsg) {
        byte[] data = Encoding.UTF8.GetBytes(givenMsg);

        _sender.Send(data, data.Length, "255.255.255.255", 9000);
    }

    public string getLocalIPAddress() {
        using(Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)) {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            return endPoint?.Address.ToString() ?? "Unknown";
        }
    }
}
