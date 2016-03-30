using System;
using System.Collections.Generic;
using Assets.Scripts.EventHandlers;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.GameInterface
{
    public interface IGameInterface
    {
        event EventHandler<MenuButtonEventArgs> MenuButtonClick;
        void SetLevelPanel(List<LevelConfig> lvlList);
    }
}
