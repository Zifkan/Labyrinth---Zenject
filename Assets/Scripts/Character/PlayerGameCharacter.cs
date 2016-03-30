using UnityEngine;
using Zenject;

namespace Assets.Scripts.Character
{
    public sealed class PlayerGameCharacter : GameCharacter
    {
        protected override void CheckInput()
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
               MoveForward();
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
               MoveBackward();
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                MoveRight();
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                MoveLeft();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                UseObject();
            }
        }

        public class Factory : GameObjectFactory<PlayerGameCharacter>
        {
        }
    }
}
