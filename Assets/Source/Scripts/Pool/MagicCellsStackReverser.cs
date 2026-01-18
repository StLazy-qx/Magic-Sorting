using Assets.Source.Scripts.MagicCells;
using System;
using System.Collections.Generic;

namespace Assets.Source.Scripts.Pool
{
    class MagicCellsStackReverser
    {
        private Stack<MagicCell> _reversedStack;

        private void Reverse(Stack<MagicCell> sourceStack)
        {
            if (sourceStack == null)
                throw new ArgumentNullException(nameof(sourceStack));

            _reversedStack = new Stack<MagicCell>();

            while (sourceStack.Count > 0)
                _reversedStack.Push(sourceStack.Pop());
        }

        public Stack<MagicCell> GetReversedStack()
        {
            if (_reversedStack == null)
                throw new InvalidOperationException("Stack was not reversed yet");

            return _reversedStack;
        }
    }
}
