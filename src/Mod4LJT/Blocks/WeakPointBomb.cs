using UnityEngine;
using Mod4LJT.Regulation;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        public Machine machine;

        void Awake()
        {
            if (MachineInspector.Instance.isJunkTank) return;
            if (StatMaster._customLevelSimulating)
            {
                (this.machine as ServerMachine).DamageController.AddTotalDamage(1.0f);
            }
        }

        void OnDestroy()
        {
            if (MachineInspector.Instance.isJunkTank) return;
            if (StatMaster._customLevelSimulating)
            {
                (this.machine as ServerMachine).DamageController.RemoveTotalDamage(1.0f);
            }
        }
    }
}
