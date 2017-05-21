﻿using CoreTweet;
using CoreTweet.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class TimelinePage : Page
    {
        private IncrementalLoadingCollection<Status, ListedResponse<Status>> _items;

        public ObservableCollection<Status> Items
        {
            get { return this._items; }
        }

        public TimelinePage()
        {
            _items = new IncrementalLoadingCollection<Status, ListedResponse<Status>>(Dispatcher);

            this.InitializeComponent();
            
            _items.makeId = it => it.Id;
            _items.LoadMoreTask = lastId => RequestStatusesFromAPI(new Dictionary<string, Object>() {
                { "count", 50 },
                { "max_id", lastId - 1L }
            });

            InitTimeline();
        }

        private async void InitTimeline()
        {
            var tl = await RequestStatusesFromAPI(new Dictionary<string, Object>() {
                 {"count", 20 }
             });
            foreach(var status in tl)
            {
                Items.Add(status);
            }
        }

        private Task<ListedResponse<Status>> RequestStatusesFromAPI(IDictionary<string, object> parameters)
        {
            return (Application.Current as App).twitter.Statuses.HomeTimelineAsync(parameters);
        }
    }
}
