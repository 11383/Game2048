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
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Canvas canvas;
        public MainWindow()
        {
            InitializeComponent();

            canvas = cv_GameBoard;

            Render();
        }

        private void Render()
        {
            canvas.Children.Clear();

            var gameBoard = GetBoard();

            //add tiles
            gameBoard.Children.Add(GetTiles(10.0, 10.0, "1024"));
            gameBoard.Children.Add(GetTiles(120.0, 10.0, "256"));

            canvas.Children.Add(gameBoard);
        }

        private Canvas GetBoard()
        {
            // calculate size for ratio 1:1
            var size = canvas.ActualHeight < canvas.ActualWidth ? canvas.ActualHeight : canvas.ActualWidth;
            var offsetX = (canvas.ActualWidth - size) / 2;
            var offsetY = (canvas.ActualHeight - size) / 2;

            // colors
            var bgColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#bbada0");

            var item = new Canvas { Width = size, Height = size };

            var itemFill = new Rectangle { Width = size, Height = size, RadiusX = 10, RadiusY = 10, Fill = bgColor };
            item.Children.Add(itemFill);

            // position
            item.SetValue(Canvas.LeftProperty, offsetX);
            item.SetValue(Canvas.TopProperty, offsetY);

            return item;
        }

        private Grid GetTiles(Double x, Double y, string text)
        {
            int width = 100, height = 100, fontSize = 30;

            // colors
            var textColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#776e65");
            var bgColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#eee4da");

            // container
            var item = new Grid { Width = width, Height = height };
            item.SetValue(Canvas.LeftProperty, x);
            item.SetValue(Canvas.TopProperty, y);

            // shape
            var rectangle = new Rectangle { RadiusX = 5, RadiusY = 5, Width = width, Height = height, Fill = bgColor };

            // text
            var myTextBlock = new TextBlock { Text = text, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Foreground = textColor, FontSize = fontSize, FontWeight = FontWeights.Bold };

            // put together
            item.Children.Add(rectangle);
            item.Children.Add(myTextBlock);

            return item;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Render();
        }
    }
}
