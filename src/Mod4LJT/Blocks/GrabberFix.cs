using UnityEngine;

namespace Mod4LJT.Blocks
{
    class GrabberFix : MonoBehaviour
    {
        Joint joint;

        void Start()
        {
            if (StatMaster.levelSimulating)
            {
                this.joint = this.GetComponent<Joint>();
                joint.breakForce = 20000f;
                joint.breakTorque = 20000f;
            }
        }
    }
}
