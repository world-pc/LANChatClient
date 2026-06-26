using Avalonia.Controls;

namespace LANChatClient;

public partial class MainWindow : Window {
    
    private UdpChatService _chatService;

    public MainWindow() {
        InitializeComponent();

        _chatService = new UdpChatService(9000);
        _ = _chatService.StartListeningAsync(message => {
            chatlog.Items.Add(message);           
        });
    }
}
