using Mod4LJT.Regulation;
using UnityEngine;

namespace Mod4LJT.Blocks
{
    class GrabberFix : MonoBehaviour
    {
        Joint joint;
        public LJTMachine ljtMachine;

        void Start()
        {
            if (StatMaster.levelSimulating && (!StatMaster.isClient || StatMaster.isLocalSim))
            {
                if (!this.ljtMachine.TankType.Equals(TankType.JunkTank))
                {
                    this.joint = this.gameObject.GetComponent<Joint>();
                    this.joint.breakForce = 20000f;
                    this.joint.breakTorque = 20000f;
                }
            }
        }
    }
}
