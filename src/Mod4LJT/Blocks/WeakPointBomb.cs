using UnityEngine;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        public bool isWeakPoint;

        void Awake()
        {
            if (this.isWeakPoint && StatMaster._customLevelSimulating)
                LJTMachineDamageController.Instance.AddWeakPoint(this.transform.parent.parent.GetComponent<ServerMachine>(), this.gameObject);
        }
    }
}
