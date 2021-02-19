using UnityEngine;

namespace Mod4LJT.Blocks
    {
    class BombFix : AbstractBlock
    {
        Rigidbody rigidbody;

        void Start()
        {
            this.rigidbody = this.GetComponent<Rigidbody>();
            //this.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }
}
