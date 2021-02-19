using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod4LJT.Regulation
{
    class HeavyTank : Regulation
    {
        private static readonly HeavyTank instance = new HeavyTank();
        new public static HeavyTank Instance => instance;

        public TankType tankType = TankType.HeavyTank;
        new public Dictionary<int, BlockRestriction> blockRestrictions;

        public HeavyTank()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Rocket, new BlockRestriction(0, 2) },
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 2)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 2)},
                { (int) BlockType.Cannon,  new BlockRestriction(0, 4, new float[]{ 10f, 7f, 5f, 5f })},
                { (int) BlockType.ShrapnelCannon, new BlockRestriction(0, 4, 6) },
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
