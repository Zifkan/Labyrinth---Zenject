using Assets.Scripts.EventDispatcher;

namespace Assets.Scripts.InteractiveObjects.Interfaces
{
    public interface IDistanceUsableItem : IUsableObject
    {
        int LinkedItemX { get; set; }

        int LinkedItemY { get; set; }
        ObjectEventDispatcher ObjectEventDispatcher { get; set; }
    }
}
