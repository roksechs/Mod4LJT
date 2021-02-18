using UnityEngine;

namespace Mod4LJT
{
    class GrabberFix : MonoBehaviour
    {
        Joint joint;

        public void Start()
        {
            this.joint = this.GetComponent<Joint>();
            joint.breakForce = 35000f;
            joint.breakTorque = 35000f;
        }
    }
}
