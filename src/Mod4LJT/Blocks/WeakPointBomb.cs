using UnityEngine;
using Mod4LJT.Regulation;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        void Awake()
        {
            MachineInspector.Instance.weakPointCount++;
            if (StatMaster._customLevelSimulating)
            {
                (Machine.Active() as ServerMachine).DamageController.AddTotalDamage(1.0f);
                LJTReferenceMaster.Instance.machineHealthController.SetWeakPoint(this.gameObject);
            }
        }

        void OnDestroy()
        {
            if (StatMaster._customLevelSimulating)
            {
                (Machine.Active() as ServerMachine).DamageController.RemoveTotalDamage(1.0f);
                Mod.Log("Destroyed");
            }
            else if (!StatMaster.levelSimulating)
            {
                MachineInspector.Instance.weakPointCount--;
            }
        }
    }
}
