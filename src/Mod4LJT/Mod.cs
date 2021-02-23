using System;
using System.Linq;
using UnityEngine;
using Modding;
using Modding.Blocks;
using Mod4LJT.Blocks;
using Mod4LJT.Regulation;

namespace Mod4LJT
{
    public class Mod : ModEntryPoint
    {
        private GameObject mod;
        private BoundResetter resetbound;
        private MachineInspector machineInspector;

        public override void OnLoad()
        {
            this.mod = new GameObject("Mod4LJT");
            UnityEngine.Object.DontDestroyOnLoad(mod);
            this.resetbound = BoundResetter.Instance;
            UnityEngine.Object.DontDestroyOnLoad(resetbound);
            this.machineInspector = MachineInspector.Instance;
            UnityEngine.Object.DontDestroyOnLoad(machineInspector);
            LightTank lightTank = new LightTank();
            Events.OnBlockInit += this.AddBlockScript;

        }

        public void AddBlockScript(Block block)
        {
            switch (block.Prefab.InternalObject.Type)
            {
                case BlockType.StartingBlock:
                    MMenu tankTypeMenu = block.InternalObject.AddMenu(new MMenu("tankTypeMenu", 0, Enum.GetNames(typeof(TankType)).ToList(), false));
                    tankTypeMenu.ValueChanged += x =>
                    {
                        MachineInspector.Instance.SetTankType((TankType)x);
                    };
                    //MachineInspector.Instance.OnTypeChangeFromGUI += index =>
                    //{
                    //    tankTypeMenu.Value = index;
                    //};
                    break;
                case BlockType.Grabber:
                    block.GameObject.AddComponent<GrabberFix>();
                    break;
            }
        }

        public static void Log(string message) => Debug.Log("Mod4LJT Log: " + message);
        public static void Warning(string msg) => Debug.LogWarning("Mod4LJT Warning: " + msg);
        public static void Error(string msg) => Debug.LogError("Mod4LJT Error: " + msg);
    }
}
