using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;

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
