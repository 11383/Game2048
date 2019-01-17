using System;

namespace GameLib
{
    public class Game
    {
        enum GameMode { Arcade, Free};

        private readonly ushort successValue = 2048;
        private readonly byte size;
        public ushort points = 0; //todo private
        private TilesGrid grid;
        private bool playing = false;
        private bool win = false;
        private GameMode mode;

        public Game(byte size = 4)
        {
            if (size < 3)
            {
                throw new ArgumentException("Size can't be smaller than 3");
            }

            this.size = size;
            restart();
        }

        public bool isPlaying()
        {
            return playing;
        }

        public bool isWin()
        {
            return win;
        }

        public void restart()
        {
            mode = GameMode.Arcade;
            playing = true;
            initGameBoard();
        }

        private void initGameBoard()
        {
            grid = new TilesGrid(size, 2);
            grid.addTile();
            grid.addTile();
        }

        private void Move()
        {
            // add points
            foreach (var point in grid.lastMergedValues)
            {
                // user reached success value, so he win!
                if (point == successValue && mode == GameMode.Arcade)
                {
                    win = true;
                    mode = GameMode.Free;
                }

                points += point;
            }

            if (grid.isMoved) {
                Console.WriteLine("Moved");
                grid.addTile();
            }

            if (!grid.canMove())
            {
                playing = false;
                Console.WriteLine("Game Over");
            }
        }

        public void MoveTop()
        {
            grid.MoveTop();
            Move();
        }

        public void MoveRight()
        { 
            grid.MoveRight();
            Move();
        }
        public void MoveBottom()
        {
            grid.MoveBottom();
            Move();
        }
        public void MoveLeft()
        {
            grid.MoveLeft();
            Move();
        }

        public ushort[,] GameBoard => grid.grid;

    }
}
