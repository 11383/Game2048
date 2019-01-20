using System;
namespace GameLib
{
    [Serializable()]
    public struct GameSaveLoadStruct
    {
        public readonly ushort[,] grid;
        public readonly int score;

        public GameSaveLoadStruct(ushort[,] grid, int score) {
            this.grid = grid;
            this.score = score;
        }
    }
}
