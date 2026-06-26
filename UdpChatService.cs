using System.Net;
using System.Net.Sockets;
using System.Text;

public class UdpChatService {

    private UdpClient _listener;

    public UdpChatService(int listeningPort) {
        _listener = UdpClient(listeningPort);
    }

    public async Task StartListeningAsync(Action<string> onMessageReceived) {
        while(true) {
            UdpReceiveResult result = await udpClient.ReceiveAsync();
            onMessageReceived(Encoding.UTF8.GetString(result.Buffer));
        }
    }
}
