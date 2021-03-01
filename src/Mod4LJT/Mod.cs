using UnityEngine;
using Modding;
using Mod4LJT.Blocks;
using Mod4LJT.Regulation;
using Mod4LJT.Localisation;

namespace Mod4LJT
{
    public class Mod : ModEntryPoint
    {
        public GameObject mod;
        public static Mod Instance;
        BoundResetter resetbound;
        MachineInspector machineInspector;
        LJTPlayerLabelManager namePlateManager;
        BlockScriptManager blockScriptManager;
        LJTMachineDamageController machineDamageController;

        public override void OnLoad()
        {
            this.mod = new GameObject("Mod4LJT");
            UnityEngine.Object.DontDestroyOnLoad(mod);
            Instance = this;
            this.resetbound = BoundResetter.Instance;
            UnityEngine.Object.DontDestroyOnLoad(resetbound);
            this.machineInspector = MachineInspector.Instance;
            UnityEngine.Object.DontDestroyOnLoad(machineInspector);
            this.namePlateManager = LJTPlayerLabelManager.Instance;
            UnityEngine.Object.DontDestroyOnLoad(namePlateManager);
            this.blockScriptManager = BlockScriptManager.Instance;
            UnityEngine.Object.DontDestroyOnLoad(blockScriptManager);
            this.machineDamageController = LJTMachineDamageController.Instance;
            UnityEngine.Object.DontDestroyOnLoad(machineDamageController);
            Events.OnBlockInit += this.blockScriptManager.AddBlockScript;
            LocalisationFile.ReadLocalisationFile();
        }

        public static void Log(string message) => Debug.Log("Mod4LJT Log: " + message);
        public static void Warning(string msg) => Debug.LogWarning("Mod4LJT Warning: " + msg);
        public static void Error(string msg) => Debug.LogError("Mod4LJT Error: " + msg);
    }
}
