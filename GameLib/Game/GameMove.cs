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
            gameBoard.MoveTop();
            Move();
        }

        /// <summary>
        /// Move blocks right
        /// </summary>
        public void MoveRight()
        {
            gameBoard.MoveRight();
            Move();
        }

        /// <summary>
        /// Move blocks bottom
        /// </summary>
        public void MoveBottom()
        {
            gameBoard.MoveBottom();
            Move();
        }

        /// <summary>
        /// Move blocks left
        /// </summary>
        public void MoveLeft()
        {
            gameBoard.MoveLeft();
            Move();
        }
    }
}
