using UnityEngine;
using Modding;

namespace Mod4LJT
{
	public class Mod : ModEntryPoint
	{
		public override void OnLoad()
		{
			GameObject mod = new GameObject("Mod4LJT");
			UnityEngine.Object.DontDestroyOnLoad(mod);
			this.AddBlockScripts();
		}


        public void AddBlockScripts()
        {
			BlockBehaviour BB;
			PrefabMaster.GetBlock(BlockType.Grabber, out BB);
			BB.gameObject.AddComponent<GrabberFix>();
			PrefabMaster.GetBlock(BlockType.Bomb, out BB);
			BB.gameObject.AddComponent<BombFix>();
        }

		public static void Log(string message) => Debug.Log("4LJT Log: " + message);
		public static void Warning(string msg) => Debug.LogWarning("4LJT Warning: " + msg);
		public static void Error(string msg) => Debug.LogError("4LJT Error: " + msg);
	}
}
