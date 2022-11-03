using Mod4LJT.Regulation;
using Modding.Blocks;
using System.Collections.Generic;
using UnityEngine;

namespace Mod4LJT.Blocks
{
    class LJTMachine : MonoBehaviour
    {
        private Machine machine;
        private MMenu tankTypeMenu;
        private bool hasCompliance;
        public static readonly Dictionary<Machine, LJTMachine> MachineDic = new Dictionary<Machine, LJTMachine>();
        static readonly Dictionary<int, AbstractRegulation> regulations = new Dictionary<int, AbstractRegulation>()
        {
            { (int) TankType.LightTank, LightTank.Instance },
            { (int) TankType.MediumTank, MediumTank.Instance },
            { (int) TankType.HeavyTank, HeavyTank.Instance },
            { (int) TankType.Destroyer, Destroyer.Instance },
            { (int) TankType.SelfPropelledArtillery, SelfPropelledArtillery.Instance },
            { (int) TankType.JunkTank, JunkTank.Instance },
        };

        public Machine Machine { get => this.machine; set => this.machine = value; }
        public MMenu TankTypeMenu { set => this.tankTypeMenu = value; }
        public TankType TankType => (TankType)this.tankTypeMenu.Value;
        public AbstractRegulation Regulation => regulations[(int)this.TankType];
        public bool HasCompliance { get => this.hasCompliance; set => this.hasCompliance = value; }

        void Start()
        {
            if (LJTMachine.MachineDic.ContainsKey(this.machine))
            {
                LJTMachine.MachineDic[this.machine] = this;
            }
            else
            {
                LJTMachine.MachineDic.Add(this.machine, this);
            }
        }

        void OnDestroy()
        {
            LJTMachine.MachineDic.Remove(this.machine);
        }
    }
}
