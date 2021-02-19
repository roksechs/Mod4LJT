using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod4LJT.Regulation
{
    class MediumTank : Regulation
    {
        private static readonly MediumTank instance = new MediumTank();
        new public static MediumTank Instance => instance;

        public TankType tankType = TankType.MediumTank;
        new public Dictionary<int, BlockRestriction> blockRestrictions;

        public MediumTank()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Propeller,  new BlockRestriction(0, 10)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(0, 10)},
                { (int) BlockType.Log,  new BlockRestriction(0, 20)},
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 2)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 2)},
                { (int) BlockType.Cannon,  new BlockRestriction(0, 3, new float[]{ 8f, 6f, 5f })},
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
