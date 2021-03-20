using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod4LJT.Regulation
{
    class JunkTank : CommonRegulation
    {
        public override int MaxBlockCount => 251;
        private static readonly JunkTank instance = new JunkTank();
        public static JunkTank Instance => instance;

        public override Dictionary<int, BlockRestriction> ChildBlockRestriction => this.blockRestrictions;
        public TankType tankType = TankType.HeavyTank;
        new public Dictionary<int, BlockRestriction> blockRestrictions;

        public JunkTank()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Bomb, new BlockRestriction(1, 251) },
                { (int) BlockType.Cannon, new BlockRestriction(0, 2, 0.1f, new float[]{ 4, 4 }) },
                { (int) BlockType.ShrapnelCannon, new BlockRestriction(0, 2, 0.1f, 4f) },
                { (int) BlockType.WaterCannon, new BlockRestriction(0, 2, 0.1f, 4f) },
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 4)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 4)},
                { (int) BlockType.CogMediumPowered, new BlockRestriction(0, 251, 0f, 2f) },
                { (int) BlockType.Grabber, new BlockRestriction(0, 8) },
                { (int) BlockType.Propeller,  new BlockRestriction(0, 251)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(0, 251)},
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
