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

    public void SendMessage(string given_msg) {
    }
}
