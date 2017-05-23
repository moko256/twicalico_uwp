using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

        private void TimelineButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(ContentRootFrame.Content is TimelinePage))
            {
                ContentRootFrame.Navigate(typeof(TimelinePage));
            }
        }

        private void MentionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(ContentRootFrame.Content is MentionsPage)){
                ContentRootFrame.Navigate(typeof(MentionsPage));
            }
        }

        private void UserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(ContentRootFrame.Content is MyAccountPage))
            {
                ContentRootFrame.Navigate(typeof(MyAccountPage));
            }
        }

        private void FollowFollowerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(ContentRootFrame.Content is LikeMePage))
            {
                ContentRootFrame.Navigate(typeof(LikeMePage));
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
