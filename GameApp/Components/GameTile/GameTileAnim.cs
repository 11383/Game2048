using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GameApp
{
    partial class GameTile
    {
        private DoubleAnimation genAnimation(double from, double to)
        {
            var anim = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(new TimeSpan(0, 0, 0, 0, 400)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                // EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
                // new CubicEase { EasingMode = EasingMode.EaseInOut } 
            };

            //anim.Completed += myanim_Completed;

            return anim;
        }

        public void ApplyTransform(int x = 0, int y = 0)
        {
            var animX = genAnimation(0, x * (size + 10));
            var animY = genAnimation(0, y * (size + 10));

            indexX += y;
            indexY += x;

            var a = new TranslateTransform();
            element.RenderTransform = a;

            a.BeginAnimation(TranslateTransform.XProperty, animX, HandoffBehavior.SnapshotAndReplace);
            a.BeginAnimation(TranslateTransform.YProperty, animY, HandoffBehavior.SnapshotAndReplace);
        }

        public async void ApplyScale()
        {
            element.Visibility = Visibility.Collapsed;

            await Task.Delay(500);

            var animX = genAnimation(0, 1.0);
            var animY = genAnimation(0, 1.0);

            element.Visibility = Visibility.Visible;

            var a = new ScaleTransform();
            element.RenderTransform = a;

            a.BeginAnimation(ScaleTransform.ScaleXProperty, animX, HandoffBehavior.SnapshotAndReplace);
            a.BeginAnimation(ScaleTransform.ScaleYProperty, animY, HandoffBehavior.SnapshotAndReplace);
        }

        public async void ApplyMerge(ushort value)
        {
            await Task.Delay(300);

            this.value = value;
            var colors = GetColors();

            var bgColor = (SolidColorBrush)new BrushConverter().ConvertFrom(colors.Item1.ToString());
            var textColor = (SolidColorBrush)new BrushConverter().ConvertFrom(colors.Item2.ToString());

            var block = (Rectangle)element.Children[0];
            block.Fill = bgColor;

            var textBox = (TextBlock)element.Children[1];
            textBox.Text = value.ToString();
            textBox.Foreground = textColor;
        }
    }
}
