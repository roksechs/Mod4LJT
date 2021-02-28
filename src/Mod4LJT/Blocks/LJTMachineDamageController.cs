using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mod4LJT.Blocks
{
    class LJTMachineDamageController : SingleInstance<LJTMachineDamageController>
    {
        readonly Dictionary<ServerMachine, GameObject> weakPointDic = new Dictionary<ServerMachine, GameObject>();
        readonly Dictionary<ServerMachine, bool> damagedBoolDic = new Dictionary<ServerMachine, bool>();

        void Awake()
        {
            SceneManager.activeSceneChanged += (x, y) =>
            {
                this.weakPointDic.Clear();
                this.damagedBoolDic.Clear();
                Mod.Log("Cleared.");
            };
        }

        public void AddWeakPoint(ServerMachine serverMachine, GameObject weakPoint)
        {
            if (this.weakPointDic.ContainsKey(serverMachine))
            {
                this.weakPointDic[serverMachine] = weakPoint;
                this.damagedBoolDic[serverMachine] = false;
            }
            else
            {
                this.weakPointDic.Add(serverMachine, weakPoint);
                this.damagedBoolDic.Add(serverMachine, weakPoint);
                //Mod.Log("Added Weak Point");
            }
        }

        void Update()
        {
            if (StatMaster._customLevelSimulating)
            {
                foreach(var kvp in this.weakPointDic)
                {
                    if (!kvp.Value.activeSelf && !this.damagedBoolDic[kvp.Key])
                    {
                        kvp.Key.DamageController.AddTotalDamage(1f);
                        kvp.Key.DamageController.ApplyJointDamage(1000f);
                        kvp.Key.DamageController.RemoveTotalDamage(1f);
                        this.damagedBoolDic[kvp.Key] = true;
                        //Mod.Log("Damaged.");
                    }
                }
            }
        }

        public override string Name => "LJT Machine Damage Controller";
    }
}
