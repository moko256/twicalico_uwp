using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace twicalico
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class OAuthPage : Page
    {
        public string PinCode { get; set; }

        CoreTweet.OAuth.OAuthSession sessions;

        public OAuthPage()
        {
            this.InitializeComponent();

            PinCode = "";

            startAuth();
        }

        private void PIN_Auth_Button_Click(object sender, RoutedEventArgs e)
        {
            requestToken();
        }

        private async void startAuth()
        {
            var consumer = new TwitterConsumerImpl() as TwitterConsumer;

            sessions = await CoreTweet.OAuth.AuthorizeAsync(consumer.ConsumerKey(), consumer.ConsumerSecret());

            ContentDialog authTypeDialog = new ContentDialog()
            {
                Title = "Select OAuth type",
                Content = "WebView or Browser",
                PrimaryButtonText = "Browser",
                SecondaryButtonText = "WebView"
            };
            ContentDialogResult result = await authTypeDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var succeed = await Windows.System.Launcher.LaunchUriAsync(sessions.AuthorizeUri);
            }
            else
            {
                AuthWebView.Navigate(sessions.AuthorizeUri);
            }
        }

        private async void requestToken()
        {
            var twitter = await CoreTweet.OAuth.GetTokensAsync(sessions, PinCode);

            new LocalTokenManager().registerToken(twitter);

            (Application.Current as App).twitter = twitter;

            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
