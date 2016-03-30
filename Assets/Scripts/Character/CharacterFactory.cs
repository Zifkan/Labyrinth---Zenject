using UnityEngine;
using Zenject;

namespace Assets.Scripts.Character
{
    public class CharacterFactory
    {
        readonly DiContainer _container;
        readonly GameObject _prefab;

        public CharacterFactory(GameObject prefab, DiContainer container)
        {
            _container = container;
            _prefab = prefab;
        }

        public PlayerGameCharacter CreatePlayer()
        {
            return _container.InstantiateComponent<PlayerGameCharacter>(CreateCharacterGameObject());
        }

        //public AiCharacter CreateAi()
        //{
        //    return _container.InstantiateComponent<AiCharacter>(CreateCharacterGameObject());
        //}

        GameObject CreateCharacterGameObject()
        {
            // We could just use GameObject.Instantiate as well
            // but this way it will inject on any MonoBehaviour's on the prefab
            return _container.InstantiatePrefab(_prefab);
        }
    }
}
