using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mod4LJT
{
    class LJTMachineDamageController : SingleInstance<LJTMachineDamageController>
    {
        readonly Dictionary<ServerMachine, GameObject> weakPointDic = new Dictionary<ServerMachine, GameObject>();
        bool isEempty = true;

        void Awake()
        {
            SceneManager.activeSceneChanged += (x, y) =>
            {
                this.weakPointDic.Clear();
                this.isEempty = true;
            };
        }

        public void AddWeakPoint(ServerMachine serverMachine, GameObject weakPoint)
        {
            if (this.weakPointDic.ContainsKey(serverMachine))
            {
                this.weakPointDic[serverMachine] = weakPoint;
            }
            else
            {
                this.weakPointDic.Add(serverMachine, weakPoint);
                this.isEempty = false;
            }
        }

        public void RemoveWeakPoint(ServerMachine serverMachine, GameObject weakPoint)
        {
            if (this.weakPointDic.ContainsKey(serverMachine))
            {
                this.weakPointDic.Remove(serverMachine);
            }
        }

        void Update()
        {
            if (!this.isEempty && StatMaster._customLevelSimulating)
            {
                foreach (var kvp in this.weakPointDic)
                {
                    if (!kvp.Value.activeSelf && kvp.Key.Health > 0)
                    {
                        kvp.Key.DamageController.AddTotalDamage(1f);
                        kvp.Key.DamageController.ApplyJointDamage(1000f);
                        kvp.Key.DamageController.RemoveTotalDamage(1f);
                        kvp.Key.DamageController.Toggle(false);
                    }
                }
            }
        }

        public override string Name => "LJT Machine Damage Controller";
    }
}
