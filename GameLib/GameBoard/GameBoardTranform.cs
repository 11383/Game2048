using System.Collections.Generic;

namespace GameLib
{
    partial class GameBoard
    {
        public enum TransformType { New, Tranlate, Merge }

        public struct GameTranform {
            public int X;
            public int Y;
            public int LastX;
            public int LastY;
            public TransformType Type;
        };

        private List<GameTranform> transforms = new List<GameTranform>();

        /* Add transform created in current move */
        private void AddTransform(int x, int y, int lastX, int lastY, TransformType type)
        {
            transforms.Add(new GameTranform {
                X = x,
                Y = y,
                LastX = lastX,
                LastY = lastY,
                Type = type
            });
        }

        /* Clear transforms */
        private void ClearTransforms() => transforms.Clear();

        /* Get list of transforms since last move */
        public List<GameTranform> Transforms() => transforms;
    }
}
