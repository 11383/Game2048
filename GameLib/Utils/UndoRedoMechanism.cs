using System;
using System.Collections.Generic;

namespace GameLib
{
    public class UndoRedoMechanism<T>
    {
        private readonly int maxSteps;
        public List<T> steps;
        private int index;

        public UndoRedoMechanism(int maxSteps)
        {
            this.maxSteps = maxSteps;
            this.steps = new List<T>();
        }

        public void AddStep(T snapshot)
        {
            int toRemove = steps.Count - index - 1;

            if (toRemove > 0)
            {
                steps.RemoveRange(index + 1, toRemove);
            }

            if (steps.Count == maxSteps)
            {
                steps.RemoveAt(0);
            }

            if (steps.Count > 1 && !steps[steps.Count - 1].Equals(snapshot))
            {
                return;
            }

            steps.Add(snapshot);
            index = steps.Count;
        }

        public virtual bool CanRedo()
        {
            return index < steps.Count - 1;
        }

        public virtual bool CanUndo()
        {
            return index > 0;
        }

        public T Redo()
        {
            if (!CanRedo())
            {
                throw new Exception("Can't redo. Out of range");
            }

            index++;
            OnUndoRedo(steps[index]);
            return steps[index];
        }

        public T Undo()
        {
            if (!CanUndo())
            {
                throw new Exception("Can't undo. Out of range");
            }

            --index;
            OnUndoRedo(steps[index]);
            return steps[index];
        }

        public void Clear()
        {
            index = 0;
            steps.Clear();
        }

        public virtual void OnUndoRedo(T state)
        {}
    }
}
