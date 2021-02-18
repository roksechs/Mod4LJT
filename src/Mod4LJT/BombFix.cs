using UnityEngine;

namespace Mod4LJT
{
    class BombFix : MonoBehaviour
    {
        Rigidbody rigidbody;

        void Start()
        {
            this.rigidbody = this.GetComponent<Rigidbody>();
            this.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }
}
