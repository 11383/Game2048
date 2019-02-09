using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameApp.Components
{
    class GameBoard
    {
        private Canvas root, canvas = null;
        private int margin = 10;
        private ushort[,] tiles = null;
        private List<Grid> childrens = new List<Grid>();

        public GameBoard(Canvas canvas)
        {
            this.root = canvas;

            Resize();
        }

        private Canvas Create()
        {
            // calculate size for ratio 1:1
            var size = root.ActualHeight < root.ActualWidth ? root.ActualHeight : root.ActualWidth;
            var offsetX = (root.ActualWidth - size) / 2;
            var offsetY = (root.ActualHeight - size) / 2;

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

        public void Clear()
        {
            canvas.Children.Clear();
        }

        public void ClearChildrens()
        {
            foreach(var item in childrens)
            {
                canvas.Children.Remove(item);
            }
        }

        public void Render(ushort[,] tiles)
        {
            this.tiles = tiles;

            // if tiles is empty or canvas is invisible, do not rerender
            if (tiles == null || canvas.Width == 0)
            {
                return;
            }
            
            // get number of tiles in row/col
            int size = tiles.GetLength(0);

            // calculate size of each tile
            double tileSize = (canvas.Width - (margin * (size + 1))) / size;

            // clear canvas
            ClearChildrens();
            
            // render childrens:
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double x = i * tileSize + (i + 1) * margin;
                    double y = j * tileSize + (j + 1) * margin;

                    int value = tiles[j, i];
                    var gameTile = new GameTile(value, x, y, tileSize);

                    var tile = gameTile.Render();
                    childrens.Add(tile);

                    canvas.Children.Add(tile);
                }
            }
        }

        public void Resize()
        {
            if (canvas != null)
            {
                root.Children.Remove(canvas);
            }
            
            canvas = Create();
            root.Children.Add(canvas);

            // ReRender childrens if exists
            Render(tiles);
        }
    }
}
