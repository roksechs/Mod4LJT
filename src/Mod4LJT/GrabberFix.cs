using UnityEngine;

namespace Mod4LJT
{
    class GrabberFix : MonoBehaviour
    {
        Joint joint;
        bool firstFrame;

        public void Awake()
        {
            Mod.Log("Awaken");
        }

        public void Start()
        {
            this.joint = this.GetComponent<Joint>();
            joint.breakForce = 35000f;
            joint.breakTorque = 35000f;
            Mod.Log("Started");
            this.firstFrame = true;
        }

        public void Update()
        {
            if (this.firstFrame)
            {
                this.firstFrame = false;
                Mod.Log("Grabber's BreakForce is " + this.joint.breakForce.ToString());
            }
        }
    }
}
