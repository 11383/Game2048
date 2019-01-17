using System;
using System.Collections.Generic;

namespace GameLib
{
    public class TilesGrid
    {
        private readonly byte size;
        private readonly byte baseNumber;
        public ushort[,] grid; //todo private
        public List<ushort> lastMergedValues = new List<ushort>();
        public bool isMoved = false;

        public TilesGrid(byte size, byte baseNumber = 2)
        {
            if (size * baseNumber == 0)
            {
                throw new ArgumentException("'size' or 'baseNumber' can't be zero value!");
            }

            this.size = size;
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
            
            // Pick random free tile slot, and set new value:
            int randomEmptyTileIndex = rnd.Next(0, emptyTiles.Count);
            var coordinatesEmptyTile = emptyTiles[randomEmptyTileIndex];

            // Fill picked tile with value:
            grid[
                coordinatesEmptyTile.Item1,
                coordinatesEmptyTile.Item2
            ] = (ushort) value;
        }

        public void MoveTop()
        {
            var moved = Utils.RotateMatrix(grid, 3, false);
                moved = merge(moved);

            grid = Utils.RotateMatrix(moved, 3, true);
        }

        public void MoveRight()
        {
            var moved = Utils.FlipVertical(grid);
                moved = merge(moved);

            grid = Utils.FlipVertical(moved);
        }

        public void MoveBottom()
        {
            var moved = Utils.RotateMatrix(grid, 3, true);
                moved = merge(moved);

            grid = Utils.RotateMatrix(moved, 3, false);
        }

        public void MoveLeft()
        {
            grid = merge(grid);
        }

        public bool canMove()
        {
            if (EmptyTiles().Count > 0)
            {
                return true;
            }

            for (byte i = 0; i < size; i++)
            {
                for (byte j = 0; j < size; j++)
                {
                    if(j < size - 1 && grid[i, j] == grid[i, j+1]) {
                        return true;
                    }

                    if(i < size - 1 && grid[i, j] == grid[i+1, j]) {
                        return true;
                    }
                }
            }

            return false;
        }

        // merge tiles from right to left
        private ushort[,] merge(ushort[,] grid)
        {
            lastMergedValues.Clear();
            ushort[,] merged = new ushort[size, size];
            isMoved = false;

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

                        if (offset != 0) { isMoved = true; }

                        // we can add value to this block next time, so:
                        offset++;
                    }
                    // we can add block with the same value ex. 2 && 2
                    else if (merged[i, j - offset] == grid[i, j] && grid[i, j] != 0)
                    {
                        merged[i, j - offset] *= 2;
                        // we can't add any value to this block, so we can't increment offset
                        // we add this value to last merged values
                        lastMergedValues.Add(merged[i, j - offset]);

                        // we moved block, so:
                        isMoved = true;
                    }
                    // we can't add block to block with diffrent value
                    // so add to the next one (offset-1)
                    else
                    {
                        merged[i, j - offset + 1] = grid[i, j];

                        // we moved block, so
                        if (offset != 1) {
                            isMoved = true; 
                        }
                    }
                }
            }

            return merged;
        }
    }
}
