using Mod4LJT.Blocks;
using UnityEngine;

namespace Mod4LJT.Regulation
{
    class MachineInspector : MonoBehaviour
    {
        private Machine machine;
        private LJTMachine ljtMachine;

        void Awake()
        {
            this.machine = this.gameObject.GetComponent<Machine>();
        }

        public void Start()
        {
        }

        void LateUpdate()
        {

        }
    }
}
