using UnityEngine;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        void Start()
        {
            if (StatMaster._customLevelSimulating && StatMaster.isHosting)
                LJTMachineDamageController.Instance.AddWeakPoint(this.transform.parent.parent.GetComponent<ServerMachine>(), this.gameObject);
        }
    }
}
