using Mod4LJT.Blocks;
using Mod4LJT.ModLocalisation;
using Mod4LJT.Network;
using Mod4LJT.Regulation;
using Modding;
using System;
using UnityEngine;

namespace Mod4LJT
{
    static class Mod
    {
        static GameObject gameObject;
        internal static MachineInspectorUI machineInspectorUI;

        public static void OnLoad()
        {
            EntryPoint.Log("version " + Mods.GetVersion(new Guid("4713d96a-ce6c-4556-8bf4-7dc838b52973")));

            gameObject = new GameObject("Mod4LJT");
            gameObject.AddComponent<UI.PlayerLabelManager>();

            RegisterDelegate();

            LocalisationFile.ReadLocalisationFile();
            LJTMessages.CreateMessageTypes();

            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }

        private static void RegisterDelegate()
        {
            Events.OnBlockInit += BlockScriptManager.Instance.AddBlockScript;

            ReferenceMaster.onSceneLoaded += delegate ()
            {
                GameObject gameObjectScene = new GameObject("Mod4LJT_Scene");
                machineInspectorUI = gameObjectScene.AddComponent<MachineInspectorUI>();
            };

            BlockMapper.onMapperOpen += BoundResetter.ResetBound;
        }
    }
}
