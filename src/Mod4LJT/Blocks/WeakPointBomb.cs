﻿using UnityEngine;
using Modding.Blocks;

namespace Mod4LJT.Blocks
{
    class WeakPointBomb : MonoBehaviour
    {
        public bool isWeakPoint;

        void Awake()
        {
            if (this.isWeakPoint && StatMaster.isMP)
            {
                LJTMachine.MachineDic.TryGetValue(PlayerMachine.From(this.transform.parent.parent.gameObject), out LJTMachine lJTMachine);
                lJTMachine.WeakPointObject = this.gameObject;
                Mod.Log("Resister weak point");
            }
        }
    }
}
