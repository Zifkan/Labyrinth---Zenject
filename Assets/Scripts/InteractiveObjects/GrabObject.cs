using System.ComponentModel;
using Assets.Scripts.Character;
using Assets.Scripts.EventDispatcher;
using Assets.Scripts.InteractiveObjects.Interfaces;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.InteractiveObjects
{
    [Description("GrabObject")]
    public class GrabObject : SceneObject, IUsableObject
    {
        public GrabObject()
        {
            CanMove = false;
            IsUsable = true;
        }

        public void UseObject(IGameCharacter gameCharacter)
        {
            gameCharacter.GrabObject(this);
        }
    }
}
