using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GameLib;

namespace GameApp.Components
{
    class GameBoard
    {
        private Canvas root, canvas = null;
        private int margin = 10;
        private ushort[,] tiles = null;
        private List<Grid> childrens = new List<Grid>();
        private List<GameTile> ch = new List<GameTile>();
        private List<GameTile> backgroundGrid = new List<GameTile>();
        private double tileSize;
        private int size;
        // public bool AnimationEnabled = true;

        public GameBoard(Canvas canvas, int size)
        {
            this.root = canvas;
            this.size = size;

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

            // create background grid
            CreateBackgroundGrid(item);

            return item;
        }

        public void CreateBackgroundGrid(Canvas canvas)
        {
            if (canvas.Height == 0)
            {
                return;
            }

            // calculate size of each tile
            var tileSize = (canvas.Width - (margin * (size + 1))) / size;

            // render childrens:
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double x = i * tileSize + (i + 1) * margin;
                    double y = j * tileSize + (j + 1) * margin;

                    int value = tiles[j, i];
                    var gameTile = new GameTile(0, x, y, tileSize, j, i);

                    canvas.Children.Add(gameTile.GetElement());
                }
            }
        }

        public void Clear()
        {
            canvas.Children.Clear();
        }

        public void ClearChildrens()
        {
            foreach(var item in ch)
            {
                canvas.Children.Remove(item.GetElement());
            }

            ch.Clear();
        }

        private GameTile GetChildren(int indexX, int indexY)
        {
            return ch.Find(item => item.Is(indexX, indexY));
        }

        public void Render(Game game = null)
        {
            if (game != null)
            {
                this.tiles = game.GameBoard;
            }

            // if tiles is empty or canvas is invisible, do not rerender
            if (tiles == null || canvas.Width == 0)
            {
                return;
            }
            
            // get number of tiles in row/col
            int size = tiles.GetLength(0);

            // calculate size of each tile
            tileSize = (canvas.Width - (margin * (size + 1))) / size;

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
                    var gameTile = new GameTile(value, x, y, tileSize, j, i);

                    if (value != 0)
                    {
                        ch.Add(gameTile);
                    }

                    canvas.Children.Add(gameTile.GetElement());
                }
            }
        }

        public void RenderWithAnimation(Game game)
        {
            if (game != null)
            {
                this.tiles = game.GameBoard;
            }

            foreach (var transform in game.LastTransforms)
            {
                if (transform.Type == GameLib.GameBoard.TransformType.Tranlate)
                {
                    var Tile = GetChildren(transform.LastY, transform.LastX);
                    Canvas.SetZIndex(Tile.GetElement(), 1);

                    int offsetX = transform.X - transform.LastX;
                    int offsetY = transform.Y - transform.LastY;

                    Tile.ApplyTransform(offsetX, offsetY);
                }

                if (transform.Type == GameLib.GameBoard.TransformType.Merge)
                {
                    var Tile = GetChildren(transform.LastY, transform.LastX);
                    Canvas.SetZIndex(Tile.GetElement(), 2);

                    int transformX = (transform.X - transform.LastX);
                    int transformY = (transform.Y - transform.LastY);

                    Tile.ApplyTransform(transformX, transformY);
                    Tile.ApplyMerge(game.GameBoard[transform.Y, transform.X]);
                }

                else if (transform.Type == GameLib.GameBoard.TransformType.New)
                {
                    double x = transform.X * tileSize + (transform.X + 1) * margin;
                    double y = transform.Y * tileSize + (transform.Y + 1) * margin;

                    var Tile = new GameTile(game.GameBoard[transform.Y, transform.X], x, y, tileSize, transform.Y, transform.X);
                    ch.Add(Tile);

                    canvas.Children.Add(Tile.GetElement());

                    Canvas.SetZIndex(Tile.GetElement(), 3);
                    Tile.ApplyScale();
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
            Render();
        }
    }
}
