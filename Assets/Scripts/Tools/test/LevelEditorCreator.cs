using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Tools.test
{
    public class LevelEditorCreator: IInitializable
    {
        private LevelEditor _levelEditor;

        public LevelEditorCreator(LevelEditor levelEditor)
        {
           
            _levelEditor = levelEditor;
            Debug.Log(_levelEditor);
        }

        public void Initialize()
        {
            _levelEditor.InitEditor();
        }
    }
}
