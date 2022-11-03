using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class LightTank : AbstractRegulation
    {
        public override int MaxBlockCount => 221;
        private static readonly LightTank instance = new LightTank();
        public static LightTank Instance => instance;

        public override Dictionary<int, BlockRestriction> BlockRestriction => this.blockRestriction;

        public LightTank()
        {
            this.blockRestriction[(int)BlockType.Cannon] = new BlockRestriction(0, 3, 0.1f, new float[] { 7f, 5f, 4f });
            this.blockRestriction[(int)BlockType.ShrapnelCannon] = new BlockRestriction(0, 4, 0.1f, 7f);
            this.blockRestriction[(int)BlockType.WaterCannon] = new BlockRestriction(0, 6, 0.1f, 4f);
            this.blockRestriction[(int)BlockType.Flamethrower] = new BlockRestriction(0, 1);
            this.blockRestriction[(int)BlockType.Crossbow] = new BlockRestriction(0, 1);
            this.blockRestriction[(int)BlockType.Log] = new BlockRestriction(0, 10);
            this.blockRestriction[(int)BlockType.CogMediumPowered] = new BlockRestriction(0, 221, 0f, 4f);
            this.blockRestriction[(int)BlockType.Wheel] = new BlockRestriction(0, 221, 0f, 4f);
            this.blockRestriction[(int)BlockType.LargeWheel] = new BlockRestriction(0, 221, 0f, 4f);
            this.blockRestriction[(int)BlockType.SmallPropeller] = new BlockRestriction(0, 221);
            this.blockRestriction[(int)BlockType.Propeller] = new BlockRestriction(0, 221);
        }
    }
}
