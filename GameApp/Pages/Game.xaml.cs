using GameLib;
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
    /// Logic for Game page
    /// </summary>
    public partial class PageGame : Page
    {
        private ushort size;
        private Canvas canvas;

        private Game game;
        private Game score;
        private Game highscore;

        private int margin = 10;
        private int tileSize = 100;

        public PageGame(ushort size)
        {
            InitializeComponent();
            Focus();
            InitGame(size);

            canvas = cv_GameBoard;
            Render();
        }

        private void InitGame(ushort size)
        {
            this.size = size;
            this.game = new Game((byte)size, 2, 2048);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            switch (e.Key)
            {
                case Key.A:
                    game.MoveLeft();
                    break;

                case Key.D:
                    game.MoveRight();
                    break;

                case Key.W:
                    game.MoveTop();
                    break;

                case Key.S:
                    game.MoveBottom();
                    break;
            }

            Render();
        }

        private void Render()
        {
            canvas.Children.Clear();

            var gameBoard = GetBoard();

            var textColor = (SolidColorBrush)GetApplicationResourceDictionary()["TextColorBrush"];
            var bgColor = (SolidColorBrush)GetApplicationResourceDictionary()["BackgroundColorBrush"];
            
            for (int i=0; i < size; i++)
            {
                for(int j=0; j < size; j++)
                {
                    /**
                     * todo: on constructor and pageChange calculate tileSize //Szerokość(canvas - marginy kafelków)/ ilość kafelków
                     */

                    int number = game.GameBoard[j, i];

                    switch(number)
                    {
                        case 2: case 4: // dark tile foreground + specific bachground
                            bgColor = (SolidColorBrush)GetApplicationResourceDictionary()["N" + number + "BackgroundTileColorBrush"];
                            textColor = (SolidColorBrush)GetApplicationResourceDictionary()["DarkTileForegroundColorBrush"];
                            gameBoard.Children.Add(
                                GetTiles(i * tileSize + (i + 1) * margin, j * tileSize + (j + 1) * margin, game.GameBoard[j, i].ToString(), bgColor.ToString(), textColor.ToString())
                            );
                            break;
                        case 8: case 16: case 32: case 64: case 128: case 256: case 512: case 1024: case 2048: 
                            bgColor = (SolidColorBrush)GetApplicationResourceDictionary()["N" + number + "BackgroundTileColorBrush"];
                            textColor = (SolidColorBrush)GetApplicationResourceDictionary()["LightTileForegroundColorBrush"];
                            gameBoard.Children.Add(
                                GetTiles(i * tileSize + (i + 1) * margin, j * tileSize + (j + 1) * margin, game.GameBoard[j, i].ToString(), bgColor.ToString(), textColor.ToString())
                            );
                            break;
                        default: // white foreground + black background
                            bgColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#CDC1B4");
                            textColor = new SolidColorBrush(Colors.White);
                            gameBoard.Children.Add(
                                GetTiles(i * tileSize + (i + 1) * margin, j * tileSize + (j + 1) * margin, game.GameBoard[j, i].ToString(), bgColor.ToString(), textColor.ToString())
                            );
                            break;

                    }
                }
            }

            canvas.Children.Add(gameBoard);
        }

        private Canvas GetBoard()
        {
            // calculate size for ratio 1:1
            var size = canvas.ActualHeight < canvas.ActualWidth ? canvas.ActualHeight : canvas.ActualWidth;
            var offsetX = (canvas.ActualWidth - size) / 2;
            var offsetY = (canvas.ActualHeight - size) / 2;

            Console.WriteLine(canvas.ActualHeight);

            // colors
            var bgColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#bbada0");

            var item = new Canvas { Width = size, Height = size };

            var itemFill = new Rectangle { Width = size, Height = size, RadiusX = 10, RadiusY = 10, Fill = bgColor };
            item.Children.Add(itemFill);

            // position
            item.SetValue(Canvas.LeftProperty, offsetX);
            item.SetValue(Canvas.TopProperty, offsetY);

            Console.WriteLine(item);

            return item;
        }

        private Grid GetTiles(Double x, Double y, string text, String bgC, String tC)
        {
            int width = 100, height = 100, fontSize = 30;

            // colors
            var textColor = (SolidColorBrush)new BrushConverter().ConvertFrom(tC);
            var bgColor = (SolidColorBrush)new BrushConverter().ConvertFrom(bgC);

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

        private void page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Render();
        }

        /*private Grid GetScore(byte score)
        {
            this.score = new Game(score);

            var ScoreTextBlock = new TextBlock { };
        }
        */
        public static ResourceDictionary GetApplicationResourceDictionary() // returns instance of application's resource dictionary
        {
            return Application.Current.Resources;
        }
    }
}
