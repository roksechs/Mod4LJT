using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod4LJT.Regulation
{
    class MachineInspector : SingleInstance<MachineInspector>
    {
        private TankType tankType;
        private Regulation regulation;
        private Dictionary<int, Regulation> regulations = new Dictionary<int, Regulation>()
        {
            { (int) TankType.LightTank, LightTank.Instance },
            { (int) TankType.MediumTank, MediumTank.Instance },
            { (int) TankType.HeavyTank, HeavyTank.Instance },
            { (int) TankType.Destroyer, Destroyer.Instance },
            { (int) TankType.Artillery, Mod4LJT.Regulation.Artillery.Instance },
        };

        public void SetTankType(TankType tankType)
        {
            this.tankType = tankType;
            this.regulations.TryGetValue((int)this.tankType, out this.regulation);
            foreach (var kvp in this.regulation.blockRestrictions)
            {
                Mod.Log(((TankType)kvp.Key).ToString());
            }
        }

        public int NumOfBlock(int blockId)
        {
            int num = 0;
            foreach (BlockBehaviour buildingBlock in Machine.Active().BuildingBlocks)
            {
                if (buildingBlock.BlockID == blockId)
                    ++num;
            }
            return num;
        }

        public override string Name => "MachineInspector";
    }
}
