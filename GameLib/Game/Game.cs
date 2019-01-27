using System;
using System.Collections.Generic;
using static GameLib.GameBoard;

namespace GameLib
{
    public partial class Game
    {
        enum GameMode { Arcade, Free};

        // value, that decides when user win the game in 'Arcade Mode'
        private readonly ushort successValue;
        private readonly byte baseValue;
        // size of grid 
        private readonly byte size;
        private GameBoard gameBoard;
        private GameMode mode;

        // score
        private GameScore score;
        public int Score => score.Score;
        public int Highscore => score.Highscore;

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

            score = new GameScore();

            if (!Load()) {
                Restart();
            }
        }

        public void Restart()
        {
            score.Clear();
            mode = GameMode.Arcade;
            IsPlaying = true;
            InitGameBoard();

            // remove incomplete game if exists
            SaveRemove();
        }

        public void End()
        {
            IsPlaying = false;
        }

        private void InitGameBoard()
        {
            gameBoard = new GameBoard(size, baseValue);
            gameBoard.SpawnTile();
            gameBoard.SpawnTile();
        }

        public ushort[,] GameBoard => gameBoard.Grid;

        public List<GameTranform> LastTransforms => gameBoard.Transforms();
    }
}
