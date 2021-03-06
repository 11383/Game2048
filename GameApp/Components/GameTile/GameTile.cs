﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameApp
{
    partial class GameTile
    {
        private int value;
        private double x, y;
        private int indexX, indexY;
        private int fontSize = 30;
        private double size;
        private Grid element;

        public GameTile(int value, double x, double y, double size, int indexX, int indexY)
        {
            this.value = value;
            this.x = x;
            this.y = y;
            this.indexX = indexX;
            this.indexY = indexY;
            this.size = size;

            this.element = Render();
        }

        public Grid GetElement()
        {
            return element;
        }

        private Grid Render()
        {
            // colors
            var colors = GetColors();
            var bgColor = (SolidColorBrush)new BrushConverter().ConvertFrom(colors.Item1.ToString());
            var textColor = (SolidColorBrush)new BrushConverter().ConvertFrom(colors.Item2.ToString());

            // container
            var item = new Grid {
                Width = size,
                Height = size,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            item.SetValue(Canvas.LeftProperty, x);
            item.SetValue(Canvas.TopProperty, y);

            // shape
            var rectangle = new Rectangle {
                RadiusX = 5,
                RadiusY = 5,
                Width = size,
                Height = size,
                Fill = bgColor
            };

            // text
            var myTextBlock = new TextBlock {
                Text = value == 0 ? "" : value.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = textColor,
                FontSize = fontSize,
                FontWeight = FontWeights.Bold
            };

            // put together
            item.Children.Add(rectangle);
            item.Children.Add(myTextBlock);

            return item;
        }

        private Tuple<Object, Object> GetColors()
        {
            Object bg, text;

            switch (value)
            {
                case 2:
                case 4: // dark tile foreground + specific bachground
                    bg = GetApplicationResourceDictionary()["N" + value + "BackgroundTileColorBrush"];
                    text = GetApplicationResourceDictionary()["DarkTileForegroundColorBrush"];
                    break;
                case 8:
                case 16:
                case 32:
                case 64:
                case 128:
                case 256:
                case 512:
                case 1024:
                case 2048:
                    bg = GetApplicationResourceDictionary()["N" + value + "BackgroundTileColorBrush"];
                    text = GetApplicationResourceDictionary()["LightTileForegroundColorBrush"];
                    break;
                default: // white foreground + black background
                    bg = new BrushConverter().ConvertFrom("#CDC1B4");
                    text = new SolidColorBrush(Colors.White);
                    break;
            }

            return new Tuple<Object, Object>(bg, text);
        }

        public bool Is(int indexX, int indexY)
        {
            return this.indexX == indexX && this.indexY == indexY;
        }

        private static ResourceDictionary GetApplicationResourceDictionary() // returns instance of application's resource dictionary
        {
            return Application.Current.Resources;
        }
    }
}
