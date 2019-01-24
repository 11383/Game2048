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
    /// Logic for Menu page
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
            
            img_select.SetImages(new Tuple<Uri, string>[] {
                new Tuple<Uri, string>( new Uri(@"/Resources/grid-3x3.png", UriKind.RelativeOrAbsolute), "3x3"),
                new Tuple<Uri, string>( new Uri(@"/Resources/grid-4x4.png", UriKind.RelativeOrAbsolute), "4x4"),
                new Tuple<Uri, string>( new Uri(@"/Resources/grid-5x5.png", UriKind.RelativeOrAbsolute), "5x5")
            });
        }

        private void bt_StartGame(object sender, RoutedEventArgs e)
        {
            Page pageGame = new PageGame((ushort) (img_select.Index + 3));
            
            NavigationService.Navigate(pageGame);
        }

        private void Cb_gameSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
