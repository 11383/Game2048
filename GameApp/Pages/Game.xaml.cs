using GameLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using GameBoard = GameApp.Components.GameBoard;

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
        private GameBoard gameBoard;

        public PageGame(ushort size)
        {
            InitializeComponent();
            Focus();
            InitGame(size);

            canvas = cv_GameBoard;

            gameBoard = new Components.GameBoard(canvas);
            Update();
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
            Restart();
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

            Update();
        }

        private void Update()
        {
            gameBoard.Render(game.GameBoard);

            UpdateScoreBoards();

            if (!game.IsPlaying)
            {
                ShowEndMessage();
            }
        }

        private void Restart()
        {
            game.Restart();
            Update();
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
                Restart();
            } else
            {
                Application.Current.Shutdown();
            }
        }
     
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gameBoard.Resize();
        }

        private void UpdateScoreBoards()
        {
            sb_score.SetScore(game.Score);
            sb_highScore.SetScore(game.Highscore);
        }
    }
}
