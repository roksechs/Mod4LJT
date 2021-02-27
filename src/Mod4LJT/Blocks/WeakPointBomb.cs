using UnityEngine;
using Mod4LJT.Regulation;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        void Awake()
        {
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
            }
        }
    }
}
