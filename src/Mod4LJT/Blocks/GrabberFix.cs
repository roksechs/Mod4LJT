using UnityEngine;
using Mod4LJT.Regulation;

namespace Mod4LJT.Blocks
{
    class GrabberFix : MonoBehaviour
    {
        Joint joint;

        void Start()
        {
            if (StatMaster.levelSimulating && (!StatMaster.isClient || StatMaster.isLocalSim))
            {
                if (MachineInspector.Instance.isJunkTank) return;
                this.joint = this.GetComponent<Joint>();
                joint.breakForce = 20000f;
                joint.breakTorque = 20000f;
            }
        }
    }
}
