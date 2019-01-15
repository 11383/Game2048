using System;
using System.Collections.Generic;

namespace GameLib
{
    public class TilesGrid
    {
        private readonly byte size;
        private readonly byte baseNumber;
        private ushort[,] grid;
        private ushort emptyTiles;

        public TilesGrid(byte size, byte baseNumber = 2)
        {
            if (size * baseNumber == 0)
            {
                throw new ArgumentException("'size' or 'baseNumber' can't be zero value!");
            }

            this.size = size;
            this.emptyTiles = (ushort) (size * size);
            this.baseNumber = baseNumber;
            this.grid = new ushort[size, size];
        }

        // Get list of coordinates of empty cells
        public List<Tuple<byte, byte>> EmptyTiles()
        {
            var result = new List < Tuple<byte, byte> >();

            for (byte i=0; i<size; i++)
            {
                for (byte j=0; j<size; j++)
                {
                    if (grid[i,j] == 0)
                    {
                        result.Add(new Tuple<byte, byte>(i, j));
                    }
                }
            }

            return result;
        }

        public void addTile()
        {
            Random rnd = new Random();

            // Generate random value for new tile: 
            // For example, if we hasve baseNumber = 2  we have 80% change to pick value = 2 
            // and 20% change to pick value = 4
            int value = rnd.Next(0, 10) > 8 ? baseNumber * 2 : baseNumber;

            // Get list of free slots
            List<Tuple<byte, byte>> emptyTiles = this.EmptyTiles();

            Console.WriteLine(emptyTiles.Count);

            // Pick random free tile slot, and set new value:
            int randomEmptyTileIndex = rnd.Next(0, emptyTiles.Count);
            var coordinatesEmptyTile = emptyTiles[randomEmptyTileIndex];

            // Fill picked tile with value:
            grid[
                coordinatesEmptyTile.Item1,
                coordinatesEmptyTile.Item2
            ] = (ushort) value;
        }

        public void move(Directions direction) 
        {
            // todo -> transform array depends of direction
            grid = merge();
        }

        public bool canMove()
        {
            // todo
            return true;
        }

        // merge tiles from right to left
        private ushort[,] merge()
        {
            ushort[,] merged = new ushort[size, size];

            for (int i = 0; i < size; i++)
            {
                int offset = 0;

                for (int j = 0; j < size; j++)
                {
                    // if empty, we don't need to move block
                    if (grid[i, j] == 0)
                    {
                        // we can add value to this block next time, so:
                        offset++;
                    }
                    // we can easy move non empty value, to empty field
                    else if (merged[i, j - offset] == 0 && grid[i, j] != 0)
                    {
                        merged[i, j - offset] = grid[i, j];
                        // we can add value to this block next time, so:
                        offset++;
                    }
                    // we can add block with the same value ex. 2 && 2
                    else if (merged[i, j - offset] == grid[i, j] && grid[i, j] != 0)
                    {
                        merged[i, j - offset] *= 2;
                        // we can't add any value to this block, so we can't increment offset
                    }
                    // we can't add block to block with diffrent value
                    // so add to the next one (offset-1)
                    else
                    {
                        merged[i, j - offset + 1] = grid[i, j];
                    }
                }
            }

            return merged;
        }
    }
}
