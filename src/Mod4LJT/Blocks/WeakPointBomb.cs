using UnityEngine;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        void Awake()
        {
            if (StatMaster._customLevelSimulating)
                LJTMachineDamageController.Instance.AddWeakPoint(this.transform.parent.parent.GetComponent<ServerMachine>(), this.gameObject);
        }
    }
}
