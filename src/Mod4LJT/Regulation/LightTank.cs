using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod4LJT.Regulation
{
    class LightTank : Regulation
    {
        private static readonly LightTank instance = new LightTank();
        public static LightTank Instance => instance;

        public override Dictionary<int, BlockRestriction> ChildBlockRestriction => this.blockRestrictions;
        public TankType tankType = TankType.LightTank;
        new public Dictionary<int, BlockRestriction> blockRestrictions;

        public LightTank()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Propeller,  new BlockRestriction(10, int.MaxValue)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(10, int.MaxValue)},
                { (int) BlockType.Log,  new BlockRestriction(0, 10)},
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 1)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 1)},
                { (int) BlockType.Cannon,  new BlockRestriction(0, 2, new float[]{ 7f, 5f })},
                { (int) BlockType.CogMediumPowered,  new BlockRestriction(0, int.MaxValue, 4)},
            };
            foreach(var kvp in base.blockRestrictions)
            {
                if (!this.blockRestrictions.ContainsKey(kvp.Key))
                {
                    this.blockRestrictions.Add(kvp.Key, kvp.Value);
                }
            }
        }
    }
}
