using CoreTweet;
using CoreTweet.Rest;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace twicalico
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class TweetImageTableControl : UserControl
    {
        public MediaEntity[] Medias => this.DataContext as MediaEntity[];
        
        private int[,,] Params = new int[,,]
        {
            {{0,0,2,2},{0,0,0,0},{0,0,0,0},{0,0,0,0}},
            {{0,0,2,1},{0,1,2,1},{0,0,0,0},{0,0,0,0}},
            {{0,0,2,1},{0,1,1,1},{1,1,1,1},{0,0,0,0}},
            {{0,0,1,1},{0,1,1,1},{1,0,1,1},{1,1,1,1}}
        };

        public TweetImageTableControl()
        {
            this.InitializeComponent();
            
            this.DataContextChanged += (fe, e) =>
            {
                var medias = fe.DataContext as MediaEntity[];
                if (medias != null && medias.Length > 0)
                {
                    var images = new Image[] { image0, image1, image2, image3 };
                    for (var i = 0; i < 4; i++)
                    {
                        if (i < medias.Length)
                        {
                            images[i].Visibility = Visibility.Visible;
                            images[i].SetValue(Grid.RowProperty, Params[medias.Length - 1, i, 0]);
                            images[i].SetValue(Grid.ColumnProperty, Params[medias.Length - 1, i, 1]);
                            images[i].SetValue(Grid.RowSpanProperty, Params[medias.Length - 1, i, 2]);
                            images[i].SetValue(Grid.ColumnSpanProperty, Params[medias.Length - 1, i, 3]);
                        }
                        else
                        {
                            images[i].SetValue(Grid.RowProperty, Params[medias.Length - 1, i, 0]);
                            images[i].SetValue(Grid.ColumnProperty, Params[medias.Length - 1, i, 1]);
                            images[i].SetValue(Grid.RowSpanProperty, 1);
                            images[i].SetValue(Grid.ColumnSpanProperty, 1);
                            images[i].Visibility = Visibility.Collapsed;
                        }
                    }
                    for (var f = 0; f < medias.Length; f++)
                    {
                        BitmapImage picture = new BitmapImage();
                        picture.DecodePixelType = DecodePixelType.Logical;
                        images[f].Source = picture;
                        picture.UriSource = new Uri(medias[f].MediaUrl + ":medium");
                    }
                }
            };
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (availableSize.Width != 0)
            {
                availableSize.Height = availableSize.Width * 9 / 16;
            } else if (availableSize.Height != 0)
            {
                availableSize.Width = availableSize.Height * 16 / 9;
            }
            return availableSize;
        }
    }
}
