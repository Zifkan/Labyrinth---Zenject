using System;
using Assets.Scripts.GameInterface;

namespace Assets.Scripts.EventHandlers
{
    public class MenuButtonEventArgs : EventArgs
    {
        public MainMenuItems Items { get; private set; }

        public MenuButtonEventArgs(MainMenuItems mainMenuItems)
        {
            Items = mainMenuItems;
        }
    }
}
