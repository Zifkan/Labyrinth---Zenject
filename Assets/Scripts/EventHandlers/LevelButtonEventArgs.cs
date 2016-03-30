using System;

namespace Assets.Scripts.EventHandlers
{
    public class LevelButtonEventArgs : EventArgs
    {
        public int LevelId { get; private set; }

        public LevelButtonEventArgs(int lvlId)
        {
            LevelId = lvlId;
        }
    }
}
