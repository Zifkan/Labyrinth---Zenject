using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.EventHandlers;

namespace Assets.Scripts.EventDispatcher
{
    public class ObjectEventDispatcher
    {
        private readonly List<Listener> _listeners = new List<Listener>();


        public void AddListener(Listener listener)
        {
            _listeners.Add(listener);
        }

        public void ExecuteEvent(int linkerItemX, int linkerItemY, UsedObjectEventArgs usedObjectEventArgs)
        {
            foreach (var listener in _listeners.Where(listener => listener.LinkerItemX == linkerItemX && listener.LinkerItemY == linkerItemY))
            {
                listener.UseObjectMethod(usedObjectEventArgs);
            }
        }

        public class Listener
        {
            public int LinkerItemX;
            public int LinkerItemY;
            public Action<UsedObjectEventArgs> UseObjectMethod;
        }
    }
}
