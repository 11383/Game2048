using GameLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private int margin = 10;

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

            sb_score.SetLabel("Score");
            sb_highScore.SetLabel("Highscore");
        }

        private void bt_MainMenu(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("Pages/Menu.xaml", UriKind.RelativeOrAbsolute));
        }

        private void bt_Restart(object sender, RoutedEventArgs e)
        {
            game.Restart();
            Render();
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

            if (gameBoard.Width == 0)
            {
                return;
            }

            double tileSize = (gameBoard.Width - (margin * (size+1))) / size;
            
            for (int i=0; i < size; i++)
            {
                for(int j=0; j < size; j++)
                {
                    int value = game.GameBoard[j, i];
                    object textColor, bgColor;

                    switch(value)
                    {
                        case 2: case 4: // dark tile foreground + specific bachground
                            bgColor = GetApplicationResourceDictionary()["N" + value + "BackgroundTileColorBrush"];
                            textColor = GetApplicationResourceDictionary()["DarkTileForegroundColorBrush"];
                            break;
                        case 8: case 16: case 32: case 64: case 128: case 256: case 512: case 1024: case 2048: 
                            bgColor = GetApplicationResourceDictionary()["N" + value + "BackgroundTileColorBrush"];
                            textColor = GetApplicationResourceDictionary()["LightTileForegroundColorBrush"];
                            break;
                        default: // white foreground + black background
                            bgColor = new BrushConverter().ConvertFrom("#CDC1B4");
                            textColor = new SolidColorBrush(Colors.White);
                            break;
                    }

                    gameBoard.Children.Add(
                        GetTiles(
                            i * tileSize + (i + 1) * margin, 
                            j * tileSize + (j + 1) * margin, 
                            tileSize,
                            value == 0 ? "" : value.ToString(), 
                            bgColor.ToString(), 
                            textColor.ToString())
                    );
                }
            }

            canvas.Children.Add(gameBoard);

            UpdateScoreBoards();

            if (!game.IsPlaying)
            {
                ShowEndMessage();
            }
        }

        public void ShowEndMessage()
        {
            MessageBoxResult result = MessageBox.Show(
                "Game over. \nWanna play again?",
                "Game over",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                game.Restart();
                Render();
            } else
            {
                Application.Current.Shutdown();
            }
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

            return item;
        }

        private Grid GetTiles(Double x, Double y, double size, string text, String bgC, String tC)
        {
            int fontSize = 30;

            // colors
            var textColor = (SolidColorBrush)new BrushConverter().ConvertFrom(tC);
            var bgColor = (SolidColorBrush)new BrushConverter().ConvertFrom(bgC);

            // container
            var item = new Grid { Width = size, Height = size };
            item.SetValue(Canvas.LeftProperty, x);
            item.SetValue(Canvas.TopProperty, y);

            // shape
            var rectangle = new Rectangle { RadiusX = 5, RadiusY = 5, Width = size, Height = size, Fill = bgColor };

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

        private void UpdateScoreBoards()
        {
            sb_score.SetScore(game.Score);
            sb_highScore.SetScore(game.Highscore);
        }
    }
}
