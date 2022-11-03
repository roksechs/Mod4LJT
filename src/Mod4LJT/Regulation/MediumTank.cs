using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class MediumTank : CommonRegulation
    {
        public override int MaxBlockCount => 251;
        private static readonly MediumTank instance = new MediumTank();
        public static MediumTank Instance => instance;

        public override Dictionary<int, BlockRestriction> BlockRestriction => this.blockRestriction;

        public MediumTank()
        {
            this.blockRestriction[(int)BlockType.Cannon] = new BlockRestriction(0, 3, 0.1f, new float[] { 8f, 6f, 5f });
            this.blockRestriction[(int)BlockType.ShrapnelCannon] = new BlockRestriction(0, 4, 0.1f, 8f);
            this.blockRestriction[(int)BlockType.Flamethrower] = new BlockRestriction(0, 2);
            this.blockRestriction[(int)BlockType.Crossbow] = new BlockRestriction(0, 2);
            this.blockRestriction[(int)BlockType.Log] = new BlockRestriction(0, 30);
            this.blockRestriction[(int)BlockType.Propeller] = new BlockRestriction(0, 10);
            this.blockRestriction[(int)BlockType.SmallPropeller] = new BlockRestriction(0, 10);
        }
    }
}
