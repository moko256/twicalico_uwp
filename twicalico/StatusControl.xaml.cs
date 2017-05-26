using CoreTweet;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace twicalico
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class StatusControl : UserControl
    {
        //public static readonly DependencyProperty PropertyStatus = DependencyProperty.Register("Status", typeof(Status), typeof(StatusControl), new PropertyMetadata(null));

        public Status Status => this.DataContext as Status;

        Status display { get; set; }

        private string retweetedTextMessage { get; set; }
        private Visibility retweetTextMessageVisibility
        {
            get=>(retweetedTextMessage != null) ? Visibility.Visible : Visibility.Collapsed;
        }

        private Visibility imageTableVisibility { get; set; }

        public StatusControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += (fe, e) =>
            {
                var status = fe.DataContext as Status;
                if (status != null)
                {
                    display = (status.RetweetedStatus != null) ? status.RetweetedStatus : status;
                    retweetedTextMessage = (status.RetweetedStatus != null) ? "Retweeted by " + status.User.Name : null;
                    imageTableVisibility = (status.ExtendedEntities != null && status.ExtendedEntities.Media != null) ? Visibility.Visible : Visibility.Collapsed;
                }
                this.Bindings.Update();
            };
        }
    }
}
