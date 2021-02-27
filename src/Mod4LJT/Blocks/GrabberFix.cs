using UnityEngine;

namespace Mod4LJT.Blocks
{
    class GrabberFix : MonoBehaviour
    {
        Joint joint;

        void Start()
        {
            if (Machine.Active().isSimulating)
            {
                this.joint = this.GetComponent<Joint>();
                joint.breakForce = 20000f;
                joint.breakTorque = 20000f;
                //Mod.Log("joint force changed");
            }
        }
    }
}
