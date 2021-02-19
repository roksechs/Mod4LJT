using UnityEngine;
using Modding;
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
			this.AddBlockScripts();
		}


        public void AddBlockScripts()
        {
			BlockBehaviour BB;
			PrefabMaster.GetBlock(BlockType.StartingBlock, out BB);
			BB.gameObject.AddComponent<TankTypeRegister>();
			PrefabMaster.GetBlock(BlockType.Grabber, out BB);
			BB.gameObject.AddComponent<GrabberFix>();
			PrefabMaster.GetBlock(BlockType.Bomb, out BB);
			BB.gameObject.AddComponent<BombFix>();
        }

		public static void Log(string message) => Debug.Log("Mod4LJT Log: " + message);
		public static void Warning(string msg) => Debug.LogWarning("Mod4LJT Warning: " + msg);
		public static void Error(string msg) => Debug.LogError("Mod4LJT Error: " + msg);
	}
}
