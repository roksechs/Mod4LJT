﻿using Modding.Blocks;
using UnityEngine;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        public bool isWeakPoint;

        void Start()
        {
            if (this.isWeakPoint && StatMaster.isMP)
            {
                LJTMachine.MachineDic.TryGetValue(this.transform.parent.parent.gameObject.GetComponent<Machine>().PlayerID, out LJTMachine lJTMachine);
                lJTMachine.WeakPointObject = this.gameObject;
            }
        }
    }
}
