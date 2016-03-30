using System;
using Assets.Scripts.InteractiveObjects.Interfaces;

namespace Assets.Scripts.EventHandlers
{
    public class UsedObjectEventArgs : EventArgs
    {
        private readonly IUsableObject _usableObject;
        private readonly int _x;
        private readonly int _y;

        public UsedObjectEventArgs(IUsableObject usableObject, int x, int y)
        {
            _usableObject = usableObject;
            _x = x;
            _y = y;
        }

        public IUsableObject UsableObject
        {
            get { return _usableObject; }
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }
    }
}
