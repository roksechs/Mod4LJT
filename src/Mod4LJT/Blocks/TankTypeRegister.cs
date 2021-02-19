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
        private TankType tankType;
        public TankType TankType => this.tankType;
        private Machine machine;
        ushort playerID;

        void Start()
        {
            MMenu tankTypeMenu = new MMenu("tankTypeMenu", 0, Enum.GetNames(typeof(TankType)).ToList(), false);
            this.BB.AddMenu(tankTypeMenu);
            tankTypeMenu.ValueChanged += (x) =>
                {
                    this.tankType = (TankType)x;
                    this.machine = this.BB._parentMachine;
                    this.playerID = !StatMaster.isMP ? (ushort) 0 : BesiegeNetworkManager.Instance.PlayerID;
                    if(this.machine.PlayerID == this.playerID)
                    {
                        MachineInspector.Instance.SetTankType(this.tankType);
                    }
                    Mod.Log("This Tank Type is " + this.tankType);
                };
        }
    }
}
