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
        }

        private void bt_StartGame(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)cb_gameSize.SelectedItem;
            ushort size = Convert.ToUInt16(typeItem.Tag.ToString());
            
            Page pageGame = new PageGame(size);
            
            NavigationService.Navigate(pageGame);
        }

        private void Cb_gameSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
