using System.Collections.Generic;

namespace GameLib
{
    public partial class GameBoard
    {
        // return if last move actually moved grid
        public bool isMoved = false;
        // List of merged values after last move
        public List<ushort> lastMergedValues = new List<ushort>();

        // Move to top
        public void MoveTop()
        {
            var moved = ArrayTransform.RotateMatrix(Grid, false);
            moved = CollapseLeft(moved);

            Grid = ArrayTransform.RotateMatrix(moved, true);

            // transform GameTransforms from left direction to right
            TransformTop();
        }

        // Move to right
        public void MoveRight()
        {
            var moved = ArrayTransform.FlipVertical(Grid);
            moved = CollapseLeft(moved);

            Grid = ArrayTransform.FlipVertical(moved);

            // transform GameTransforms from left direction to right
            TransformRight();
        }

        // Move to bottom
        public void MoveBottom()
        {
            var moved = ArrayTransform.RotateMatrix(Grid, true);
            moved = CollapseLeft(moved);

            Grid = ArrayTransform.RotateMatrix(moved, false);

            // transform GameTransforms from left direction to right
            TransformBottom();
        }

        // Move to left
        public void MoveLeft()
        {
            Grid = CollapseLeft(Grid);
        }

        // Check available moves
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
            // clear lastMergedValues && transforms from last move
            lastMergedValues.Clear();
            ClearTransforms();

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

                        if (offset != 0)
                        {
                            SetMoved(i, j - offset, i, j);
                        }

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
                        SetMoved(i, j - offset, i, j, true);
                    }
                    // we can't add block to block with diffrent value
                    // so add to the next one (offset-1)
                    else
                    {
                        merged[i, j - offset + 1] = grid[i, j];

                        // we moved block, so
                        if (offset != 1)
                        {
                            SetMoved(i, j - offset + 1, i, j);
                        }
                    }
                }
            }

            return merged;
        }

        // Set flag that one of blocks has moved
        private void SetMoved(int y, int x, int lastY, int lastX, bool merged = false)
        {
            isMoved = true;

            // Log this action (block moved|translated) to tranforms
            AddTransform(x, y, lastX, lastY, merged ? TransformType.Merge : TransformType.Tranlate);
        }
    }
}
