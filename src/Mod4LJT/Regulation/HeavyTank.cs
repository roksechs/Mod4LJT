using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class HeavyTank : CommonRegulation
    {
        public override int MaxBlockCount => 271;
        private static readonly HeavyTank instance = new HeavyTank();
        public static HeavyTank Instance => instance;

        public override Dictionary<int, BlockRestriction> BlockRestriction => this.blockRestriction;

        public HeavyTank() : base()
        {
            this.blockRestriction[(int)BlockType.Cannon] = new BlockRestriction(0, 4, 0.1f, new float[] { 10f, 7f, 5f, 5f });
            this.blockRestriction[(int)BlockType.ShrapnelCannon] = new BlockRestriction(0, 4, 0.1f, 10f);
            this.blockRestriction[(int)BlockType.WaterCannon] = new BlockRestriction(0, 1, 0.1f, 4f);
            this.blockRestriction[(int)BlockType.Flamethrower] = new BlockRestriction(0, 2);
            this.blockRestriction[(int)BlockType.Crossbow] = new BlockRestriction(0, 2);
            this.blockRestriction[(int)BlockType.Rocket] = new BlockRestriction(0, 2, 0.5f, 4f);
            this.blockRestriction[(int)BlockType.Grabber] = new BlockRestriction(0, 10);
            this.blockRestriction[(int)BlockType.Propeller] = new BlockRestriction(0, 0);
            this.blockRestriction[(int)BlockType.SmallPropeller] = new BlockRestriction(0, 0);
        };
    }
}
}
