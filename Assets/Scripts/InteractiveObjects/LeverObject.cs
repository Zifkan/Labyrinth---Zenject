using System.ComponentModel;
using Assets.Scripts.Character;
using Assets.Scripts.EventDispatcher;
using Assets.Scripts.EventHandlers;
using Assets.Scripts.InteractiveObjects.Interfaces;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.InteractiveObjects
{
    [Description("Lever Object")]
    public class LeverObject : SceneObject, IDistanceUsableItem
    {
        public int LinkedItemX { get; set; }
        public int LinkedItemY { get; set; }
        public ObjectEventDispatcher ObjectEventDispatcher { get; set; }

        public LeverObject()
        {
            CanMove = true;
            IsUsable = true;
        }
        public void UseObject(IGameCharacter gameCharacter)
        {
            ObjectEventDispatcher.ExecuteEvent(LinkedItemX, LinkedItemY, new UsedObjectEventArgs(this, LinkedItemX, LinkedItemY));
        }

       
    }
}
