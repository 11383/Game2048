using System;
using System.Collections.Generic;

namespace GameLib
{
    public partial class GameBoard
    {
        // size horiznotall/vertiacal of grid
        private readonly byte size;
        // number used to spawn new Tile
        private readonly byte baseNumber;
        // return actual state of Grid
        public ushort[,] Grid { get; private set; }

        public GameBoard(byte size = 4, byte baseNumber = 2)
        {
            if (size * baseNumber == 0)
            {
                throw new ArgumentException("'size' or 'baseNumber' can't be zero value!");
            }

            this.size = size;
            this.baseNumber = baseNumber;
            this.Grid = new ushort[size, size];
        }

        // create gameboard from given grid
        public GameBoard(ushort[,] grid, byte size = 4, byte baseNumber = 2) : this(size, baseNumber)
        {
            this.Grid = grid;
        }

        // Get list of coordinates of empty cells
        public List<Tuple<byte, byte>> GetEmptyTilesPos()
        {
            var result = new List < Tuple<byte, byte> >();

            for (byte i=0; i<size; i++)
            {
                for (byte j=0; j<size; j++)
                {
                    if (Grid[i,j] == 0)
                    {
                        result.Add(new Tuple<byte, byte>(i, j));
                    }
                }
            }

            return result;
        }

        private void AddTile(byte y, byte x, ushort value)
        {
            // Fill picked tile with value:
            Grid[y, x] = (ushort) value;

            // log action (create new block) to transforms
            AddTransform(x, y, x, y, TransformType.New);
        }

        public void SpawnTile()
        {
            Random rnd = new Random();

            // Generate random value for new tile: 
            // For example, if we hasve baseNumber = 2  we have 80% change to pick value = 2 
            // and 20% change to pick value = 4
            int value = rnd.Next(0, 10) > 8 ? baseNumber * 2 : baseNumber;

            // Get positions list of free slots
            List<Tuple<byte, byte>> emptyTiles = GetEmptyTilesPos();

            // Pick random free tile slot, and set new value:
            int randomEmptyTileIndex = rnd.Next(0, emptyTiles.Count);
            var coordinatesEmptyTile = emptyTiles[randomEmptyTileIndex];

            AddTile(coordinatesEmptyTile.Item1, coordinatesEmptyTile.Item2, (ushort) value);
        }

    }
}
