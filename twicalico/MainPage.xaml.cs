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

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace twicalico
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ContentRootFrame.Navigate(typeof(TimelinePage));
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            ExploreMenuSpliteView.IsPaneOpen = !ExploreMenuSpliteView.IsPaneOpen;
        }

        private void TweetButton_Click(object sender, RoutedEventArgs e)
        {
            postTweet(TweetBox.Text);
            TweetBox.Text = "";
        }

        private async void postTweet(string text)
        {
            var twitter = (Application.Current as App).twitter;
            await twitter.Statuses.UpdateAsync(status => text);
        }
    }
}
