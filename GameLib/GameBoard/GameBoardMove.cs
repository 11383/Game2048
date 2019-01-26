using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public partial class GameBoard
    {
        public void MoveTop()
        {
            var moved = ArrayTransform.RotateMatrix(Grid, 3, false);
            moved = CollapseLeft(moved);

            Grid = ArrayTransform.RotateMatrix(moved, 3, true);
        }

        public void MoveRight()
        {
            var moved = ArrayTransform.FlipVertical(Grid);
            moved = CollapseLeft(moved);

            Grid = ArrayTransform.FlipVertical(moved);
        }

        public void MoveBottom()
        {
            var moved = ArrayTransform.RotateMatrix(Grid, 3, true);
            moved = CollapseLeft(moved);

            Grid = ArrayTransform.RotateMatrix(moved, 3, false);
        }

        public void MoveLeft()
        {
            Grid = CollapseLeft(Grid);
        }

        public bool CanMove()
        {
            if (GetEmptyTilesPos().Count > 0)
            {
                return true;
            }

            for (byte i = 0; i < size; i++)
            {
                for (byte j = 0; j < size; j++)
                {
                    if (j < size - 1 && Grid[i, j] == Grid[i, j + 1])
                    {
                        return true;
                    }

                    if (i < size - 1 && Grid[i, j] == Grid[i + 1, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // merge tiles from right to left of given grid and return new merged array
        private ushort[,] CollapseLeft(ushort[,] grid)
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
                    // we can merge blocks with the same values ex. 2 && 2
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
                        if (offset != 1)
                        {
                            isMoved = true;
                        }
                    }
                }
            }

            return merged;
        }
    }
}
