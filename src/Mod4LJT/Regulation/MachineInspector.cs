using Mod4LJT.Blocks;
using Modding.Blocks;
using System;
using System.Collections.Generic;
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
