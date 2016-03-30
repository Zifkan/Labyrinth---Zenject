using System.ComponentModel;
using Assets.Scripts.Character;
using Assets.Scripts.EventDispatcher;
using Assets.Scripts.InteractiveObjects.Interfaces;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.InteractiveObjects
{
    [Description("Door")]
    public class Door : SceneObject, IDistanceUsableItem
    {
        public int LinkedItemX { get; set; }
        public int LinkedItemY { get; set; }

        private ObjectEventDispatcher _objectEventDispatcher;

        public ObjectEventDispatcher ObjectEventDispatcher
        {
            get { return _objectEventDispatcher; }
            set
            {

                _objectEventDispatcher = value;
                _objectEventDispatcher.AddListener(new ObjectEventDispatcher.Listener
                {
                    LinkerItemX = ParentX,
                    LinkerItemY = ParentY,
                    UseObjectMethod = args =>
                    {
                        CanMove = true;
                    }
                });

            }
        }

        public Door()
        {
            CanMove = false;
            IsUsable = true;
        }

        public void UseObject(IGameCharacter gameCharacter)
        {
            
        }
    }
}
