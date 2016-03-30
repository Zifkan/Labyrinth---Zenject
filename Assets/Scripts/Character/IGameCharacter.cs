using Assets.Scripts.InteractiveObjects;

namespace Assets.Scripts.Character
{
    public interface IGameCharacter
    {
        void AddInventoryItem(int linkedItemX, int linkedItemY, string itemName);
        void GrabObject(GrabObject grabObject);

        void UseKeyDoor(KeyDoor keyDoor);
    }
}
