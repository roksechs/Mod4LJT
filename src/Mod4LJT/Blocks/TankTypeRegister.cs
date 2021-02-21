using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Mod4LJT.Regulation;

namespace Mod4LJT.Blocks
{
    class TankTypeRegister : AbstractBlock
    {
        public MMenu tankTypeMenu;

        internal override void Awake()
        {
            base.Awake();
            if (this.BB.isSimulating) return;
            tankTypeMenu = new MMenu("tankTypeMenu", 0, Enum.GetNames(typeof(TankType)).ToList(), false);
            this.BB.AddMenu(tankTypeMenu);
            tankTypeMenu.ValueChanged += x => MachineInspector.Instance.SetTankType((TankType)x);
            MachineInspector.Instance.OnClick += x => tankTypeMenu.Value = x;
            tankTypeMenu.ApplyValue();
        }
    }
}
