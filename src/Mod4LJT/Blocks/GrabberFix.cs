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
                    if (LJTMachine.MachineDic.TryGetValue(this.transform.parent.parent.gameObject.GetComponent<Machine>().PlayerID, out LJTMachine ljtMachine))
                    {
                        if (ljtMachine.TankTypeInt != 5)
                        {
                            this.joint = this.GetComponent<Joint>();
                            joint.breakForce = 20000f;
                            joint.breakTorque = 20000f;
                        }
                    }
                }
                else
                {
                    if (!MachineInspector.Instance.isJunkTank)
                    {
                        this.joint = this.GetComponent<Joint>();
                        joint.breakForce = 20000f;
                        joint.breakTorque = 20000f;
                    }
                }
            }
        }
    }
}
