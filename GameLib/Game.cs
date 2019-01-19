using System;

namespace GameLib
{
    public class Game
    {
        enum GameMode { Arcade, Free};

        // value, when user win the game in Arcade Mode
        private readonly ushort successValue;
        private readonly byte baseValue;
        // size of grid 
        private readonly byte size;
        private TilesGrid grid;
        private GameMode mode;

        public int Score { get; private set; } = 0;
        public bool IsPlaying { get; private set; }
        public bool IsWin { get; private set; }

        public Game(byte size = 4, byte baseValue = 2, ushort successValue = 2048)
        {
            if (size < 3)
            {
                throw new ArgumentException("Size can't be smaller than 3");
            }

            this.size = size;
            this.baseValue = baseValue;
            this.successValue = successValue;

            Restart();
        }

        public void Restart()
        {
            mode = GameMode.Arcade;
            IsPlaying = true;
            InitGameBoard();
        }

        public void End()
        {
            IsPlaying = false;
        }

        private void InitGameBoard()
        {
            grid = new TilesGrid(size, baseValue);
            grid.SpawnTile();
            grid.SpawnTile();
        }

        private void Move()
        {
            // add points
            foreach (var point in grid.lastMergedValues)
            {
                // user reached success value, so he win!
                if (point == successValue && mode == GameMode.Arcade)
                {
                    IsWin = true;
                    mode = GameMode.Free;
                }

                Score += point;
            }

            if (grid.isMoved) {
                grid.SpawnTile();
            }

            if (!grid.CanMove())
            {
                IsPlaying = false;
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

        public ushort[,] GameBoard => grid.Grid;

    }
}
