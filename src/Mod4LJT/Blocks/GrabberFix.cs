using Mod4LJT.Regulation;
using Modding.Blocks;
using UnityEngine;

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
                        this.joint.breakForce = 20000f;
                        this.joint.breakTorque = 20000f;
                    }
                }
                else
                {
                    if (MachineInspector.Instance.isJunkTank) return;
                    this.joint = this.GetComponent<Joint>();
                    this.joint.breakForce = 20000f;
                    this.joint.breakTorque = 20000f;
                }
            }
        }
    }
}
