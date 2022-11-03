using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class Destroyer : AbstractRegulation
    {
        public override int MaxBlockCount => 201;
        private static readonly Destroyer instance = new Destroyer();
        public static Destroyer Instance => instance;


        public override Dictionary<int, BlockRestriction> BlockRestriction => this.blockRestriction;

        public Destroyer() : base()
        {
            this.blockRestriction[(int)BlockType.ShrapnelCannon] = new BlockRestriction(0, 4, 0.1f, 13f);
            this.blockRestriction[(int)BlockType.WaterCannon] = new BlockRestriction(0, 1, 0.1f, 4f);
            this.blockRestriction[(int)BlockType.Log] = new BlockRestriction(0, 10);
            this.blockRestriction[(int)BlockType.Grabber] = new BlockRestriction(0, 10);
            this.blockRestriction[(int)BlockType.Propeller] = new BlockRestriction(0, 10);
            this.blockRestriction[(int)BlockType.SmallPropeller] = new BlockRestriction(0, 10);
            this.blockRestriction[(int)BlockType.Cannon] = new BlockRestriction(0, 2, 0.1f, new float[] { 13f, 10f });
        }
    }
}
