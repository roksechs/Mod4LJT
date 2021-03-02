using UnityEngine;
using Modding.Blocks;
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
                if (StatMaster.isMP)
                {
                    if (LJTMachine.MachineDic.TryGetValue(PlayerMachine.From(this.transform.parent.parent.gameObject), out LJTMachine ljtMachine))
                    {
                        if (ljtMachine.TankTypeInt == 5) return;
                        this.joint = this.GetComponent<Joint>();
                        joint.breakForce = 20000f;
                        joint.breakTorque = 20000f;
                    }
                }
                else
                {
                    if (MachineInspector.Instance.isJunkTank) return;
                    this.joint = this.GetComponent<Joint>();
                    joint.breakForce = 20000f;
                    joint.breakTorque = 20000f;
                }
            }
        }
    }
}
