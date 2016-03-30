using System.ComponentModel;
using Assets.Scripts.Character;
using Assets.Scripts.InteractiveObjects.Interfaces;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.InteractiveObjects
{
    [Description("Key Door")]
    public class KeyDoor : SceneObject, IUsableObject
    {
        public void UseObject(IGameCharacter gameCharacter)
        {
            gameCharacter.UseKeyDoor(this);
        }

        public void OpenDoor()
        {
            CanMove = true;
        }

        private KeyDoor()
        {
            IsUsable = true;
        }
    }
}
