using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Containers;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Tools.test
{
    public class LevelEditorInstaller : Editor
    {
        private static DiContainer _diContainer;

        [MenuItem("Window/Level Editor Installer")]
        private static void Init()
        {
            _diContainer = new DiContainer();
            
            

            _diContainer.Bind<LevelEditorCreator>().ToSingle();
            _diContainer.Bind<IInitializable>().ToSingle<LevelEditorCreator>();
            _diContainer.Bind<LevelEditor>().ToSingle();
        }


    }
}
