using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameApp
{
    /// <summary>
    /// Logika interakcji dla klasy ImageSelect.xaml
    /// </summary>
    public partial class ImageSelect : UserControl
    {
        private Tuple<Uri, string>[] data;
        public int Index { private set; get; }

        public ImageSelect()
        {
            InitializeComponent();
        }

        private void UpdateImage()
        {
            img_Preview.Source = new BitmapImage(
                data[Index].Item1
            );

            tb_Name.Text = data[Index].Item2;
        }

        public void SetImages(Tuple<Uri, string>[] images)
        {
            data = images;
            Index = 0;

            UpdateImage();
        }

        public void Next()
        {
            Index = Index < data.Length - 1 ? ++Index : 0;

            UpdateImage();
        }

        public void Previous()
        {
            Index = Index > 0 ? --Index : data.Length - 1;

            UpdateImage();
        }

        public void First()
        {
            Index = 0;

            UpdateImage();
        }

        public void Last()
        {
            Index = data.Length - 1;

            UpdateImage();
        }

        private void bt_Next(object sender, RoutedEventArgs e)
        {
            Next();
        }

        private void bt_Previous(object sender, RoutedEventArgs e)
        {
            Previous();
        }
    }
}
