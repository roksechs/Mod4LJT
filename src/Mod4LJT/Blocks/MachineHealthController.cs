using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mod4LJT.Blocks
{
    class MachineHealthController : MonoBehaviour
    {
        GameObject weakPoint;

        void Update()
        {
            if (StatMaster._customLevelSimulating)
            {
                if (!this.weakPoint) return;
                if (!this.weakPoint.activeSelf)
                {
                    (Machine.Active() as ServerMachine).DamageController.ApplyBlockDamage(this.weakPoint.GetComponent<BlockBehaviour>(), 10000f);
                    Mod.Log("Damaged");
                }
            }
        }

        public void SetWeakPoint(GameObject weakPoint)
        {
            this.weakPoint = weakPoint;
        }
    }
}
