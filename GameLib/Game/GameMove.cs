using System;
namespace GameLib
{
    public partial class Game
    {
        private void Move()
        {
            // add points
            foreach (var point in gameBoard.lastMergedValues)
            {
                // user reached success value, so he win!
                if (point == successValue && mode == GameMode.Arcade)
                {
                    IsWin = true;
                    mode = GameMode.Free;
                }

                score.Add(point);
            }

            if (gameBoard.isMoved)
            {
                gameBoard.SpawnTile();
                
                Save();
            }

            if (!gameBoard.CanMove())
            {
                End();
            }
        }

        /// <summary>
        /// Move blocks top
        /// </summary>
        public void MoveTop()
        {
            DoMove(gameBoard.MoveTop);
        }

        /// <summary>
        /// Move blocks right
        /// </summary>
        public void MoveRight()
        {
            DoMove(gameBoard.MoveRight);
        }

        /// <summary>
        /// Move blocks bottom
        /// </summary>
        public void MoveBottom()
        {
            DoMove(gameBoard.MoveBottom);
        }

        /// <summary>
        /// Move blocks left
        /// </summary>
        public void MoveLeft()
        {
            DoMove(gameBoard.MoveLeft);
        }

        private void DoMove(Action action)
        {
            var stateBeforeMove = new GameSaveLoadStruct(gameBoard.Grid, score.Score);

            action();
            Move();

            // add snapshot to undoRedo
            if (gameBoard.isMoved)
            {
                AddStep(stateBeforeMove);
                canUndo = true;
            }
        }
    }
}
