using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class HeavyTank : CommonRegulation
    {
        public override int MaxBlockCount => 271;
        private static readonly HeavyTank instance = new HeavyTank();
        public static HeavyTank Instance => instance;

        public override Dictionary<int, BlockRestriction> ChildBlockRestriction => this.blockRestrictions;
        public TankType tankType = TankType.HeavyTank;
        new public Dictionary<int, BlockRestriction> blockRestrictions;

        public HeavyTank()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Cannon,  new BlockRestriction(0, 4, 0.1f, new float[]{ 10f, 7f, 5f, 5f })},
                { (int) BlockType.ShrapnelCannon, new BlockRestriction(0, 4, 0.1f, 10f) },
                { (int) BlockType.WaterCannon, new BlockRestriction(0, 1, 0.1f, 4f) },
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 2)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 2)},
                { (int) BlockType.Rocket, new BlockRestriction(0, 2, 0.5f, 4f) },
                { (int) BlockType.Grabber, new BlockRestriction(0, 10) },
                { (int) BlockType.Propeller,  new BlockRestriction(0, 0)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(0, 0)},
            };
            foreach (var kvp in base.blockRestrictions)
            {
                if (!this.blockRestrictions.ContainsKey(kvp.Key))
                {
                    this.blockRestrictions.Add(kvp.Key, kvp.Value);
                }
            }
        }
    }
}
