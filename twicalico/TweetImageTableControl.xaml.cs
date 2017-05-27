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
            {{0,0,2,2},{0,0,1,1},{0,0,1,1},{0,0,1,1}},
            {{0,0,2,1},{0,1,2,1},{0,0,1,1},{0,0,1,1}},
            {{0,0,2,1},{0,1,1,1},{1,1,1,1},{0,0,1,1}},
            {{0,0,1,1},{0,1,1,1},{1,0,1,1},{1,1,1,1}}
        };

        private int count()
        {
            if (Medias != null)
            {
                return Medias.Length;
            }
            else
            {
                return 1;
            }
        }

        private int GridRow0 { get => Params[count() - 1, 0, 0]; }
        private int GridColumn0 { get => Params[count() - 1, 0, 1]; }
        private int GridRowSpan0 { get => Params[count() - 1, 0, 2]; }
        private int GridColumnSpan0 { get => Params[count() - 1, 0, 3]; }
        private Visibility GridVisibility0 { get => count() > 0 ? Visibility.Visible : Visibility.Collapsed; }

        private int GridRow1 { get => Params[count() - 1, 1, 0]; }
        private int GridColumn1 { get => Params[count() - 1, 1, 1]; }
        private int GridRowSpan1 { get => Params[count() - 1, 1, 2]; }
        private int GridColumnSpan1 { get => Params[count() - 1, 1, 3]; }
        private Visibility GridVisibility1 { get => count() > 1 ? Visibility.Visible : Visibility.Collapsed; }

        private int GridRow2 { get => Params[count() - 1, 2, 0]; }
        private int GridColumn2 { get => Params[count() - 1, 2, 1]; }
        private int GridRowSpan2 { get => Params[count() - 1, 2, 2]; }
        private int GridColumnSpan2 { get => Params[count() - 1, 2, 3]; }
        private Visibility GridVisibility2 { get => count() > 2 ? Visibility.Visible : Visibility.Collapsed; }

        private int GridRow3 { get => Params[count() - 1, 3, 0]; }
        private int GridColumn3 { get => Params[count() - 1, 3, 1]; }
        private int GridRowSpan3 { get => Params[count() - 1, 3, 2]; }
        private int GridColumnSpan3 { get => Params[count() - 1, 3, 3]; }
        private Visibility GridVisibility3 { get => count() > 3?Visibility.Visible:Visibility.Collapsed; }

        public TweetImageTableControl()
        {
            this.InitializeComponent();
            
            this.DataContextChanged += (fe, e) =>
            {
                var medias = fe.DataContext as MediaEntity[];
                if (medias != null && medias.Length > 0)
                {
                    this.Visibility = Visibility.Visible;

                    this.Bindings.Update();

                    var images = new Image[] { image0, image1, image2, image3 };

                    for (var i = 0; i < medias.Length; i++)
                    {
                        BitmapImage picture = new BitmapImage();
                        picture.UriSource = new Uri(medias[i].MediaUrl + ":small");
                        picture.AutoPlay = true;
                        images[i].Source = picture;
                    }
                }
                else
                {
                    this.Visibility = Visibility.Collapsed;
                }
            };
        }
    }
}
