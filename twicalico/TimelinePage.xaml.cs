﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace twicalico
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class TimelinePage : Page
    {
        public TimelinePage()
        {
            this.InitializeComponent();
            StatusListView.StatusesSource = param => (Application.Current as App).twitter.Statuses.HomeTimelineAsync(param);
        }
    }
}
