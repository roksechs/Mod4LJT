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
            machineInspectorUI = gameObject.AddComponent<MachineInspectorUI>();
            machineInspectorUI.enabled = false;

            RegisterToDelegate();

            LocalisationFile.ReadLocalisationFile();
            LJTMessages.CreateMessageTypes();

            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }

        private static void RegisterToDelegate()
        {
            ReferenceMaster.onBlockPlaced += BlockIntialiser.Instance.AddBlockScript;

            ReferenceMaster.onMachinePostLoad += delegate (Machine machine)
            {

            };

            StatMaster.inMenuChanged += delegate
            {

                if (StatMaster.inMenu)
                    machineInspectorUI.enabled = false;
                else
                    machineInspectorUI.enabled = true;
            };

            ReferenceMaster.onLevelSimulation += delegate (bool flag)
            {
                EntryPoint.Log($"ReferenceMaster.onLevelSimulation:{flag}");
            };

            ReferenceMaster.onMachineSimulation += delegate (bool flag)
            {
                EntryPoint.Log($"ReferenceMaster.onMachineSimulation:{flag}");
                machineInspectorUI.enabled = !flag;
            };

            ReferenceMaster.onSceneLoaded += delegate
            {
                if (StatMaster.isMainMenu)
                    machineInspectorUI.enabled = false;
                else
                    machineInspectorUI.enabled = true;
            };

            BlockMapper.onMapperOpen += BoundResetter.ResetBound;
        }
    }
}
