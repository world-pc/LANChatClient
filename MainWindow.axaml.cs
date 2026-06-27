using System;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;

namespace LANChatClient;

public partial class MainWindow : Window {
    
    private UdpChatService _chatService;

    private System.Threading.Timer _timer;

    public MainWindow() {
        InitializeComponent();

        _chatService = new UdpChatService(9000);

        //listen for incoming messages
        _ = _chatService.StartListeningAsync(message => {
            chatlog.Items.Add(message);           
        });

        //broadcast my IP Addr every 2min to update our lobbies.
        _timer = new System.Threading.Timer(_chatService.BroadcastIdent, null, 0, 5000);

        //listen for lobby updates
        _ = _chatService.StartLobbyListen(message => {
            lobby.Items.Add(message);
        });
    }

    private void MainWindow_Opened(object? sender, EventArgs e) {
        usrMsgInput.Focus();
    }

    private void SendButton_Click(object? sender, RoutedEventArgs e) {
        _chatService.SendMessageToAll(usrMsgInput.Text);
        usrMsgInput.Text = "";
    }

    private void UsrMsgInput_KeyDown(object? sender, KeyEventArgs e) {
        if(e.Key == Key.Enter) {
            _chatService.SendMessageToAll(usrMsgInput.Text);
            usrMsgInput.Text = "";
        }
    }
}
