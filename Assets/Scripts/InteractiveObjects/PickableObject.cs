using System.ComponentModel;
using Assets.Scripts.Character;
using Assets.Scripts.InteractiveObjects.Interfaces;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.InteractiveObjects
{
    [Description("Pickable Object")]
    public class PickableObject : SceneObject, IPickableItem
    {
        public PickableObject()
        {
            CanMove = true;
            IsUsable = true;
        }

        public void UseObject(IGameCharacter gameCharacter)
        {
            gameCharacter.AddInventoryItem(LinkedItemX, LinkedItemY,Name);
            Destroy(gameObject);
        }

        public int LinkedItemX { get; set; }
        public int LinkedItemY { get; set; }
    }
}
