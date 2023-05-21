using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class Destroyer : CommonRegulation
    {
        public override int MaxBlockCount => 201;
        private static readonly Destroyer instance = new Destroyer();
        public static Destroyer Instance => instance;


        public override Dictionary<int, BlockRestriction> ChildBlockRestriction => this.blockRestrictions;
        public TankType tankType = TankType.Destroyer;
        new public Dictionary<int, BlockRestriction> blockRestrictions;

        public Destroyer()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Cannon,  new BlockRestriction(0, 2, 0.1f, new float[]{ 12f, 10f })},
                { (int) BlockType.ShrapnelCannon,  new BlockRestriction(0, 6, 0.1f, 13f)},
                { (int) BlockType.WaterCannon,  new BlockRestriction(0, 1, 0.1f, 4f)},
                { (int) BlockType.Log,  new BlockRestriction(0, 10)},
                { (int) BlockType.Grabber, new BlockRestriction(0, 10) },
                { (int) BlockType.Propeller,  new BlockRestriction(0, 10)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(0, 10)},
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
