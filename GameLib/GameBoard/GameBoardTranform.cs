using System;
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

        private List<GameTranform> MakeTransform(List<GameTranform> source, Func<GameTranform, GameTranform> p)
        {
            var result = new List<GameTranform>();

            source.ForEach(item => { result.Add(p(item)); });

            return result;

        }

        private void TransformRight()
        {
            transforms = MakeTransform(transforms, (item) => {
                item.X = size - item.X - 1;
                item.LastX = size - item.LastX - 1;

                return item;
            });
        }

        private void TransformTop()
        {
            transforms = MakeTransform(transforms, (item) =>
            {
                Utils.Common.Swap(ref item.Y, ref item.X);
                Utils.Common.Swap(ref item.LastY, ref item.LastX);
                item.X = size - item.X - 1;
                item.LastX = size - item.LastX - 1;

                return item;
            });
        }

        private void TransformBottom()
        {
            transforms = MakeTransform(transforms, item =>
            {
                Utils.Common.Swap(ref item.Y, ref item.X);
                Utils.Common.Swap(ref item.LastY, ref item.LastX);
                item.Y = size - item.Y - 1;
                item.LastY = size - item.LastY - 1;

                return item;
            });
        }


    }
}
