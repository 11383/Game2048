using System;

namespace GameLib
{
    public class Game
    {
        enum GameMode { Arcade, Free};

        //private readonly int size;
        private bool win = false;
        private GameMode mode;

        public Game(byte size = 4)
        {
            if (size < 3)
            {
                throw new ArgumentException("Size can't be smaller than 3");
            }

            //this.size = size;
            this.restart();
        }

        public bool isPlaying()
        {
            //todo
            return true;
        }

        public bool isWin()
        {
            return win;
        }

        public void restart()
        {
            this.mode = GameMode.Arcade;
        }

    }
}
