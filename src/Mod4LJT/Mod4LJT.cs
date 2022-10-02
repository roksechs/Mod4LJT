using Mod4LJT.Blocks;
using Mod4LJT.Localisation;
using Mod4LJT.Regulation;
using Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mod4LJT
{
    static class Mod4LJT
    {
        static GameObject gameObject;

        public static void OnLoad()
        {
            gameObject = new GameObject("Mod4LJT");
            gameObject.AddComponent<BoundResetter>();
            gameObject.AddComponent<MachineInspector>();
            gameObject.AddComponent<PlayerLabelManager>();

            Events.OnBlockInit += BlockScriptManager.AddBlockScript;
            ReferenceMaster.onSceneLoaded += delegate ()
            {
                GameObject gameObjectScene = new GameObject("Mod4LJT_Scene");
                gameObjectScene.AddComponent<LocalisationManager>();
            };

            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }
    }
}
