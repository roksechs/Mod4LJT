using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod4LJT.Regulation
{
    class Destroyer : Regulation
    {
        private static readonly Destroyer instance = new Destroyer();
        new public static Destroyer Instance => instance;

        public TankType tankType = TankType.Destroyer;
        new public Dictionary<int, BlockRestriction> blockRestrictions;

        public Destroyer()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Propeller,  new BlockRestriction(0, 10)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(0, 10)},
                { (int) BlockType.Log,  new BlockRestriction(0, 10)},
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 1)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 1)},
                { (int) BlockType.Cannon,  new BlockRestriction(0, 2, new float[]{ 15f, 10f })},
                { (int) BlockType.ShrapnelCannon,  new BlockRestriction(0, 4, 15f)},
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
