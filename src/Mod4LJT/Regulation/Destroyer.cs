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
                { (int) BlockType.Cannon,  new BlockRestriction(0, 2, new float[]{ 14f, 10f })},
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 1)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 1)},
                { (int) BlockType.ShrapnelCannon,  new BlockRestriction(0, 4, 14f)},
                { (int) BlockType.Log,  new BlockRestriction(0, 10)},
                { (int) BlockType.CogMediumPowered,  new BlockRestriction(0, 201, 2f)},
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
