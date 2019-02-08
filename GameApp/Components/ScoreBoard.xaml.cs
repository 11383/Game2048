using System.Windows.Controls;

namespace GameApp
{
    /// <summary>
    /// Logika interakcji dla klasy ScoreBoard.xaml
    /// </summary>
    public partial class ScoreBoard : UserControl
    {
        public ScoreBoard()
        {
            InitializeComponent();
        }

        public void SetLabel(string label)
        {
            lb_title.Content = label;
        }

        public void SetScore(int score)
        {
            lb_score.Content = score.ToString();
        }
    }
}
