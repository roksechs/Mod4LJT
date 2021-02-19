using UnityEngine;

namespace Mod4LJT.Blocks
{
    class GrabberFix : AbstractBlock
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
