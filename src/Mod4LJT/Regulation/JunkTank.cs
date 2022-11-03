using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class JunkTank : AbstractRegulation
    {
        public override int MaxBlockCount => 251;
        private static readonly JunkTank instance = new JunkTank();
        public static JunkTank Instance => instance;

        public override Dictionary<int, BlockRestriction> BlockRestriction => this.blockRestriction;


        public JunkTank()
        {
            this.blockRestriction[(int)BlockType.Bomb] = new BlockRestriction(1, this.MaxBlockCount);
            this.blockRestriction[(int)BlockType.Cannon] = new BlockRestriction(0, 2, 0.1f, new float[] { 4, 4 });
            this.blockRestriction[(int)BlockType.ShrapnelCannon] = new BlockRestriction(0, 2, 0.1f, 4f);
            this.blockRestriction[(int)BlockType.WaterCannon] = new BlockRestriction(0, 2, 0.1f, 4f);
            this.blockRestriction[(int)BlockType.Flamethrower] = new BlockRestriction(0, 4);
            this.blockRestriction[(int)BlockType.Crossbow] = new BlockRestriction(0, 4);
            this.blockRestriction[(int)BlockType.CogMediumPowered] = new BlockRestriction(0, this.MaxBlockCount, 0f, 2f);
            this.blockRestriction[(int)BlockType.Grabber] = new BlockRestriction(0, 8);
            this.blockRestriction[(int)BlockType.Propeller] = new BlockRestriction(0, this.MaxBlockCount);
            this.blockRestriction[(int)BlockType.SmallPropeller] = new BlockRestriction(0, this.MaxBlockCount);
        }
    }
}
