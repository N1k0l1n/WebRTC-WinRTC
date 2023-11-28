using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebRTC_WinRTC
{

    public sealed partial class MainPage : Page
    {
        private Microsoft.MixedReality.WebRTC.PeerConnection peerConnection;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await InitializePeerConnection();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred: {ex.Message}");
            }
        }

        private async Task InitializePeerConnection()
        {
            var config = new Microsoft.MixedReality.WebRTC.PeerConnectionConfiguration();
            peerConnection = new Microsoft.MixedReality.WebRTC.PeerConnection();
            peerConnection.Connected += PeerConnection_Connected;
            peerConnection.IceStateChanged += PeerConnection_IceStateChanged;
            await peerConnection.InitializeAsync(config);
        }


        private async void PeerConnection_Connected()
        {
            try
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    UpdateStatus("WebRTC Status: Connected");
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in PeerConnection_Connected: {ex.Message}");
            }
        }



        private async void PeerConnection_IceStateChanged(Microsoft.MixedReality.WebRTC.IceConnectionState newState)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                UpdateStatus($"WebRTC Status: {newState}");
            });
        }


        private void UpdateStatus(string status)
        {
            StatusText.Text = status;
        }
    }
}