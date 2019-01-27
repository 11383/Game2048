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

        public void MoveTop()
        {
            gameBoard.MoveTop();
            Move();
        }

        public void MoveRight()
        {
            gameBoard.MoveRight();
            Move();
        }

        public void MoveBottom()
        {
            gameBoard.MoveBottom();
            Move();
        }

        public void MoveLeft()
        {
            gameBoard.MoveLeft();
            Move();
        }
    }
}
