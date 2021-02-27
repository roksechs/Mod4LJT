using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mod4LJT.Blocks
{
    class MachineHealthController : MonoBehaviour
    {
        BlockBehaviour weakPoint;

        void Update()
        {
            if (StatMaster._customLevelSimulating)
            {
                if (!this.weakPoint) return;
                if (!this.weakPoint.gameObject.activeSelf)
                {
                    (Machine.Active() as ServerMachine).DamageController.ApplyBlockDamage(this.weakPoint, 10000f);
                    Mod.Log("Damaged");
                }
            }
        }

        public void SetWeakPoint(BlockBehaviour weakPoint)
        {
            this.weakPoint = weakPoint;
        }
    }
}
