using Modding;
using UnityEngine;

namespace Mod4LJT
{
    public class EntryPoint : ModEntryPoint
    {

        public override void OnLoad()
        {
            Mod.OnLoad();
        }

        public static void Log(string message)
        {
            Debug.Log("Mod4LJT Log: " + message);
        }

        public static void Warning(string msg)
        {
            Debug.LogWarning("Mod4LJT Warning: " + msg);
        }

        public static void Error(string msg)
        {
            Debug.LogError("Mod4LJT Error: " + msg);
        }
    }
}
