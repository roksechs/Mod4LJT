using Mod4LJT.Blocks;
using Mod4LJT.Localisation;
using Mod4LJT.Network;
using Mod4LJT.Regulation;
using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mod4LJT
{
    public class EntryPoint : ModEntryPoint
    {

        public override void OnLoad()
        {
            Mod4LJT.OnLoad();

            LocalisationFile.ReadLocalisationFile();
            LJTMessages.CreateMessageTypes();
            //ReferenceMaster.onSceneLoaded += delegate ()
            //{
            //    Scene scene = SceneManager.GetActiveScene();
            //    Log($"Scene of this mod GameObject : {this.mod.scene.name}");
            //    Log($"Scene of this mod GameObject : {this.mod.scene.path}");
            //    Log($"Scene of this mod GameObject : {this.mod.scene.isLoaded}");
            //    Log($"Scene of this mod GameObject : {this.mod.scene.IsValid()}");
            //    //SceneManager.MoveGameObjectToScene(this.mod, scene);
            //    //Log($"Scene of this mod GameObject : {this.mod.scene.name}");
            //    Log($"Root object of this mod GameObject : {this.mod.transform.root.gameObject.name}");
            //    Log($"Active Scene : {scene.name}");
            //    foreach (GameObject gameObject in scene.GetRootGameObjects())
            //    {
            //        Log($"Root GameObject in {scene.name} : {gameObject.name}");
            //        //foreach (Component childComponent in gameObject.GetComponents<Component>())
            //        //{
            //        //    Log($"Root GameObject in {gameObject.name} : {childComponent.name}");

            //        //}
            //    }
            //    //for (int index = 0; index < SceneManager.sceneCount; index++)
            //    //{
            //    //    scene = SceneManager.GetSceneAt(index);
            //    //    Log($"Scenes : {scene.name}");
            //    //}
            //};
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
