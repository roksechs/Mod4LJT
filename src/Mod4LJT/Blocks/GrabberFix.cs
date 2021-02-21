using System.Collections.Generic;
using UnityEngine;

namespace Mod4LJT.Blocks
{
    class GrabberFix : AbstractBlock
    {
        Joint joint;

        void Start()
        {
            this.joint = this.GetComponent<Joint>();
            joint.breakForce = 20000f;
            joint.breakTorque = 20000f;
        }
    }
}
